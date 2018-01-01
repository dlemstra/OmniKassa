﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    internal sealed class IntValidator : Validator<int>
    {
        public IntValidator(string name, int value)
          : base(name, value)
        {
        }

        public void IsLowerThan(int value)
        {
            if (Value >= value)
                ThrowException($"The value for {Name} should be lower than {value}.");
        }

        public void IsHigherThan(int value)
        {
            if (Value <= value)
                ThrowException($"The value for {Name} should be higher than {value}.");
        }
    }
}
