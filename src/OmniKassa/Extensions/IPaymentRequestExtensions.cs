// <copyright file="IPaymentRequestExtensions.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System.Diagnostics;

namespace OmniKassa
{
    internal static class IPaymentRequestExtensions
    {
        public static int AmountAsNumber(this IPaymentRequest self)
        {
            Debug.Assert(self != null);

            if (self.CurrencyCode == CurrencyCode.JapaneseYen)
                return (int)self.Amount;
            else
                return (int)(self.Amount * 100);
        }
    }
}
