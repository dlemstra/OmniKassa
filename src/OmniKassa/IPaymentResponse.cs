// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the response that was received from OmniKassa.
    /// </summary>
    public interface IPaymentResponse
    {
        /// <summary>
        /// Gets the final amount of a transaction (debit or credit) or amount of an operation(refund, cancellation, etc.).
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// Gets the dentifier of the authorisation provided by the acquirer.Configured by the merchant/webshop for manual authorisation.
        /// </summary>
        string AuthorisationId { get; }

        /// <summary>
        /// Gets the number of days after authorisation of a credit card transaction after which automatic validation of the transaction follows.
        /// </summary>
        int? CaptureDay { get; }

        /// <summary>
        /// Gets the mode that can be used to indicate that the user of the Rabo OmniKassa dashboard must manually validate credit card
        /// transactions after the automatic authorisation of this transaction. (This is in contrast to the standard credit card transaction processing
        /// procedure, in which validation is automatic after authorisation.)
        /// </summary>
        string CaptureMode { get; }

        /// <summary>
        /// Gets the currency of the amount.
        /// </summary>
        CurrencyCode CurrencyCode { get; }

        /// <summary>
        /// Gets the version number of the secret key. Can be found on the Rabo OmniKassa Downloadsite.
        /// </summary>
        int KeyVersion { get; }

        /// <summary>
        /// Gets the hidden Primary Account Number.
        /// </summary>
        string MaskedPan { get; }

        /// <summary>
        /// Gets the identifier of the merchant/webshop.
        /// </summary>
        string MerchantId { get; }

        /// <summary>
        /// Gets an open field that can be used to link the identification of the order in the webshop to the payment in the Rabo OmniKassa.
        /// </summary>
        string OrderId { get; }

        /// <summary>
        /// Gets the brand name of payment method the customer has selected.
        /// </summary>
        PaymentBrand PaymentMeanBrand { get; }

        /// <summary>
        /// Gets the response code for a payment request.
        /// </summary>
        ResponseCode ResponseCode { get; }

        /// <summary>
        /// Gets the transaction time. If the payment is sent to the acquirer for authorisation: date/time in the Rabo OmniKassa server
        /// at which the payment is sent to the acquirer, in the merchant/webshop's time zone. Otherwise: date and time at which the Rabo OmniKassa
        /// response codeis generated on the Rabo OmniKassa server.
        /// </summary>
        DateTime? TransactionDateTime { get; }

        /// <summary>
        /// Gets the identifier of the transaction.
        /// </summary>
        string TransactionReference { get; }
    }
}
