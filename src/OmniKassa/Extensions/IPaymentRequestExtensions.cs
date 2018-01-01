// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    internal static class IPaymentRequestExtensions
    {
        public static int AmountAsNumber(this IPaymentRequest self)
        {
            if (self.CurrencyCode == CurrencyCode.JapaneseYen)
                return (int)self.Amount;
            else
                return (int)(self.Amount * 100);
        }
    }
}
