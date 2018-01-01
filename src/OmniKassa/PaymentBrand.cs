﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the payment brands.
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
