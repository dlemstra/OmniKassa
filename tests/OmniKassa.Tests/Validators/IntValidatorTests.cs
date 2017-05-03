// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class IntValidatorTests
    {
        [TestMethod]
        public void IsLowerThan_ValueEqual_ThrowsException()
        {
            IntValidator validator = new IntValidator("test", 1);

            ExceptionAssert.ThrowsLowerThanValidationException("test", 1, () =>
            {
                validator.IsLowerThan(1);
            });
        }

        [TestMethod]
        public void IsLowerThan_ValueIsLower_NoException()
        {
            IntValidator validator = new IntValidator("test", 1);

            validator.IsLowerThan(2);
        }

        [TestMethod]
        public void IsLowerThan_ValueIsHigher_ThrowsException()
        {
            IntValidator validator = new IntValidator("test", 1);

            ExceptionAssert.ThrowsLowerThanValidationException("test", 0, () =>
            {
                validator.IsLowerThan(0);
            });
        }

        [TestMethod]
        public void IsHigherThan_ValueEqual_ThrowsException()
        {
            IntValidator validator = new IntValidator("test", 1);

            ExceptionAssert.ThrowsHigherThanValidationException("test", 1, () =>
            {
                validator.IsHigherThan(1);
            });
        }

        [TestMethod]
        public void IsHigherThan_ValueIsLower_ThrowsException()
        {
            IntValidator validator = new IntValidator("test", 1);

            ExceptionAssert.ThrowsHigherThanValidationException("test", 2, () =>
            {
                validator.IsHigherThan(2);
            });
        }

        [TestMethod]
        public void IsHigherThan_ValueIsHigher_NoException()
        {
            IntValidator validator = new IntValidator("test", 1);

            validator.IsHigherThan(0);
        }
    }
}
