// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    internal sealed class StringValidator : Validator<string>
    {
        public StringValidator(string name, string value)
          : base(name, value)
        {
        }

        public StringValidator(string name, Uri value)
          : this(name, value?.ToString())
        {
        }

        public void DoesNotContainSeparator()
        {
            if (Value != null && Value.Contains("|"))
                ThrowException($"The value for {Name} should not contain a '|'.");
        }

        public void IsAlphanumeric()
        {
            for (int i = 0; i < Value.Length; i++)
            {
                if (!char.IsLetterOrDigit(Value[i]))
                    ThrowException($"The value for {Name} should should only contain alphanumeric characters.");
            }
        }

        public void IsNotNullOrWhiteSpace()
        {
            if (Value == null)
                ThrowException($"The value for {Name} should not be null.");

            if (Value.Length == 0)
                ThrowException($"The value for {Name} should not be empty.");

            IsNotWhiteSpace();
        }

        public void IsNotLongerThan(int maxLength)
        {
            if (Value.Length > maxLength)
                ThrowException($"The value for {Name} should not be longer than {maxLength} characters.");
        }

        public void IsNotWhiteSpace()
        {
            for (int i = 0; i < Value.Length; i++)
            {
                if (!char.IsWhiteSpace(Value[i]))
                    return;
            }

            ThrowException($"The value for {Name} only contains whitespace.");
        }
    }
}
