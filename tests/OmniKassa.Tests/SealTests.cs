// <copyright file="SealTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OmniKassa.Tests
{
    [TestClass]
    public class SealTests
    {
        [TestMethod]
        public void Create_ValidData_ReturnsHashedString()
        {
            string data = nameof(data);
            string secretKey = nameof(secretKey);

            string hash = Seal.Create(data, secretKey);

            Assert.AreEqual("783d75107b3ce5d2a7a6dc6f8d311edd5154992b625f870965e5c7c543e91ba5", hash);
        }
    }
}
