﻿using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace NumberChallenge.Responses
{
    /// <summary>
    /// I organized all of my output responses as functions so it is easy to reuse and evolve the responses over time without having to rewrite my business logic
    /// </summary>
    public static class DefaultResponses
    {
        public static async Task ReplyWithGreeting(ITurnContext context)
        {
            await context.SendActivity($"Hello, I'm the number guessing game bot.");
        }

        public static async Task ReplyWithHelp(ITurnContext context)
        {
            await context.SendActivity($"You can guess or show guesses.");
        }

        public static async Task ReplyWithResumeTopic(ITurnContext context)
        {
            await context.SendActivity($"What next?");
        }

        public static async Task ReplyWithConfused(ITurnContext context)
        {
            await context.SendActivity($"I am sorry, I didn't understand that.");
        }
    }
}