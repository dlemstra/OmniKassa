// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests.Validators
{
    [TestClass]
    public class PaymentRequestValidatorTests
    {
        [TestMethod]
        public void Validate_RequestIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("request", () =>
            {
                PaymentRequestValidator.Validate(null);
            });
        }

        [TestMethod]
        public void Validate_IsValid_NoException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234"
            };

            PaymentRequestValidator.Validate(request);
        }

        [TestMethod]
        public void Validate_AmountInYenIsLessThanIntMinValue_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = ((decimal)int.MaxValue) + 1,
                CurrencyCode = CurrencyCode.JapaneseYen,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsOverflowException(() =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_AmountInYenIsMoreThanMaxMaxValue_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = ((decimal)int.MinValue) - 1,
                CurrencyCode = CurrencyCode.JapaneseYen,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsOverflowException(() =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_AmountIsLessThanIntMinValue_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = (int.MaxValue / 100.0m) + 1,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsOverflowException(() =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_AmountIsMoreThanMaxMaxValue_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = (int.MinValue / 100.0m) - 1,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsOverflowException(() =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_AmountIsZero_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 0,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsHigherThanValidationException(nameof(request.Amount), 0, () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_CaptureDayIsZero_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                CaptureDay = 0
            };

            ExceptionAssert.ThrowsHigherThanValidationException(nameof(request.CaptureDay), 0, () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_CaptureDayIsMoreThan99_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                CaptureDay = 100
            };

            ExceptionAssert.ThrowsLowerThanValidationException(nameof(request.CaptureDay), 100, () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_CaptureModeIsWhitespace_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                CaptureMode = "    "
            };

            ExceptionAssert.ThrowsWhitespaceValidationException(nameof(request.CaptureMode), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_CaptureModeIsLongerThanMaxLength_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                CaptureMode = "123456789012345678901"
            };

            ExceptionAssert.ThrowsLengthValidationException(nameof(request.CaptureMode), 20, () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_CaptureModeHasNonAlphanumericCharacter_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                CaptureMode = "$"
            };

            ExceptionAssert.ThrowsAlphanumericValidationException(nameof(request.CaptureMode), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_CurrencyCodeIsNotSet_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
            };

            ExceptionAssert.Throws<InvalidOperationException>("The CurrencyCode should be set.", () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_ExpirationDateIsNotUTC_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                ExpirationDate = DateTime.Now
            };

            ExceptionAssert.Throws<InvalidOperationException>("The value for ExpirationDate should use UTC.", () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_ExpirationDateIsInThePast_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                ExpirationDate = DateTime.UtcNow.AddSeconds(-1)
            };

            ExceptionAssert.Throws<InvalidOperationException>("The value for ExpirationDate should be in the future.", () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_OrderIdIsWhitespace_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                OrderId = "    "
            };

            ExceptionAssert.ThrowsWhitespaceValidationException(nameof(request.OrderId), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_OrderIdIsLongerThanMaxLength_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                OrderId = "123456789012345678901234567890123"
            };

            ExceptionAssert.ThrowsLengthValidationException(nameof(request.OrderId), 32, () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_OrderIdIsThanMaxLength_NoException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                OrderId = "12345678901234567890123456789012"
            };

            PaymentRequestValidator.Validate(request);
        }

        [TestMethod]
        public void Validate_OrderIdHasNonAlphanumericCharacter_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                OrderId = "|"
            };

            ExceptionAssert.ThrowsAlphanumericValidationException(nameof(request.OrderId), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_PaymentBrandsContainsDuplicates_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                PaymentBrands = new PaymentBrand[] { PaymentBrand.VISA, PaymentBrand.VISA }
            };

            ExceptionAssert.Throws<InvalidOperationException>($"The value for {nameof(request.PaymentBrands)} should not contain duplicates.", () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_PaymentBrandsContainsUnknown_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "1234",
                PaymentBrands = new PaymentBrand[] { PaymentBrand.VISA, PaymentBrand.Unknown }
            };

            ExceptionAssert.Throws<InvalidOperationException>($"The value for {nameof(request.PaymentBrands)} should not contain an unknown payment brand.", () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_ReturnUrlIsNull_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = null,
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsNullValidationException(nameof(request.ReturnUrl), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_ReturnUrlContainsSeparator_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://github.com/?a=|"),
                TransactionReference = "1234"
            };

            ExceptionAssert.ThrowsSeparatorValidationException(nameof(request.ReturnUrl), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_TransactionReferenceIsWhitespace_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "    "
            };

            ExceptionAssert.ThrowsWhitespaceValidationException(nameof(request.TransactionReference), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_TransactionReferenceIsLongerThanMaxLength_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "123456789012345678901234567890123"
            };

            ExceptionAssert.ThrowsLengthValidationException(nameof(request.TransactionReference), 32, () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }

        [TestMethod]
        public void Validate_TransactionReferenceIsThanMaxLength_NoException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "12345678901234567890123456789012"
            };

            PaymentRequestValidator.Validate(request);
        }

        [TestMethod]
        public void Validate_TransactionReferenceHasNonAlphanumericCharacter_ThrowsException()
        {
            PaymentRequest request = new PaymentRequest()
            {
                Amount = 15.95m,
                CurrencyCode = CurrencyCode.Euro,
                ReturnUrl = new Uri("https://www.github.com"),
                TransactionReference = "&"
            };

            ExceptionAssert.ThrowsAlphanumericValidationException(nameof(request.TransactionReference), () =>
            {
                PaymentRequestValidator.Validate(request);
            });
        }
    }
}
