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
            sb.AppendLine("# Current Guesses\n");

            if (guesses != null && guesses.Any())
            {
                foreach (var guess in guesses)
                {
                    sb.AppendLine($"* {guess.GuessValue}");
                }
            }
            else
                sb.AppendLine("*There are no guesses yet.*");

            await context.SendActivity(sb.ToString());
        }
    }
}
