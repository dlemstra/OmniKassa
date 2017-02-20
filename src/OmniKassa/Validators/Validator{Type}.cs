// <copyright file="Validator{Type}.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

namespace OmniKassa
{
    internal abstract class Validator<Type> : Validator
    {
        protected Validator(string name, Type value)
        {
            Name = name;
            Value = value;
        }

        protected string Name { get; }

        protected Type Value { get; }
    }
}
