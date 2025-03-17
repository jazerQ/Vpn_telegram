using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Exceptions;
using Infrastructure.VpnLibrary;
using StackExchange.Redis;
using Telegram.Bot;
using Telegram.Bot.Types;
using Weather_bot.Commands.Keyboard;

namespace Weather_bot.Commands
{
    public class VpnCommand
    {
        private readonly VpnAccessGlobal _vpnAccess;
        public VpnCommand(VpnAccessGlobal vpnAccess)
        {
            _vpnAccess = vpnAccess;
        }
        public async Task GetList() 
        {
            await _vpnAccess.GetListInbounds();
        }
        public async Task GetInboundById(int id) 
        {
            await _vpnAccess.GetInboundById(id);
        }
        public async Task GetInboundByEmail(string email) 
        {
            await _vpnAccess.GetInboundByEmail(email);
        }
        public async Task GetInboundByUserId(string userId) 
        {
            await _vpnAccess.GetInboundByUserId(userId);
        }
        public async Task AddSimpleUserToInbound(ITelegramBotClient bot, Message message, string username, CancellationToken cancellationToken) 
        {
            try
            {

                TelegramUser telegramUser = GetTelegramUser(message, username);
                await _vpnAccess.AddSimpleUserToInbound(telegramUser, cancellationToken);
                await bot.SendMessage(message.From.Id, $"твой тариф ровно на 30 минут, получается он заканчивается {DateTime.Now.AddMinutes(30).ToShortDateString()}", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);

                await bot.SendMessage(message.From.Id, $"создал строку подключения введи команду \"моя строка подключения\"", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            catch (AlreadyHaveException ex)
            {
                await bot.SendMessage(message.From.Id, $"у тебя уже есть строка подключения введи команду \"моя строка подключения\"", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            catch (VpnTimeIsOverException ex) 
            {
                await bot.SendMessage(message.From.Id, $"{ex.Message}", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                await bot.SendMessage(message.From.Id, $"{ex.Message}", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);

            }
        }
        public async Task AddPrimaryUserToInbound(ITelegramBotClient bot, Message message, string username, CancellationToken cancellationToken)
        {
            try
            {
                TelegramUser telegramUser = GetTelegramUser(message, username);
                await _vpnAccess.AddPrimaryToInbound(telegramUser, cancellationToken);
                await bot.SendMessage(message.From.Id, $"твой тариф ровно на один месяц, получается он заканчивается {DateTime.Now.AddDays(30).ToShortDateString()}", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);

                await bot.SendMessage(message.From.Id, $"создал строку подключения введи команду \"моя строка подключения\"", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            catch (AlreadyHaveException ex)
            {
                await bot.SendMessage(message.From.Id, $"у тебя уже есть строка подключения введи команду \"моя строка подключения\"", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            catch (VpnTimeIsOverException ex) 
            {
                await bot.SendMessage(message.From.Id, $"{ex.Message}", replyMarkup: KeyboardService.GetInlineKeyboardForPay(message.From.Id), cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                await bot.SendMessage(message.From.Id, $"{ex.Message}", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);

            }
        }
        public async Task GetMyConnectionString(ITelegramBotClient bot, long tgId, CancellationToken cancellationToken) 
        {
            try
            {
                var connectionString = await _vpnAccess.GetConnectionString(tgId, cancellationToken);
                var time = await _vpnAccess.GetRemainderTime(tgId, cancellationToken);
                if (time.Nanoseconds < -1)
                {
                    await bot.SendMessage(tgId, $"твоя подписка закончилась, купи новую чтобы дальше пользоваться впн", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
                    return;
                }
                await bot.SendMessage(tgId, $"Вот твоя строка подключения - {connectionString} \nТебе осталось пользоваться ВПН {time.Days} дней, {time.Hours} часов, {time.Minutes} минут, {time.Seconds} секунд", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);
            }
            catch (SqlNullValueException ex)
            {
                await bot.SendMessage(tgId, $"я не смог найти твою строку подключения, попробуй взять платный или пробный впн командой на клавиатуре", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);

                throw;
            }
            catch (Exception ex)
            {
                await bot.SendMessage(tgId, $"Ошибка! {ex.Message}", replyMarkup: KeyboardService.GetMainKeyboard(), cancellationToken: cancellationToken);

                throw;
            }
        }
        private TelegramUser GetTelegramUser(Message message, string username) 
        {
            if (message.From == null) throw new Exception("i can`t take local information");
            TelegramUser telegramUser = new TelegramUser
            {
                Id = message.From.Id,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName ?? "",
                Name = username,
                Shortname = message.From.Username ?? "",
                StartDate = DateTime.Now,
            };
            return telegramUser;
        }
    }
}
