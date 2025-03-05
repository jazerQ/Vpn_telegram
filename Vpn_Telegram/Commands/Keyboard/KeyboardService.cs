using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Weather_bot.Commands.Keyboard
{
    public static class KeyboardService
    {
        public static ReplyKeyboardMarkup GetMainKeyboard()
        {
            return new ReplyKeyboardMarkup(new[] {
                new KeyboardButton[]{ "Получить пробный ВПН", "моя строка подключения" },
                new KeyboardButton[]{ "Полная подписка" },
                new KeyboardButton[]{ "О проекте" }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        
        }
        public static InlineKeyboardMarkup GetInlineKeyboardAboutProject() 
        {
            return new InlineKeyboardMarkup(new[] {
                new[]{ InlineKeyboardButton.WithUrl("Github проекта", "https://github.com/jazerQ/weather_telegram_bot") }
            });
        }
        public static InlineKeyboardMarkup GetInlineKeyboardForPay() 
        {
            return new InlineKeyboardMarkup(new[] {new[]{ InlineKeyboardButton.WithUrl("оплатить", "https://github.com/jazerQ") }
            });
        }
    }
}
