using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests;
    [TestClass]
    public class QuantityLengthTest
    {
        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_InchToInch_SameValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength q2 = new QuantityLength(1.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_FeetToInch_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(q1.Equals(null));
        }

        [TestMethod]
        public void TestEquality_SameReference()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q1));
        }
        [TestMethod]
        public void TestEquality_YardToFeet_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(3.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_YardToInch_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(36.0, LengthUnit.Inch);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_CentimeterToInch_EquivalentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength q2 = new QuantityLength(0.393701, LengthUnit.Inch);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_YardToYard_DifferentValue()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength q2 = new QuantityLength(2.0, LengthUnit.Yard);

            Assert.IsFalse(q1.Equals(q2));
        }
        [TestMethod]
        public void TestConversion_RoundTrip()
        {
            double original = 5.0;

            double toYard = QuantityLength.Convert(original, LengthUnit.Feet, LengthUnit.Yard);
            double backToFeet = QuantityLength.Convert(toYard, LengthUnit.Yard, LengthUnit.Feet);

            Assert.AreEqual(original, backToFeet, 0.000001);
        }
        [TestMethod]
        public void TestConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(-12.0, result, 0.000001);
        }
        [TestMethod]
        public void TestConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(0.0, result, 0.000001);
        }
        [TestMethod]
        public void TestConversion_CentimeterToInch()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.Centimeter, LengthUnit.Inch);
            Assert.AreEqual(1.0, result, 0.0001);
        }
        [TestMethod]
        public void TestConversion_YardsToFeet()
        {
            double result = QuantityLength.Convert(3.0, LengthUnit.Yard, LengthUnit.Feet);
            Assert.AreEqual(9.0, result, 0.000001);
        }
        [TestMethod]
        public void TestConversion_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, 0.000001);
        }
        [TestMethod]
        public void TestConversion_FeetToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(12.0, result, 0.000001);
        }
    }
