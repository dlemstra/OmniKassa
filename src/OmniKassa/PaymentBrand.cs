// <copyright file="PaymentBrand.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the the payment brands.
    /// </summary>
    public enum PaymentBrand
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// ACCEPTGIRO (giro collection form)
        /// </summary>
        ACCEPTGIRO,

        /// <summary>
        /// BCMC
        /// </summary>
        BCMC,

        /// <summary>
        /// IDEAL
        /// </summary>
        IDEAL,

        /// <summary>
        /// INCASSO (direct debit)
        /// </summary>
        INCASSO,

        /// <summary>
        /// MAESTRO
        /// </summary>
        MAESTRO,

        /// <summary>
        /// MASTERCARD
        /// </summary>
        MASTERCARD,

        /// <summary>
        /// REMBOURS (cash on delivery)
        /// </summary>
        REMBOURS,

        /// <summary>
        /// VISA
        /// </summary>
        VISA,

        /// <summary>
        /// VPAY
        /// </summary>
        VPAY,
    }
}
