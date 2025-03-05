using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure
{
    public class CallbackService
    {
        public async Task CallbackAction(ITelegramBotClient bot, CallbackQuery callback) 
        {
            if (callback == null || callback.Message == null || callback.Message.From == null) throw new NullReferenceException("Callback can`t be null");
            long chatId = callback.Message.From.Id;
            string callbackData = callback.Data;
            switch (callbackData)
            {
                case "fdf":
                    Console.WriteLine("f");
                    break;
                default:
                    Console.WriteLine("a");
                    break;

            }
        }
    }
}
