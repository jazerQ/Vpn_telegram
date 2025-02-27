
using DataAccess;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

internal class Program
{
    private static TelegramBotClient bot;
    private async static Task Main(string[] args)
    {
        
        using var cts = new CancellationTokenSource();
        bot = await TelegramBotClientFabric.GetTelegramBotClientAsync(cts.Token);
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = new[] { UpdateType.Message },
            
        };
        bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken: cts.Token);
        var me = await bot.GetMe();
        Console.WriteLine($"Бот под названием {me.FirstName} запущен");
        Console.ReadLine();
        cts.Cancel();
    }
    //private static List<string> _strings = new List<string> { "сука", "иди нахуй", "ой бляя", "ты даун или да?", "долбаеб", "ущербный", "что это возле плинтуса, а это твой уровень тестостерона" };
    //private static Random _random = new Random();
    //private static CancellationTokenSource? _source;
    //private static int _timer;
    private static IServiceCollection builder;
    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message) return;
        if (message.Text is not { } messageText) return;
        long chatId = message.Chat.Id;
        switch (message.Text.Split(' ')[0].ToLower()) {
            case "/start":
                await bot.SendMessage(chatId, $"{message.From.FirstName}, Привет я твой телеграмм бот для VPN 🚀!", cancellationToken: cancellationToken);
                break;
            case "/help":
                await bot.SendMessage(chatId, $"список команд для бота: /start - запускает бота \n/help - список возможных команд", cancellationToken: cancellationToken);
                break;
            case "/setname" when message.Text.Split(' ').Length > 1:

                break;
            case "/setname":
                await bot.SendMessage(chatId, "укажите команду /setname {имя}", cancellationToken: cancellationToken);
                break;
            default:
                await bot.SendMessage(chatId, $"я не понимаю такой команды", cancellationToken: cancellationToken);
                break;
        
        }
    }
    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) 
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
    
}