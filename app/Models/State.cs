
using System.Collections.Generic;
using Microsoft.Bot.Builder.Core.Extensions;

namespace NumberChallenge.Models
{
    /// <summary>
    /// Object persisted as conversation state
    /// </summary>
    public class ConversationData : StoreItem
    {
        public ITopic ActiveTopic { get; set; }
    }

    /// <summary>
    /// Object persisted as user state
    /// </summary>
    public class UserData : StoreItem
    {
        public IList<Game> Games { get; set; }
        public IList<Guess> Guesses { get; set; }
    }
}