using System.Collections.Generic;
using System.Threading.Tasks;
using NumberChallenge.Models;
using NumberChallenge.Responses;
using Microsoft.Bot.Builder;

namespace NumberChallenge.Topics
{
    /// <summary>
    /// Class around topic of listing games
    /// </summary>
    public class ShowGamesTopic : ITopic
    {
        public ShowGamesTopic()
        {
        }

        public string Name { get; set; } = "ShowGames";

        /// <summary>
        /// Called when topic is activated (SINGLE TURN)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> StartTopic(NumberBotContext context)
        {
            await ShowGames(context);

            // end the topic immediately
            return false;
        }

        public Task<bool> ContinueTopic(NumberBotContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ResumeTopic(NumberBotContext context)
        {
            throw new System.NotImplementedException();
        }


        public static async Task ShowGames(NumberBotContext context)
        {
            await ShowGamesResponses.ReplyWithShowGames(context, context.UserState.Games);
        }

    }
}
