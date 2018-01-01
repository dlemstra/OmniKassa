// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class PaymentPostDataValidatorTests
    {
        private const string _expectedSeal = "87403cd1e21565ca6f6c021df4bef5d06204331ed08cf9cec58d0080a392b8ee";

        private readonly KassaConfiguration _configuration = new KassaConfiguration()
        {
            KeyVersion = 1,
            MerchantId = "123456789012345",
            SecretKey = "secret",
            Url = new Uri("https://www.github.com")
        };

        [TestMethod]
        public void Validate_ConfigurationIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                PaymentPostDataValidator.Validate(null, new PaymentPostData());
            });
        }

        [TestMethod]
        public void Validate_PostDataIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("postData", () =>
            {
                PaymentPostDataValidator.Validate(new KassaConfiguration(), null);
            });
        }

        [TestMethod]
        public void Validate_IsValid_NoException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = "data",
                Seal = _expectedSeal,
            };

            PaymentPostDataValidator.Validate(_configuration, postData);
        }

        [TestMethod]
        public void Validate_DataIsNull_ThrowsException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = null,
                Seal = _expectedSeal,
            };

            ExceptionAssert.ThrowsNullValidationException(nameof(postData.Data), () =>
            {
                PaymentPostDataValidator.Validate(_configuration, postData);
            });
        }

        [TestMethod]
        public void Validate_DataIsEmpty_ThrowsException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = string.Empty,
                Seal = _expectedSeal,
            };

            ExceptionAssert.ThrowsEmptyValidationException(nameof(postData.Data), () =>
            {
                PaymentPostDataValidator.Validate(_configuration, postData);
            });
        }

        [TestMethod]
        public void Validate_DataIsWhitespace_ThrowsException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = " ",
                Seal = _expectedSeal,
            };

            ExceptionAssert.ThrowsWhitespaceValidationException(nameof(postData.Data), () =>
            {
                PaymentPostDataValidator.Validate(_configuration, postData);
            });
        }

        [TestMethod]
        public void Validate_InterfaceVersionIsInvalid_ThrowsNoException()
        {
            /*
             * It appears that the api can send a V2 response when our request
             * was V1 so this should not throw an exception.
             */

            PaymentPostData postData = new PaymentPostData()
            {
                Data = "data",
                InterfaceVersion = "HP_1.1",
                Seal = _expectedSeal,
            };

            PaymentPostDataValidator.Validate(_configuration, postData);
        }

        [TestMethod]
        public void Validate_SealIsNull_ThrowsException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = "data",
                Seal = null,
            };

            ExceptionAssert.Throws<InvalidOperationException>($"The seal is invalid.{Environment.NewLine}Expected value: {_expectedSeal}.", () =>
            {
                PaymentPostDataValidator.Validate(_configuration, postData);
            });
        }

        [TestMethod]
        public void Validate_SealIsEmpty_ThrowsException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = "data",
                Seal = string.Empty,
            };

            ExceptionAssert.Throws<InvalidOperationException>($"The seal is invalid.{Environment.NewLine}Expected value: {_expectedSeal}.", () =>
            {
                PaymentPostDataValidator.Validate(_configuration, postData);
            });
        }

        [TestMethod]
        public void Validate_SealIsWhitespace_ThrowsException()
        {
            PaymentPostData postData = new PaymentPostData()
            {
                Data = "data",
                Seal = "   ",
            };

            ExceptionAssert.Throws<InvalidOperationException>($"The seal is invalid.{Environment.NewLine}Expected value: {_expectedSeal}.", () =>
            {
                PaymentPostDataValidator.Validate(_configuration, postData);
            });
        }
    }
}
