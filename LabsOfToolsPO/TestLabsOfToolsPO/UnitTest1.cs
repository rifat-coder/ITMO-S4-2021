using System;
using LabsOfToolsPO;
using Xunit;

namespace TestLabsOfToolsPO
{
    public class UnitTest1
    {

        [Fact]
        public void Add_5Plus2_Returned7()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategyAdd());

            double arg1 = 5;
            double arg2 = 2;
            double expected = 7;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void Add_5Subtract2_Returned3()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategySubtract());

            double arg1 = 5;
            double arg2 = 2;
            double expected = 3;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void Add_5Subtract7_Returned_Minus2()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategySubtract());

            double arg1 = 5;
            double arg2 = 7;
            double expected = -2;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void Add_33Multiply2_Returned66()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategyMultiply());

            double arg1 = 33;
            double arg2 = 2;
            double expected = 66;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void Add_10Division2_Returned5()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategyDivision());

            double arg1 = 10;
            double arg2 = 2;
            double expected = 5;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }

        /*
        [Fact]
        public void Add_5Division0_ReturnedErr()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategyDivision());

            double arg1 = 5;
            double arg2 = 0;
            double expected = 0;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }
        */

        [Fact]
        public void Add_100Percent30_Returned30()
        {
            // Arrange 
            var MiniCalculator = new ContextCalculate(new StrategyPercent());

            double arg1 = 100;
            double arg2 = 30;
            double expected = 30;

            // Act
            double result = MiniCalculator.Making(arg1, arg2);

            // Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CalculateLine_10Add2Div6Mult8Sub1_Returned15()
        {
            // Arrange
            var MiniCalculator = new Calculator("/Users/rifat/Desktop/LabsOfToolsPO/TestText.txt");
            string expected = "10 + 2 / 6 * 8 - 1 = 15";

            // Act
            string result = MiniCalculator.WorkingWithLine("10 + 2 / 6 * 8 - 1");

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
