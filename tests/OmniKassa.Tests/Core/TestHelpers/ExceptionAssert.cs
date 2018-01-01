// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

#if NETCOREAPP1_1

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    internal static partial class ExceptionAssert
    {
        public static async Task<TException> ThrowsAsync<TException>(string message, Func<Task> action)
           where TException : Exception
        {
            TException exception = await ThrowsAsync<TException>(action);
            return CheckMessage(exception, message);
        }

        public static async Task ThrowsArgumentNullExceptionAsync(string paramName, Func<Task> action)
        {
            ArgumentException exception = await ThrowsAsync<ArgumentNullException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static async Task<TException> ThrowsAsync<TException>(Func<Task> action)
            where TException : Exception
        {
            try
            {
                await action();
                return AssertNotThrown<TException>();
            }
            catch (TException exception)
            {
                return CheckException(exception);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

#endif
