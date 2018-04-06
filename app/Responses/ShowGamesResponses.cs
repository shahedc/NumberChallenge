using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberChallenge.Models;
using Microsoft.Bot.Builder;

namespace NumberChallenge.Responses
{
    /// <summary>
    /// I organized all of my output responses as functions so it is easy to reuse and evolve the responses over time without having to rewrite my business logic
    /// </summary>
    public static class ShowGamesResponses
    {
        public static async Task ReplyWithShowGames(ITurnContext context, IEnumerable<Guess> guesses)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Secret Number: {((NumberBotContext)context).UserState.SecretNumber}\n");
            sb.AppendLine("# Current Guesses\n");

            if (guesses != null && guesses.Any())
            {
                foreach (var guess in guesses)
                {
                    var compText = new StringBuilder("(");
                    if (guess.Comp < 0)
                        compText.Append("too low");
                    else if (guess.Comp > 0)
                        compText.Append("too high");
                    else if (guess.Comp == 0)
                        compText.Append("correct!");

                    compText.Append(")");
                    sb.AppendLine($"* {guess.GuessValue}: {compText}");
                }
            }
            else
                sb.AppendLine("*There are no guesses yet.*");

            await context.SendActivity(sb.ToString());
        }
    }
}
