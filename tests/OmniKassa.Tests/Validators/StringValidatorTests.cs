// <copyright file="StringValidatorTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class StringValidatorTests
    {
        [TestMethod]
        public void DoesNotContainSeparator_ValueContainsSeparator_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", "|");

            ExceptionAssert.ThrowsSeparatorValidationException("test", () =>
            {
                validator.DoesNotContainSeparator();
            });
        }

        [TestMethod]
        public void DoesNotContainSeparator_ValueIsValid_NoException()
        {
            StringValidator validator = new StringValidator("test", "test");

            validator.DoesNotContainSeparator();
        }

        [TestMethod]
        public void IsAlphanumeric_ValueIsInvalid_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", "$");

            ExceptionAssert.ThrowsAlphanumericValidationException("test", () =>
            {
                validator.IsAlphanumeric();
            });
        }

        [TestMethod]
        public void IsAlphanumeric_ValueWithAllPossibleValues_NoException()
        {
            StringValidator validator = new StringValidator("test", "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

            validator.IsAlphanumeric();
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace_ValueIsNull_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", (string)null);

            ExceptionAssert.ThrowsNullValidationException("test", () =>
            {
                validator.IsNotNullOrWhiteSpace();
            });
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace_UrlValueIsNull_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", (Uri)null);

            ExceptionAssert.ThrowsNullValidationException("test", () =>
            {
                validator.IsNotNullOrWhiteSpace();
            });
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace_ValueIsEmpty_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", string.Empty);

            ExceptionAssert.ThrowsEmptyValidationException("test", () =>
            {
                validator.IsNotNullOrWhiteSpace();
            });
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace_ValueIsWhiteSpace_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", " ");

            ExceptionAssert.ThrowsWhitespaceValidationException("test", () =>
            {
                validator.IsNotNullOrWhiteSpace();
            });
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace_ValueIsValid_NoException()
        {
            StringValidator validator = new StringValidator("test", "test");

            validator.IsNotNullOrWhiteSpace();
        }

        [TestMethod]
        public void IsNotLongerThan_ValueIsLongerThanMaxLength_ThrowsException()
        {
            StringValidator validator = new StringValidator("test", "12");

            ExceptionAssert.ThrowsLengthValidationException("test", 1, () =>
            {
                validator.IsNotLongerThan(1);
            });
        }

        [TestMethod]
        public void IsNotLongerThan_ValueIsMaxLength_NoException()
        {
            StringValidator validator = new StringValidator("test", "1");

            validator.IsNotLongerThan(1);
        }
    }
}
