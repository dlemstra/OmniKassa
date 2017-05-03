// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the request that will be send to the OmniKassa.
    /// </summary>
    public sealed class PaymentRequest : IPaymentRequest
    {
        /// <summary>
        /// Gets or sets the final amount of a transaction (debit or credit) or amount of an operation(refund, cancellation, etc.).
        /// [Required]
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the address the Rabo OmniKassa server will automatically notify with the current status after a payment or process.
        /// [Recommended to be included in the payment request for optimal reconciliation]
        /// </summary>
        public Uri AutomaticResponseUrl { get; set; }

        /// <summary>
        /// Gets or sets the number of days after authorisation of a credit card transaction after which automatic validation of the transaction follows.
        /// [Optional]
        /// </summary>
        public int? CaptureDay { get; set; }

        /// <summary>
        /// Gets or sets the currency of the amount.
        /// [Required]
        /// </summary>
        public CurrencyCode CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the mode that can be used to indicate that the user of the Rabo OmniKassa dashboard must manually validate credit card
        /// transactions after the automatic authorisation of this transaction. (This is in contrast to the standard credit card transaction processing
        /// procedure, in which validation is automatic after authorisation.)
        /// [Optional]
        /// </summary>
        public string CaptureMode { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the payment request.
        /// [Optional]
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets an open field that can be used to link the identification of the order in the webshop to the payment in the Rabo OmniKassa.
        /// [Recommended to be included in the payment request for optimal reconciliation]
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the language of the customer; used for presentation to customers on the Rabo OmniKassa payment page and other pages.
        /// [Optional]
        /// </summary>
        public LanguageCode? Language { get; set; }

        /// <summary>
        /// Gets or sets a list of payment methods from which the customer can choose. If using the register services INCASSO (direct debit),
        /// ACCEPTGIRO (giro collection form) and REMBOURS (cash on delivery), these payment methods must always be included in
        /// the list. The order of names in this field determines the order the methods are presented to your customer.
        /// [Optional]
        /// </summary>
        public IEnumerable<PaymentBrand> PaymentBrands { get; set; }

        /// <summary>
        /// Gets or sets the page to which the customer is redirected after payment and where the Rabo OmniKassa server sends the manual response message.
        /// [Required]
        /// </summary>
        public Uri ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the transaction.
        /// [Required]
        /// </summary>
        public string TransactionReference { get; set; }
    }
}
