// ======================= QuantityLengthTests.cs =======================
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using System;

namespace QuantityMeasurementApp.Tests;

[TestClass]
public class QuantityLengthTests
{
    private QuantityService _service;

    [TestInitialize]
    public void Setup(){
        _service = new QuantityService();
    }

    // -------------------- Equality Tests --------------------

    [TestMethod] 
    public void TestEquality_FEETToFEET_SameValue() 
    {
         Assert.IsTrue(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET))); }
    [TestMethod]
     public void TestEquality_INCHESToINCHES_SameValue()
      {
         Assert.IsTrue(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.INCHES), new QuantityModel<LengthUnit>(1.0, LengthUnit.INCHES))); }
    [TestMethod]
     public void TestEquality_FEETToINCHES_EquivalentValue()
      {
         Assert.IsTrue(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES))); }
    [TestMethod]
     public void TestEquality_DifferentValue() 
     {
         Assert.IsFalse(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET))); }
    [TestMethod]
     public void TestEquality_NullComparison() 
     {
         Assert.IsFalse(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET).Equals(null)); }
    [TestMethod] 
    public void TestEquality_SameReference()
     
     { var q = new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET); Assert.IsTrue(q.Equals(q)); }
    [TestMethod] 
    public void TestEquality_YARDSToFEET_EquivalentValue()
     {
         Assert.IsTrue(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.YARDS), new QuantityModel<LengthUnit>(3.0, LengthUnit.FEET))); }
    [TestMethod] 
    public void TestEquality_YARDSToINCHES_EquivalentValue() 
    {
         Assert.IsTrue(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.YARDS), new QuantityModel<LengthUnit>(36.0, LengthUnit.INCHES))); }
    [TestMethod] 
    public void TestEquality_CENTIMETERSToINCHES_EquivalentValue() 
    {
         Assert.IsTrue(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.CENTIMETERS), new QuantityModel<LengthUnit>(0.393701, LengthUnit.INCHES)));
          }
    [TestMethod] 
    public void TestEquality_YARDSToYARDS_DifferentValue()
     {
         Assert.IsFalse(_service.Compare(new QuantityModel<LengthUnit>(1.0, LengthUnit.YARDS), new QuantityModel<LengthUnit>(2.0, LengthUnit.YARDS))); }

    // -------------------- Conversion Tests --------------------

    [TestMethod]
    public void TestConversion_RoundTrip()
    {
        var orig = new QuantityModel<LengthUnit>(5.0, LengthUnit.FEET);
        var toYards = _service.Convert(orig, LengthUnit.YARDS);
        var back = _service.Convert(toYards, LengthUnit.FEET);
        Assert.AreEqual(5.0, back.Value, 0.000001);
    }
    [TestMethod]
    public void TestConversion_NegativeValue()
    {
        var res = _service.Convert(new QuantityModel<LengthUnit>(-1.0, LengthUnit.FEET), LengthUnit.INCHES); Assert.AreEqual(-12.0, res.Value, 0.000001);
    }
    [TestMethod] 
    public void TestConversion_ZeroValue() 
    {
         var res = _service.Convert(new QuantityModel<LengthUnit>(0.0, LengthUnit.FEET), LengthUnit.INCHES);
          Assert.AreEqual(0.0, res.Value, 0.000001);
           }
    [TestMethod]
     public void TestConversion_CENTIMETERSToINCHES()
      {
         var res = _service.Convert(new QuantityModel<LengthUnit>(2.54, LengthUnit.CENTIMETERS), LengthUnit.INCHES);
          Assert.AreEqual(1.0, res.Value, 0.0001);
           }
    [TestMethod]
     public void TestConversion_YARDSsToFEET() 
     {
         var res = _service.Convert(new QuantityModel<LengthUnit>(3.0, LengthUnit.YARDS), LengthUnit.FEET);
          Assert.AreEqual(9.0, res.Value, 0.000001); 
          
          }
    [TestMethod]
     public void TestConversion_INCHESesToFEET() { 
        var res = _service.Convert(new QuantityModel<LengthUnit>(24.0, LengthUnit.INCHES), LengthUnit.FEET);
         Assert.AreEqual(2.0, res.Value, 0.000001);
          }
    [TestMethod]
     public void TestConversion_FEETToINCHESes() 
     { 
        var res = _service.Convert(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), LengthUnit.INCHES); 
        Assert.AreEqual(12.0, res.Value, 0.000001);
         }

    // -------------------- Addition Tests --------------------

    [TestMethod] 
    public void TestAddition_NullSecondOperand() 
    { 
        try 
        {
             _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), null); 
             Assert.Fail(); 
             }
              catch (ArgumentException)
        {
            
        } }
    [TestMethod]
     public void TestAddition_NegativeValue() 
     {
         var res = _service.Add(new QuantityModel<LengthUnit>(5.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(-2.0, LengthUnit.FEET));
          Assert.AreEqual(3.0, res.Value, 0.000001); }
    [TestMethod]
     public void TestAddition_WithZero() 
     { 
        var res = _service.Add(new QuantityModel<LengthUnit>(5.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(0.0, LengthUnit.INCHES)); 
        Assert.AreEqual(5.0, res.Value, 0.000001); }
    [TestMethod] 
    public void TestAddition_CrossUnit_INCHESPlusFEET()
     {
         var res = _service.Add(new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET));
     Assert.AreEqual(24.0, res.Value, 0.000001); }
    [TestMethod] 
    public void TestAddition_CrossUnit_FEETPlusINCHES() 
    { 
        var res = _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES)); 
        Assert.AreEqual(2.0, res.Value, 0.000001); }
    [TestMethod] 
    public void TestAddition_SameUnit_FEETPlusFEET() 
    {
         var res = _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET)); 
         Assert.AreEqual(3.0, res.Value, 0.000001); 
         }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_FEET() 
    { 
        var res = _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), LengthUnit.FEET); 
        Assert.AreEqual(2.0, res.Value, 0.000001); 
        }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_INCHES() 
    { 
        var res = _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), LengthUnit.INCHES);
         Assert.AreEqual(24.0, res.Value, 0.000001); 
         }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_YARDSs()
     {
         var res = _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), LengthUnit.YARDS); 
         Assert.AreEqual(0.666666, res.Value, 0.000001); 
         }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_CENTIMETERS()
     {
         var res = _service.Add(new QuantityModel<LengthUnit>(2.54, LengthUnit.CENTIMETERS), new QuantityModel<LengthUnit>(1.0, LengthUnit.INCHES), LengthUnit.CENTIMETERS);
          Assert.AreEqual(5.08, res.Value, 0.000001); 
          }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_Commutativity()
     { 
        var r1 = _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), LengthUnit.YARDS);
         var r2 = _service.Add(new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), LengthUnit.YARDS);
          Assert.AreEqual(r1.Value, r2.Value, 0.000001); }
    [TestMethod] 
    public void TestAddition_ExplicitTargetUnit_InvalidTarget() 
    {
         try { 
            _service.Add(new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), (LengthUnit)999);
             Assert.Fail(); } catch (ArgumentException)
        {
            
        } }

    // -------------------- Subtraction Tests --------------------

    [TestMethod] 
    public void TestSubtraction_SameUnit_FEETMinusFEET() 
    {
         var res = _service.Subtract(new QuantityModel<LengthUnit>(5.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET)); 
         Assert.AreEqual(3.0, res.Value, 0.000001);
          }
    [TestMethod] 
    public void TestSubtraction_CrossUnit_FEETMinusINCHES() 
    {
         var res = _service.Subtract(new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES));
          Assert.AreEqual(1.0, res.Value, 0.000001); 
          }
    [TestMethod] 
    public void TestSubtraction_CrossUnit_INCHESMinusFEET()
     
     { var res = _service.Subtract(new QuantityModel<LengthUnit>(24.0, LengthUnit.INCHES), new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET));
      Assert.AreEqual(12.0, res.Value, 0.000001);
       }
    [TestMethod] 
    public void TestSubtraction_TargetUnit_FEET()
     {
         var res = _service.Subtract(new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), LengthUnit.FEET);
          Assert.AreEqual(1.0, res.Value, 0.000001);
           }
    [TestMethod] 
    public void TestSubtraction_TargetUnit_INCHES() 
    { 
        var res = _service.Subtract(new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(12.0, LengthUnit.INCHES), LengthUnit.INCHES);
         Assert.AreEqual(12.0, res.Value, 0.000001);
          }

    // -------------------- Division Tests --------------------

    [TestMethod] 
    public void TestDivision_SameUnit() 
    {
         var res = _service.Divide(new QuantityModel<LengthUnit>(10.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(2.0, LengthUnit.FEET));
          Assert.AreEqual(5.0, res, 0.000001);
           }
    [TestMethod]
     public void TestDivision_CrossUnit()
      {
         var res = _service.Divide(new QuantityModel<LengthUnit>(24.0, LengthUnit.INCHES), new QuantityModel<LengthUnit>(1.0, LengthUnit.FEET));
          Assert.AreEqual(2.0, res, 0.000001);
           }
    [TestMethod]
    public void TestDivision_ByZero()
    {
        try
        {
            _service.Divide(new QuantityModel<LengthUnit>(10.0, LengthUnit.FEET), new QuantityModel<LengthUnit>(0.0, LengthUnit.FEET));
            Assert.Fail();
        }
        catch (ArithmeticException)
        {

        }
    }
}