// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Bot.Schema
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A receipt card
    /// </summary>
    public partial class ReceiptCard
    {
        /// <summary>
        /// Initializes a new instance of the ReceiptCard class.
        /// </summary>
        public ReceiptCard()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ReceiptCard class.
        /// </summary>
        /// <param name="title">Title of the card</param>
        /// <param name="items">Array of Receipt Items</param>
        /// <param name="facts">Array of Fact Objects   Array of key-value
        /// pairs.</param>
        /// <param name="tap">This action will be activated when user taps on
        /// the card</param>
        /// <param name="total">Total amount of money paid (or should be
        /// paid)</param>
        /// <param name="tax">Total amount of TAX paid(or should be
        /// paid)</param>
        /// <param name="vat">Total amount of VAT paid(or should be
        /// paid)</param>
        /// <param name="buttons">Set of actions applicable to the current
        /// card</param>
        public ReceiptCard(string title = default(string), IList<ReceiptItem> items = default(IList<ReceiptItem>), IList<Fact> facts = default(IList<Fact>), CardAction tap = default(CardAction), string total = default(string), string tax = default(string), string vat = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            Title = title;
            Items = items;
            Facts = facts;
            Tap = tap;
            Total = total;
            Tax = tax;
            Vat = vat;
            Buttons = buttons;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets title of the card
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets array of Receipt Items
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public IList<ReceiptItem> Items { get; set; }

        /// <summary>
        /// Gets or sets array of Fact Objects   Array of key-value pairs.
        /// </summary>
        [JsonProperty(PropertyName = "facts")]
        public IList<Fact> Facts { get; set; }

        /// <summary>
        /// Gets or sets this action will be activated when user taps on the
        /// card
        /// </summary>
        [JsonProperty(PropertyName = "tap")]
        public CardAction Tap { get; set; }

        /// <summary>
        /// Gets or sets total amount of money paid (or should be paid)
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets total amount of TAX paid(or should be paid)
        /// </summary>
        [JsonProperty(PropertyName = "tax")]
        public string Tax { get; set; }

        /// <summary>
        /// Gets or sets total amount of VAT paid(or should be paid)
        /// </summary>
        [JsonProperty(PropertyName = "vat")]
        public string Vat { get; set; }

        /// <summary>
        /// Gets or sets set of actions applicable to the current card
        /// </summary>
        [JsonProperty(PropertyName = "buttons")]
        public IList<CardAction> Buttons { get; set; }

    }
}
