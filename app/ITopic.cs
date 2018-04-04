using System.Threading.Tasks;
using NumberChallenge.Models;

namespace NumberChallenge
{
    public interface ITopic
    {
        /// <summary>
        /// Name of the topic
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Called when topic starts
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> StartTopic(NumberBotContext context);

        /// <summary>
        /// called while topic active
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> ContinueTopic(NumberBotContext context);

        /// <summary>
        ///  Called when a topic is resumed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> ResumeTopic(NumberBotContext context);
    }
}