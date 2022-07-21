using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.InputFiles;

namespace TelegramBotFilesharer
{
    class Program
    {
        static ITelegramBotClient botClient = new TelegramBotClient("xxx");

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            const string mainImage = @"https://static.1000.menu/img/content-v2/17/21/27110/postnye-draniki-iz-kartoshki_1614782786_14_max.jpg";
            Console.WriteLine(JsonSerializer.Serialize(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {

                if (message.Text == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Ыернул null");
                }
                else
                {
                    if (message.Text.ToLower() == "/dranik")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Подготовьте ингредиенты:Картофель  — 5 Штук, Сыр твердый  — 100 Грамм, Яйцо  — 1 Штука," +
                            " Чеснок  — 1 Зубчик, Лук зеленый  — 3 Штуки, Соль  — 1/2 Чайных ложки, Перец черный молотый  — 1/4 Чайных ложки, Паприка копченая  — 1/2" +
                            " Чайных ложки, Мука пшеничная  — 3 Ст. ложки, Масло для жарки  — 4-5 Ст. ложек." +
                            " \n Приступим! Картофель очистите, натрите на крупной терке." +
                            " Добавьте тертый сыр (лучше на мелкой терке), яйцо, измельченный чеснок и зеленый лук. Добавьте муку, паприку, соль, перец черный " +
                            "молотый. Тщательно перемешайте картофельную массу. Разогрейте ложку масла в сковороде, выкладывайте картофельную массу, формируя" +
                            " круглые лепешки. Жарьте до золотистой корочки. Переверните лопаткой и обжарьте с другой стороны. Драники выкладывайте на бумажное" +
                            " полотенце, чтобы убрать лишний жир. Пережарьте все драники. Из указанного количества ингредиентов у меня получилось 15 штук." +
                            " Готовые драники подавайте горячими со сметаной.");

                        await botClient.SendPhotoAsync(
                            chatId: message.Chat.Id,
                            photo: mainImage,
                           caption: "Приятного Аппетита!");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Введите команду корректно (/dranik)");
                    }

                }
            }
        }


        static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonSerializer.Serialize(exception));
        }


        public static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + botClient.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}