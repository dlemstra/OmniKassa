// <copyright file="PaymentRequestValidator.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using System;
using System.Linq;

namespace OmniKassa
{
    internal sealed class PaymentRequestValidator : Validator
    {
        private readonly IPaymentRequest _request;

        private PaymentRequestValidator(IPaymentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _request = request;
        }

        public static void Validate(IPaymentRequest request)
        {
            PaymentRequestValidator validator = new PaymentRequestValidator(request);
            validator.Validate();
        }

        private void Validate()
        {
            ValidateAmount();
            ValidateCaptureDay();
            ValidateCaptureMode();
            ValidateCurrencyCode();
            ValidateExpirationDate();
            ValidateOrderId();
            ValidatePaymentTypes();
            ValidateReturnUrl();
            ValidateTransactionReference();
        }

        private void ValidateCurrencyCode()
        {
            if (_request.CurrencyCode == CurrencyCode.Unknown)
                ThrowException($"The {nameof(_request.CurrencyCode)} should be set.");
        }

        private void ValidateAmount()
        {
            int amount = _request.AmountAsNumber();

            IntValidator validator = new IntValidator(nameof(_request.Amount), amount);
            validator.IsHigherThan(0);
        }

        private void ValidateCaptureDay()
        {
            if (_request.CaptureDay == null)
                return;

            IntValidator validator = new IntValidator(nameof(_request.CaptureDay), _request.CaptureDay.Value);
            validator.IsHigherThan(0);
            validator.IsLowerThan(100);
        }

        private void ValidateCaptureMode()
        {
            if (string.IsNullOrEmpty(_request.CaptureMode))
                return;

            StringValidator validator = new StringValidator(nameof(_request.CaptureMode), _request.CaptureMode);
            validator.IsNotWhiteSpace();
            validator.IsNotLongerThan(20);
            validator.IsAlphanumeric();
        }

        private void ValidateExpirationDate()
        {
            if (_request.ExpirationDate == null)
                return;

            if (_request.ExpirationDate.Value.Kind != DateTimeKind.Utc)
                ThrowException($"The value for {nameof(_request.ExpirationDate)} should use UTC.");

            if (_request.ExpirationDate.Value < DateTime.UtcNow)
                ThrowException($"The value for {nameof(_request.ExpirationDate)} should be in the future.");
        }

        private void ValidateOrderId()
        {
            if (string.IsNullOrEmpty(_request.OrderId))
                return;

            StringValidator validator = new StringValidator(nameof(_request.OrderId), _request.OrderId);
            validator.IsNotWhiteSpace();
            validator.IsNotLongerThan(32);
            validator.IsAlphanumeric();
        }

        private void ValidatePaymentTypes()
        {
            if (_request.PaymentTypes == null)
                return;

            if (_request.PaymentTypes.Count() != _request.PaymentTypes.Distinct().Count())
                ThrowException($"The value for {nameof(_request.PaymentTypes)} should not contain duplicates.");

            if (_request.PaymentTypes.Any(type => type == PaymentType.Unknown))
                ThrowException($"The value for {nameof(_request.PaymentTypes)} should not contain an unknown payment type.");
        }

        private void ValidateReturnUrl()
        {
            StringValidator validator = new StringValidator(nameof(_request.ReturnUrl), _request.ReturnUrl);
            validator.IsNotNullOrWhiteSpace();
            validator.DoesNotContainSeparator();
        }

        private void ValidateTransactionReference()
        {
            StringValidator validator = new StringValidator(nameof(_request.TransactionReference), _request.TransactionReference);
            validator.IsNotNullOrWhiteSpace();
            validator.IsNotLongerThan(32);
            validator.IsAlphanumeric();
        }
    }
}
