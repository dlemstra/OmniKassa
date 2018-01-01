// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    internal abstract class Validator
    {
        protected static void ThrowException(string message)
        {
            throw new InvalidOperationException(message);
        }

        protected static void ThrowException(string message, string expectedValue, string actualValue)
        {
            throw new InvalidOperationException($"{message}{Environment.NewLine}Expected value: {expectedValue}.{Environment.NewLine}Actual value: {actualValue}.");
        }
    }
}
