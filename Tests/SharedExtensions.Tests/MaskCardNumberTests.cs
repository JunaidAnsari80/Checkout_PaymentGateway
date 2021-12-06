using NUnit.Framework;
using System;

namespace SharedExtensions.Tests
{
    [TestFixture]
    public class MaskCardNumberTests
    {
        [Test]
        public void ToMask_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            string masked = "************0022";
            string cardNumber = "5295640000000022";

            // Act
            var output = cardNumber.ToMask();

            // Assert
            Assert.AreEqual(output, masked);
        }

        [Test]
        public void ToMask_StateUnderTest_UnexpectedBehavior()
        {
            // Arrange           
            string cardNumber = "";

            // Act Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => { cardNumber.ToMask(); });          
        }
    }
}
