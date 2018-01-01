// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa.Tests
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
    internal sealed class ExcludeFromCodeCoverageAttribute : Attribute
    {
    }
}
