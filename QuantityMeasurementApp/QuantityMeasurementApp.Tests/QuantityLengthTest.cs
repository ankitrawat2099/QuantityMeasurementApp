using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests;
    // Unit tests for the QuantityLength class
    [TestClass]
    public class QuantityLengthTest
    {
        //Testing equality of two lengths in the same unit and same value should return true
        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in the same unit but different values should return false
        [TestMethod]
        public void TestEquality_InchToInch_SameValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_FeetToInch_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in the same unit but different values should return false
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(q1.Equals(q2));
        }

        //Testing equality of a QuantityLength object with null should return false
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(q1.Equals(null));
        }

        //Testing equality of the same reference should return true
        [TestMethod]
        public void TestEquality_SameReference()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q1));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_YardToFeet_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(3.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_YardToInch_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(36.0, LengthUnit.Inch);

            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_CentimeterToInch_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength q2 = new QuantityLength(0.393701, LengthUnit.Inch);

            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in the same unit but different values should return false
        [TestMethod]
        public void TestEquality_YardToYard_DifferentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.Yard);

            Assert.IsFalse(q1.Equals(q2));
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_RoundTrip()
        {
            double original = 5.0;

            double toYard = QuantityLength.Convert(original, LengthUnit.Feet, LengthUnit.Yard);
            double backToFeet = QuantityLength.Convert(toYard, LengthUnit.Yard, LengthUnit.Feet);

            Assert.AreEqual(original, backToFeet, 0.000001);
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(-12.0, result, 0.000001);
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(0.0, result, 0.000001);
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_CentimeterToInch()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.Centimeter, LengthUnit.Inch);
            Assert.AreEqual(1.0, result, 0.0001);
        }
        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_YardsToFeet()
        {
            double result = QuantityLength.Convert(3.0, LengthUnit.Yard, LengthUnit.Feet);
            Assert.AreEqual(9.0, result, 0.000001);
        }
        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, 0.000001);
        }
        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_FeetToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(12.0, result, 0.000001);
        }

        //Testing addition of two lengths where one operand is null
        [TestMethod]
        public void TestAddition_NullSecondOperand()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            try
            {
                q1.Add(null);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                // Test passes
            }
        }

        //Testing addition of two lengths where one has a negative value
        [TestMethod]
        public void TestAddition_NegativeValue()
        {
            QuantityLength q1 = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(-2.0, LengthUnit.Feet);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.Feet)));
        }
        //Testing addition of zero to a length
        [TestMethod]
        public void TestAddition_WithZero()
        {
            QuantityLength q1 = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(0.0, LengthUnit.Inch);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(5.0, LengthUnit.Feet)));
        }
        //Testing addition of two lengths in different units
        [TestMethod]
        public void TestAddition_CrossUnit_InchPlusFeet()
        {
            QuantityLength q1 = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.Feet);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(24.0, LengthUnit.Inch)));
        }
        //Testing addition of two lengths in different units
        [TestMethod]
        public void TestAddition_CrossUnit_FeetPlusInch()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.Feet)));
        }
        //Testing addition of two lengths in the same unit
        [TestMethod]
        public void TestAddition_SameUnit_FeetPlusFeet()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.Feet);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.Feet)));
        }
        //Testing addition of two lengths in the same unit
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_Feet()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.Feet);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.Feet)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_Inches()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.Inch);

            Assert.IsTrue(result.Equals(new QuantityLength(24.0, LengthUnit.Inch)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_Yards()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.Yard);

            Assert.IsTrue(result.Equals(new QuantityLength(0.666666, LengthUnit.Yard)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_Centimeter()
        {
            QuantityLength q1 = new QuantityLength(2.54, LengthUnit.Centimeter);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.Inch);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.Centimeter);

            Assert.IsTrue(result.Equals(new QuantityLength(5.08, LengthUnit.Centimeter)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_Commutativity()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);

            QuantityLength r1 =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.Yard);

            QuantityLength r2 =
                QuantityLength.AddTwoUnits_TargetUnit(q2, q1, LengthUnit.Yard);

            Assert.IsTrue(r1.Equals(r2));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_InvalidTarget()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);

            try
            {
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, (LengthUnit)999);
                Assert.Fail("Expected ArgumentException not thrown");
            }
            catch (ArgumentException)
            {
                // pass
            }
        }



    }
