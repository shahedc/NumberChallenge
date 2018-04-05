using System;
using System.Text;
using System.Threading.Tasks;
using NumberChallenge.Models;
using Microsoft.Bot.Builder;

namespace NumberChallenge.Responses
{
    /// <summary>
    /// I organized all of my output responses as functions so it is easy to reuse and evolve the responses over time without having to rewrite my business logic
    /// </summary>
    public static class AddGameResponses
    {
        public static async Task ReplyWithStartTopic(ITurnContext context)
        {
            await context.SendActivity($"Ok, let's start a game.");
        }

        public static async Task ReplyWithHelp(ITurnContext context, Game game = null)
        {
            await context.SendActivity($"I am working with you to create a game.  To do that I need to know the title.\n\n{GameDescription(context, game)}");
        }

        public static async Task ReplyWithConfused(ITurnContext context)
        {
            await context.SendActivity($"I am sorry, I didn't understand: {context.Activity.AsMessageActivity().Text}.");
        }

        public static async Task ReplyWithCancelPrompt(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithSuggestions(context, "Cancel Game?", $"Did you want to cancel the game?\n\n{GameDescription(context, game)}", YesNo));
        }

        public static async Task ReplyWithCancelReprompt(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithSuggestions(context, $"Cancel Game?", $"Please answer the question with a \"yes\" or \"no\" reply. Did you want to cancel the game?\n\n{GameDescription(context, game)}", YesNo));
        }

        public static async Task ReplyWithTopicCanceled(ITurnContext context)
        {
            await context.SendActivity($"OK, I have canceled this game.");
        }

        public static async Task ReplyWithTimePrompt(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithTitle(context, $"Starting game", $"{GameDescription(context, game)}\n\nWhat time would you like to set the game for?"));
        }

        public static async Task ReplyWithTimePromptFuture(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithTitle(context, $"Starting game", $"{GameDescription(context, game)}\n\nYou need to specify a time in the future. What time would you like to set the game?"));
        }

        public static async Task ReplyWithTitlePrompt(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithTitle(context, $"Guess a number:", $"{GameDescription(context, game)}\n\nWhat is your guess?"));
        }

        public static async Task ReplyWithTitleValidationPrompt(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithTitle(context, $"Starting game", $"Your title needs to be between 1 and 100 characterslong\n\n{GameDescription(context, game)}\n\nWhat would you like to call your game ?"));
        }

        public static async Task ReplyWithAddConfirmation(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithSuggestions(context, $"Storing guess...", $"{GameDescription(context, game)}\n\nDo you want to submit this guess?", YesNo));
        }

        public static async Task ReplyWithAddedAlarm(ITurnContext context, Game game)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithTitle(context, $"Guess stored", $"{GameDescription(context, game)}."));
        }

        public static async Task ReplyWithAddedGuess(ITurnContext context, Guess guess)
        {
            await context.SendActivity(ResponseHelpers.ReplyWithTitle(context, $"Guess stored", $"{GuessInfo(context, guess)}."));
        }

        /// <summary>
        /// Standard language game description
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GameDescription(ITurnContext context, Game game)
        {
            StringBuilder sb = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(game.Title))
                sb.AppendLine($"* Guess: {game.Title}");
            else
                sb.AppendLine($"* Guess: -");

            return sb.ToString();
        }

        public static string GuessInfo(ITurnContext context, Guess guess)
        {
            return $"* Guess: {guess.GuessValue}";
        }

        public static string[] YesNo = { "Yes", "No" };
    }    
}