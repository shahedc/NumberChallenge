﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Recognizers.Text.DateTime;

namespace NumberChallenge.Models
{
    public class NumberBotContext : TurnContextWrapper
    {
        public NumberBotContext(ITurnContext context) : base(context)
        {
        }
        
        public ConversationData ConversationState
        {
            get
            {
                return ConversationState<ConversationData>.Get(this);
            }
        }
        
        public UserData UserState
        {
            get
            {
                return UserState<UserData>.Get(this);
            }
        }

        public IRecognizedIntents RecognizedIntents { get { return this.Services.Get<IRecognizedIntents>(); } }

        public IList<DateTime> GetDateTimes()
        {
            IList<DateTime> times = new List<DateTime>();
            // Get DateTime model for English
            var model = new DateTimeRecognizer(this.Activity.Locale ?? "en-us").GetDateTimeModel();
            var results = model.Parse(this.Activity.Text);

            // Check there are valid results
            if (results.Any() && results.First().TypeName.StartsWith("datetimeV2"))
            {
                // The DateTime model can return several resolution types (https://github.com/Microsoft/Recognizers-Text/blob/master/.NET/Microsoft.Recognizers.Text.DateTime/Constants.cs#L7-L14)
                // We only care for those with a date, date and time, or date time period:
                // date, daterange, datetime, datetimerange

                return results.Where(result =>
                {
                    var subType = result.TypeName.Split('.').Last();
                    return (subType.Contains("date") || subType.Contains("time")) && !subType.Contains("range");
                })
                .Select(result =>
                {
                    var resolutionValues = (IList<Dictionary<string, string>>)result.Resolution["values"];
                    return resolutionValues.Select(v => DateTime.Parse(v["value"]));
                }).SelectMany(l => l).ToList();
            }
            return times;
        }

    }
}