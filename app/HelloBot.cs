using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;

namespace Microsoft.Bot.Samples
{
    public class HelloBot : IBot
    {
        static int minNumber = 1;
        static int maxNumber = 100;
        static int secretNumber = -1;
        static int guessedNumber = 0;

        public async Task OnReceiveActivity(ITurnContext context)
        {
            if (context.Activity.Type is ActivityTypes.ConversationUpdate)
            {
                IConversationUpdateActivity update = context.Activity;
                var requestData = context.Activity;
                var client = new ConnectorClient(new Uri(requestData.ServiceUrl), new MicrosoftAppCredentials("",""));
                if (update.MembersAdded != null && update.MembersAdded.Count > 0)
                {
                    foreach (var newMember in update.MembersAdded)
                    {
                        if (newMember.Id != requestData.Recipient.Id)
                        {
                            var reply = requestData.CreateReply();
                            // reply.Text = $"Welcome {newMember.Name}!";

                            Random rnd = new Random();
                            secretNumber = rnd.Next(minNumber, maxNumber); 

                            reply.Text = $"Guess a number (" + secretNumber + ") between" + minNumber + " and " + maxNumber + ".";
                            await client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
            }
            else if (context.Activity.Type is ActivityTypes.Message)
            {
                var inputMessage = context.Activity.Text;
                var responseText = ProcessMessage(inputMessage);
                await context.SendActivity($"Your Guess:'" + responseText + "'.");

                guessedNumber = Convert.ToInt32(responseText);

                if (guessedNumber == secretNumber)
                    await context.SendActivity($"You guessed right! Secret number is: " + secretNumber + "!");
                else
                {
                    if (guessedNumber > secretNumber)
                    {
                        if (guessedNumber < maxNumber)
                            maxNumber = guessedNumber;

                        await context.SendActivity($"Your guess is too high: " + guessedNumber + "");
                    }
                    else if (guessedNumber < secretNumber)
                    {
                        if (guessedNumber > minNumber)
                            minNumber = guessedNumber;

                        await context.SendActivity($"Your guess is too low: " + guessedNumber + "");
                    }
                    await context.SendActivity($"Guess again between: " + minNumber + " and " + maxNumber + ".");
                }
            }

        }

        private string ProcessMessage(string inputMessage)
        {
            return inputMessage;
        }
    }
}