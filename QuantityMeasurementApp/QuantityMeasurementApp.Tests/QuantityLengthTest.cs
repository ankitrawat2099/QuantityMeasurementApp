using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests;
    // Unit tests for the QuantityLength class
    [TestClass]
    public class QuantityLengthTest
    {
        //Testing equality of two lengths in the same unit and same value should return true
        [TestMethod]
        public void TestEquality_FEETToFEET_SameValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in the same unit but different values should return false
        [TestMethod]
        public void TestEquality_INCHESToINCHES_SameValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.INCHES);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.INCHES);
            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_FEETToINCHES_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);
            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in the same unit but different values should return false
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.FEET);
            Assert.IsFalse(q1.Equals(q2));
        }

        //Testing equality of a QuantityLength object with null should return false
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.IsFalse(q1.Equals(null));
        }

        //Testing equality of the same reference should return true
        [TestMethod]
        public void TestEquality_SameReference()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.IsTrue(q1.Equals(q1));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_YARDSToFEET_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength q2 = new QuantityLength(3.0, LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_YARDSToINCHES_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength q2 = new QuantityLength(36.0, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in different units but equivalent values should return true
        [TestMethod]
        public void TestEquality_CENTIMETERSToINCHES_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            QuantityLength q2 = new QuantityLength(0.393701, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        //Testing equality of two lengths in the same unit but different values should return false
        [TestMethod]
        public void TestEquality_YARDSToYARDS_DifferentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.YARDS);

            Assert.IsFalse(q1.Equals(q2));
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_RoundTrip()
        {
            double original = 5.0;

            double toYARDS = QuantityLength.Convert(original, LengthUnit.FEET, LengthUnit.YARDS);
            double backToFEET = QuantityLength.Convert(toYARDS, LengthUnit.YARDS, LengthUnit.FEET);

            Assert.AreEqual(original, backToFEET, 0.000001);
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.AreEqual(-12.0, result, 0.000001);
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.AreEqual(0.0, result, 0.000001);
        }

        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_CENTIMETERSToINCHES()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.CENTIMETERS, LengthUnit.INCHES);
            Assert.AreEqual(1.0, result, 0.0001);
        }
        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_YARDSsToFEET()
        {
            double result = QuantityLength.Convert(3.0, LengthUnit.YARDS, LengthUnit.FEET);
            Assert.AreEqual(9.0, result, 0.000001);
        }
        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_INCHESesToFEET()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.INCHES, LengthUnit.FEET);
            Assert.AreEqual(2.0, result, 0.000001);
        }
        //Testing conversion of a value from one unit to the same unit should return the original value
        [TestMethod]
        public void TestConversion_FEETToINCHESes()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.AreEqual(12.0, result, 0.000001);
        }

        //Testing addition of two lengths where one operand is null
        [TestMethod]
        public void TestAddition_NullSecondOperand()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
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
            QuantityLength q1 = new QuantityLength(5.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(-2.0, LengthUnit.FEET);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.FEET)));
        }
        //Testing addition of zero to a length
        [TestMethod]
        public void TestAddition_WithZero()
        {
            QuantityLength q1 = new QuantityLength(5.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(0.0, LengthUnit.INCHES);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(5.0, LengthUnit.FEET)));
        }
        //Testing addition of two lengths in different units
        [TestMethod]
        public void TestAddition_CrossUnit_INCHESPlusFEET()
        {
            QuantityLength q1 = new QuantityLength(12.0, LengthUnit.INCHES);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.FEET);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(24.0, LengthUnit.INCHES)));
        }
        //Testing addition of two lengths in different units
        [TestMethod]
        public void TestAddition_CrossUnit_FEETPlusINCHES()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.FEET)));
        }
        //Testing addition of two lengths in the same unit
        [TestMethod]
        public void TestAddition_SameUnit_FEETPlusFEET()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.FEET);

            QuantityLength result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityLength(3.0, LengthUnit.FEET)));
        }
        //Testing addition of two lengths in the same unit
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_FEET()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.FEET);

            Assert.IsTrue(result.Equals(new QuantityLength(2.0, LengthUnit.FEET)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_INCHESes()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.INCHES);

            Assert.IsTrue(result.Equals(new QuantityLength(24.0, LengthUnit.INCHES)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_YARDSs()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.YARDS);

            Assert.IsTrue(result.Equals(new QuantityLength(0.666666, LengthUnit.YARDS)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_CENTIMETERS()
        {
            QuantityLength q1 = new QuantityLength(2.54, LengthUnit.CENTIMETERS);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.INCHES);

            QuantityLength result =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.CENTIMETERS);

            Assert.IsTrue(result.Equals(new QuantityLength(5.08, LengthUnit.CENTIMETERS)));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_Commutativity()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            QuantityLength r1 =
                QuantityLength.AddTwoUnits_TargetUnit(q1, q2, LengthUnit.YARDS);

            QuantityLength r2 =
                QuantityLength.AddTwoUnits_TargetUnit(q2, q1, LengthUnit.YARDS);

            Assert.IsTrue(r1.Equals(r2));
        }
        //Testing addition of two lengths with an invalid target unit should throw an exception
        [TestMethod]
        public void TestAddition_ExplicitTargetUnit_InvalidTarget()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.FEET);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.INCHES);

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


        // ====================================================================================
        //Testing for WeightUnit and QuantityWeight classes

        [TestMethod]
        public void testEquality_KilogramToKilogram_SameValue()
        {
            QuantityWeight q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            QuantityWeight q2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_KilogramToGram_EquivalentValue()
        {
            QuantityWeight q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            QuantityWeight q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_GramToKilogram_EquivalentValue()
        {
            QuantityWeight q1 = new QuantityWeight(1000.0, WeightUnit.GRAM);
            QuantityWeight q2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

            Assert.IsTrue(q1.Equals(q2));
        }
        [TestMethod]
        public void testConversion_KilogramToGram()
        {
            QuantityWeight q = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

            QuantityWeight result = q.ConvertTo(WeightUnit.GRAM);

            Assert.IsTrue(result.Equals(new QuantityWeight(1000.0, WeightUnit.GRAM)));
        }

        [TestMethod]
        public void testConversion_PoundToKilogram()
        {
            QuantityWeight q = new QuantityWeight(2.20462, WeightUnit.POUND);

            QuantityWeight result = q.ConvertTo(WeightUnit.KILOGRAM);

            Assert.IsTrue(result.Equals(new QuantityWeight(1.0, WeightUnit.KILOGRAM)));
        }
        [TestMethod]
        public void testAddition_SameUnit_KilogramPlusKilogram()
        {
            QuantityWeight q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            QuantityWeight q2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);

            QuantityWeight result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityWeight(3.0, WeightUnit.KILOGRAM)));
        }

        [TestMethod]
        public void testAddition_CrossUnit_KilogramPlusGram()
        {
            QuantityWeight q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            QuantityWeight q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

            QuantityWeight result = q1.Add(q2);

            Assert.IsTrue(result.Equals(new QuantityWeight(2.0, WeightUnit.KILOGRAM)));
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Kilogram()
        {
            QuantityWeight q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            QuantityWeight q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

            QuantityWeight result = q1.Add(q2, WeightUnit.GRAM);

            Assert.IsTrue(result.Equals(new QuantityWeight(2000.0, WeightUnit.GRAM)));
        }


    }
