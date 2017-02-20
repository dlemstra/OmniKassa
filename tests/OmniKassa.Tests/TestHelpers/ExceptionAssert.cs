// <copyright file="ExceptionAssert.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class ExceptionAssert
    {
        public static TException Throws<TException>(string message, Action action)
           where TException : Exception
        {
            TException exception = Throws<TException>(action);
            Assert.IsNotNull(exception.Message);
            if (!exception.Message.StartsWith(message))
                Assert.Fail($"The message `{exception.Message}` does not start with `{message}`.");

            return exception;
        }

        public static void ThrowsArgumentNullException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentNullException>($"Value cannot be null.", action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentException(string paramName, string message, Action action)
        {
            ArgumentException exception = Throws<ArgumentException>(message, action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsAlphanumericValidationException(string name, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should should only contain alphanumeric characters.", action);
        }

        public static void ThrowsEmptyValidationException(string name, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should not be empty.", action);
        }

        public static void ThrowsHigherThanValidationException(string name, int value, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should be higher than {value}.", action);
        }

        public static void ThrowsLengthValidationException(string name, int length, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should not be longer than {length} characters.", action);
        }

        public static void ThrowsLowerThanValidationException(string name, int value, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should be lower than {value}.", action);
        }

        public static void ThrowsOverflowException(Action action)
        {
            Throws<OverflowException>(action);
        }

        public static void ThrowsNullValidationException(string name, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should not be null.", action);
        }

        public static void ThrowsSeparatorValidationException(string name, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} should not contain a '|'.", action);
        }

        public static void ThrowsWhitespaceValidationException(string name, Action action)
        {
            Throws<InvalidOperationException>($"The value for {name} only contains whitespace.", action);
        }

        public static TException Throws<TException>(Action action)
           where TException : Exception
        {
            try
            {
                action();
                Assert.Fail("Exception of type {0} was not thrown.", typeof(TException).Name);
                return null;
            }
            catch (TException exception)
            {
                Type type = exception.GetType();
                if (type != typeof(TException))
                    Assert.Fail("Exception of type {0} was not thrown an exception of type {1} was thrown.", typeof(TException).Name, type.Name);

                return exception;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
