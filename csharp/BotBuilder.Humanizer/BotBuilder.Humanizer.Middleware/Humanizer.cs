using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Middleware;
using Microsoft.Bot.Schema;

namespace BotBuilder.Humanizer.Middleware
{
    public partial class Humanizer : IMiddleware, ISendActivity
    {
        private static IList<string> _wrongPersonStatements = new[]
        {
            "not a chance",
            "thursday",
            "I said 7!!",
            "thbbbbpt!"
        };

        private readonly double _misspelling, _wrongPerson, _transpositions, _typingSpeed;
        public Humanizer(HumanizerOptions options)
        {
            _misspelling = options.Misspelling;
            _wrongPerson = options.WrongPerson;
            _transpositions = options.Transpositions;
            _typingSpeed = options.TypingSpeed;
        }

        public async Task SendActivity(IBotContext context, IList<IActivity> activities, MiddlewareSet.NextDelegate next)
        {
            foreach (var response in activities.OfType<IMessageActivity>().ToList())    // ToList required else collection labeled "modified"
            {
                var delay = response.Text.Split(' ').Length / (_typingSpeed / 60 / 1000);

                // transpositions
                if (new Random().NextDouble() < _transpositions)
                {
                    var text = response.Text.ToCharArray();
                    var position = this.Random(0, text.Length - 2);

                    var aux = text[position + 1];
                    text[position + 1] = text[position];
                    text[position] = aux;

                    response.Text = new string(text);
                }

                // wrongPerson
                if (new Random().NextDouble() < _wrongPerson)
                {
                    TypingDelay(context, delay)
                        .Reply(_wrongPersonStatements[Random(0, _wrongPersonStatements.Count - 1)]);
                    TypingDelay(context, delay)
                        .Reply("sorry... that last message was meant for someone else");
                }

                // misspelling
                foreach (var word in response.Text.Split(' '))
                {
                    if (_misspellings.TryGetValue(word, out var replacement) && new Random().NextDouble() < _misspelling)
                    {
                        response.Text = response.Text.Replace(word, replacement);
                        TypingDelay(context, delay).Reply($@"{word}*");
                    }
                }
            }

            await next();
        }

        private IBotContext TypingDelay(IBotContext context, double msDelay) => context.ShowTyping().Delay((int)msDelay);

        private int Random(double a, double b) => (int)Math.Round(b + (new Random().NextDouble() * (a - b)));
    }
}
