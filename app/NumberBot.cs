using System;
using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using NumberChallenge.Models;
using NumberChallenge.Topics;

namespace NumberChallenge
{
    public class NumberBot : IBot
    {
        public async Task OnReceiveActivity(ITurnContext botContext)
        {
            // Get the current ActiveTopic from my persisted conversation state
            var context = new NumberBotContext(botContext);

            var handled = false;

            // if we don't have an active topic yet
            if (context.ConversationState.ActiveTopic == null)
            {
                // use the default topic
                context.ConversationState.ActiveTopic = new DefaultTopic();
                handled = await context.ConversationState.ActiveTopic.StartTopic(context);
            }
            else
            {
                // we do have an active topic, so call it 
                handled = await context.ConversationState.ActiveTopic.ContinueTopic(context);
            }

            // if activeTopic's result is false and the activeTopic is NOT already the default topic
            if (handled == false && !(context.ConversationState.ActiveTopic is DefaultTopic))
            {
                // Use DefaultTopic as the active topic
                context.ConversationState.ActiveTopic = new DefaultTopic();
                await context.ConversationState.ActiveTopic.ResumeTopic(context);
            }
        }
    }
}