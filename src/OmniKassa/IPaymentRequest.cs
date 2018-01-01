// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the request that will be send to the OmniKassa.
    /// </summary>
    public interface IPaymentRequest
    {
        /// <summary>
        /// Gets the final amount of a transaction (debit or credit) or amount of an operation(refund, cancellation, etc.).
        /// [Required]
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// Gets the address the Rabo OmniKassa server will automatically notify with the current status after a payment or process.
        /// [Recommended to be included in the payment request for optimal reconciliation]
        /// </summary>
        Uri AutomaticResponseUrl { get; }

        /// <summary>
        /// Gets the number of days after authorisation of a credit card transaction after which automatic validation of the transaction follows.
        /// [Optional]
        /// </summary>
        int? CaptureDay { get; }

        /// <summary>
        /// Gets the mode that can be used to indicate that the user of the Rabo OmniKassa dashboard must manually validate credit card
        /// transactions after the automatic authorisation of this transaction. (This is in contrast to the standard credit card transaction processing
        /// procedure, in which validation is automatic after authorisation.)
        /// [Optional]
        /// </summary>
        string CaptureMode { get; }

        /// <summary>
        /// Gets the currency of the amount.
        /// [Required]
        /// </summary>
        CurrencyCode CurrencyCode { get; }

        /// <summary>
        /// Gets the expiration date of the payment request.
        /// [Optional]
        /// </summary>
        DateTime? ExpirationDate { get; }

        /// <summary>
        /// Gets an open field that can be used to link the identification of the order in the webshop to the payment in the Rabo OmniKassa.
        /// [Recommended to be included in the payment request for optimal reconciliation]
        /// </summary>
        string OrderId { get; }

        /// <summary>
        /// Gets the language of the customer; used for presentation to customers on the Rabo OmniKassa payment page and other pages.
        /// [Optional]
        /// </summary>
        LanguageCode? Language { get; }

        /// <summary>
        /// Gets a list of payment methods from which the customer can choose. If using the register services INCASSO(direct debit),
        /// ACCEPTGIRO(giro collection form) and REMBOURS(cash on delivery), these payment methods must always be included in
        /// the list. The order of names in this field determines the order the methods are presented to your customer.
        /// [Optional]
        /// </summary>
        IEnumerable<PaymentBrand> PaymentBrands { get; }

        /// <summary>
        /// Gets the page to which the customer is redirected after payment and where the Rabo OmniKassa server sends the manual response message.
        /// [Required]
        /// </summary>
        Uri ReturnUrl { get; }

        /// <summary>
        /// Gets the identifier of the transaction.
        /// [Required]
        /// </summary>
        string TransactionReference { get; }
    }
}