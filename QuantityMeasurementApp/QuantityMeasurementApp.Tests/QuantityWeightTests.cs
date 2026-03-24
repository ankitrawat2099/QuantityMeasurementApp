// ======================= QuantityWeightTests.cs =======================
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests;

[TestClass]
public class QuantityWeightTests
{
    private QuantityService _service;

    [TestInitialize]
    public void Setup(){
         _service = new QuantityService();
    }

    // -------------------- Equality Tests --------------------

    [TestMethod]
     public void TestEquality_KilogramToKilogram_SameValue()
      {
         Assert.IsTrue(_service.Compare(new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM), new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM)));
          }
    [TestMethod]
     public void TestEquality_KilogramToGram_EquivalentValue()
      {
         Assert.IsTrue(_service.Compare(new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM), new QuantityModel<WeightUnit>(1000.0, WeightUnit.GRAM)));
          }
    [TestMethod]
     public void TestEquality_GramToKilogram_EquivalentValue()
      {
         Assert.IsTrue(_service.Compare(new QuantityModel<WeightUnit>(1000.0, WeightUnit.GRAM), new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM)));
          }


    // -------------------- Conversion Tests --------------------

    [TestMethod]
     public void TestConversion_KilogramToGram()
      {
         var res = _service.Convert(new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM), WeightUnit.GRAM);
          Assert.AreEqual(1000.0, res.Value, 0.000001); 
         Assert.AreEqual(WeightUnit.GRAM, res.Unit);
          }
    [TestMethod]
     public void TestConversion_PoundToKilogram() 
     {
         var res = _service.Convert(new QuantityModel<WeightUnit>(2.20462, WeightUnit.POUND), WeightUnit.KILOGRAM);
          Assert.AreEqual(1.0, res.Value, 0.00001);
           Assert.AreEqual(WeightUnit.KILOGRAM, res.Unit); 
           }

    // -------------------- Addition Tests --------------------

    [TestMethod] 
    public void TestAddition_SameUnit_KilogramPlusKilogram() 
    {
         var res = _service.Add(new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM), new QuantityModel<WeightUnit>(2.0, WeightUnit.KILOGRAM));
          Assert.AreEqual(3.0, res.Value, 0.000001);
           Assert.AreEqual(WeightUnit.KILOGRAM, res.Unit);
            }
    [TestMethod] 
    public void TestAddition_CrossUnit_KilogramPlusGram()
     {
         var res = _service.Add(new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM), new QuantityModel<WeightUnit>(1000.0, WeightUnit.GRAM)); 
         Assert.AreEqual(2.0, res.Value, 0.000001); 
         Assert.AreEqual(WeightUnit.KILOGRAM, res.Unit);
          }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_Kilogram()
     {
         var res = _service.Add(new QuantityModel<WeightUnit>(1.0, WeightUnit.KILOGRAM), new QuantityModel<WeightUnit>(1000.0, WeightUnit.GRAM), WeightUnit.GRAM);
          Assert.AreEqual(2000.0, res.Value, 0.000001);
          Assert.AreEqual(WeightUnit.GRAM, res.Unit);
           }

    // -------------------- Subtraction Tests --------------------

    [TestMethod] 
    public void TestSubtraction_KilogramMinusGram()
     {
         var res = _service.Subtract(new QuantityModel<WeightUnit>(2.0, WeightUnit.KILOGRAM), new QuantityModel<WeightUnit>(500.0, WeightUnit.GRAM));
          Assert.AreEqual(1.5, res.Value, 0.000001); 
          Assert.AreEqual(WeightUnit.KILOGRAM, res.Unit); 
          }
}
