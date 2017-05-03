// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    internal abstract class Validator<TValidatable> : Validator
    {
        protected Validator(string name, TValidatable value)
        {
            Name = name;
            Value = value;
        }

        protected string Name { get; }

        protected TValidatable Value { get; }
    }
}
