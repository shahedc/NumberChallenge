using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberChallenge.Models;
using NumberChallenge.Responses;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace NumberChallenge.Topics
{
    /// <summary>
    /// Class around topic of adding a game
    /// </summary>
    public class AddGameTopic : ITopic
    {
        /// <summary>
        /// enumeration of states of the converation
        /// </summary>
        public enum TopicStates
        {
            // initial state
            Started,

            // we asked for title
            TitlePrompt,

            // we asked for time
            TimePrompt,

            // we asked for confirmation to cancel
            CancelConfirmation,

            // we asked for confirmation to add
            AddConfirmation,

            // we asked for confirmation to show help instead of allowing help as the answer
            HelpConfirmation
        };

        /// <summary>
        /// Game object representing the information being gathered by the conversation before it is committed
        /// </summary>
        public Game Game { get; set; }

        /// <summary>
        /// Current state of the topic conversation
        /// </summary>
        public TopicStates TopicState { get; set; } = TopicStates.Started;

        public string Name { get; set; } = "AddGame";

        /// <summary>
        /// Called when the add game topic is started
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<bool> StartTopic(NumberBotContext context)
        {
            this.Game = new Game()
            {
                // initialize from intent entities
                Title = context.RecognizedIntents.TopIntent?.Entities.Where(entity => entity.GroupName == "1")
                    .Select(entity => entity.ValueAs<string>()).FirstOrDefault(),

            };

            return PromptForMissingData(context);
        }


        /// <summary>
        /// we call for every turn while the topic is still active
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> ContinueTopic(NumberBotContext context)
        {
            // for messages
            if (context.Activity.Type == ActivityTypes.Message)
            {
                switch (context.RecognizedIntents.TopIntent?.Name)
                {
                    case "showGames":
                        // allow show alarm to interrupt, but it's one turn, so we show the data without changing the topic
                        await new ShowGamesTopic().StartTopic(context);
                        return await this.PromptForMissingData(context);

                    case "help":
                        // show contextual help 
                        await AddGameResponses.ReplyWithHelp(context, this.Game);
                        return await this.PromptForMissingData(context);

                    case "cancel":
                        // prompt to cancel
                        await AddGameResponses.ReplyWithCancelPrompt(context, this.Game);
                        this.TopicState = TopicStates.CancelConfirmation;
                        return true;

                    default:
                        return await ProcessTopicState(context);
                }
            }
            return true;
        }

        private async Task<bool> ProcessTopicState(NumberBotContext context)
        {
            string utterance = (context.Activity.AsMessageActivity().Text ?? "").Trim();

            // we ar eusing TopicState to remember what we last asked
            switch (this.TopicState)
            {
                case TopicStates.TitlePrompt:
                    this.Game.Title = utterance;
                    return await this.PromptForMissingData(context);                    

                case TopicStates.AddConfirmation:
                    switch (context.RecognizedIntents.TopIntent?.Name)
                    {
                        case "confirmYes":
                            if (context.UserState.Games == null)
                            {
                                context.UserState.Games = new List<Game>();
                            }
                            context.UserState.Games.Add(this.Game);
                            await AddGameResponses.ReplyWithAddedAlarm(context, this.Game);
                            // end topic
                            return false;

                        case "confirmNo":
                            await AddGameResponses.ReplyWithTopicCanceled(context);
                            // End current topic
                            return false;
                        default:
                            return await this.PromptForMissingData(context);
                    }

                default:
                    return await this.PromptForMissingData(context);
            }
        }

        /// <summary>
        /// Called when this topic is resumed after being interrupted
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<bool> ResumeTopic(NumberBotContext context)
        {
            // simply prompt again based on our state
            return this.PromptForMissingData(context);
        }

        /// <summary>
        /// Shared method to get missing information
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> PromptForMissingData(ITurnContext context)
        {
            // If we don't have a title (or if its too long), prompt the user to get it.
            if (String.IsNullOrWhiteSpace(this.Game.Title))
            {
                this.TopicState = TopicStates.TitlePrompt;
                await AddGameResponses.ReplyWithTitlePrompt(context, this.Game);
                return true;
            }
            // if title exists but is not valid, then provide feedback and prompt again
            else if (this.Game.Title.Length < 1 || this.Game.Title.Length > 100)
            {
                this.Game.Title = null;
                this.TopicState = TopicStates.TitlePrompt;
                await AddGameResponses.ReplyWithTitleValidationPrompt(context, this.Game);
                return true;
            }
            
            // ask for confirmation that we want to add it
            await AddGameResponses.ReplyWithAddConfirmation(context, this.Game);
            this.TopicState = TopicStates.AddConfirmation;
            return true;
        }
    }
}