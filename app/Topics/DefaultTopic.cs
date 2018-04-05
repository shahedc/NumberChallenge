using System.Linq;
using System.Threading.Tasks;
using NumberChallenge;
using NumberChallenge.Models;
using NumberChallenge.Responses;
using Microsoft.Bot.Schema;

namespace NumberChallenge.Topics
{
    /// <summary>
    /// Class around root default topic 
    /// </summary>
    public class DefaultTopic : ITopic
    {
        public string Name { get; set; } = "Default";

        // track in this topic if we have greeted the user already
        public bool Greeted { get; set; } = false;

        /// <summary>
        /// Called when the default topic is started
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> StartTopic(NumberBotContext context)
        {
            switch (context.Activity.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    {
                        // greet when added to conversation
                        var activity = context.Activity.AsConversationUpdateActivity();
                        if (activity.MembersAdded.Any(m => m.Id == activity.Recipient.Id))
                        {
                            await DefaultResponses.ReplyWithGreeting(context);
                            await DefaultResponses.ReplyWithHelp(context);
                            this.Greeted = true;
                        }
                    }
                    break;

                case ActivityTypes.Message:
                    // greet on first message if we haven't already 
                    if (!Greeted)
                    {
                        await DefaultResponses.ReplyWithGreeting(context);
                        this.Greeted = true;
                    }
                    return await this.ContinueTopic(context);
            }

            return true;
        }

        /// <summary>
        /// Continue the topic, method which is routed to while this topic is active
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> ContinueTopic(NumberBotContext context)
        {
            switch (context.Activity.Type)
            {
                case ActivityTypes.Message:
                    switch (context.RecognizedIntents.TopIntent?.Name)
                    {
                        case "startGame":
                            // switch to startGame topic
                            context.ConversationState.ActiveTopic = new StartGameTopic();
                            return await context.ConversationState.ActiveTopic.StartTopic(context);
                        case "showGames":
                            // switch to show games topic
                            context.ConversationState.ActiveTopic = new ShowGamesTopic();
                            return await context.ConversationState.ActiveTopic.StartTopic(context);
                        //case "deleteAlarm":
                        //    // switch to delete alarm topic
                        //    context.ConversationState.ActiveTopic = new DeleteAlarmTopic();
                        //    return await context.ConversationState.ActiveTopic.StartTopic(context);
                        case "help":
                            // show help
                            await DefaultResponses.ReplyWithHelp(context);
                            return true;

                        default:
                            // show our confusion
                            await DefaultResponses.ReplyWithConfused(context);
                            return true;
                    }
                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// Method which is called when this topic is resumed after an interruption
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> ResumeTopic(NumberBotContext context)
        {
            // just prompt the user to ask what they want to do
            await DefaultResponses.ReplyWithResumeTopic(context);
            return true;
        }
    }
}