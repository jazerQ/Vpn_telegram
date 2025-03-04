using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Core.Abstractions;
using Core.Entities;
using Infrastructure;
using Infrastructure.VpnLibrary;
using Microsoft.EntityFrameworkCore.Storage;
using Telegram.Bot;
using Telegram.Bot.Types;
using Weather_bot.Actions;
using Weather_bot.Commands;
using Weather_bot.Commands.Keyboard;
using Weather_bot.Controllers;

namespace Vpn_Telegram
{
    public class BotHandler
    {
        private readonly ITelegramUserService _serviceUser;
        private readonly IRedisService _redisService;
        private readonly ActionByKey _actionByKey;
        private readonly VpnCommand _vpnCommand;
        public BotHandler(ITelegramUserService serviceUser, IRedisService redisService, ActionByKey actionByKey, VpnCommand vpnCommand)
        {
            _redisService = redisService;
            _serviceUser = serviceUser;
            _actionByKey = actionByKey;
            _vpnCommand = vpnCommand;            
        }
        public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message) return;
            if (message.Text is not { } messageText) return;
            string user = await _serviceUser.GetNameById(message.From.Id, cancellationToken) ?? message.From.FirstName;
            long chatId = message.Chat.Id;
            if(await _redisService.Db.KeyExistsAsync(chatId.ToString()))
            {
                await _actionByKey.DoAction(bot, message, cancellationToken);
                return;
            }
            if (int.TryParse(message.Text, out int id)) 
            {
                await _vpnCommand.GetInboundById(id);
                await bot.SendMessage(chatId, $"отправил в логи информацию об подключении номер", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            switch (message.Text.Split(' ')[0].ToLower())
            {
                
                case "/start":
                    await StartCommands.ExecuteAsync(bot, chatId, user, cancellationToken);
                    break;
                case "/help":
                    await bot.SendMessage(chatId, $"список команд для бота: /start - запускает бота \n/help - список возможных команд", cancellationToken: cancellationToken);
                    break;
                case "/inboundid" when message.Text.Split(' ').Length > 1:
                    await _vpnCommand.GetInboundByUserId(message.Text.Split(' ')[1]);
                    await bot.SendMessage(chatId, $"отправил в лог", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
                    break;
                case "addme":
                    await _vpnCommand.AddInbound(message.From.Id.ToString());
                    await bot.SendMessage(chatId, $"добавил", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
                    break;
                case "/inbound" when message.Text.Split(' ').Length > 1: 
                    await _vpnCommand.GetInboundByEmail(message.Text.Split(' ')[1]);
                    await bot.SendMessage(chatId, $"отправил в логи информацию о пользователе {message.Text.Split(' ')[1]}", cancellationToken: cancellationToken);
                    break;
                case "поменять имя":
                    await NameCommands.ChangeNameRequest(_redisService.Db, bot, chatId, cancellationToken);
                    break;
                case "мое имя":
                    await NameCommands.GetMyName(bot, chatId, user, cancellationToken);
                    break;
                case "о проекте":
                    await AboutUsCommands.GetInfo(bot, chatId, user, cancellationToken);
                    break;
                case "базаучастников":
                    await _vpnCommand.GetList();
                    await bot.SendMessage(chatId, "отправил в логи", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
                    break;
                default:
                    
                    await bot.SendMessage(chatId, $"я не понимаю такой команды", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
                    break;

            }
        }
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
            return;
        }
    }
}
