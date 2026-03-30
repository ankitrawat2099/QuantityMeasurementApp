// ======================= QuantityVolumeTests.cs =======================
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using System;

namespace QuantityMeasurementApp.Tests;

[TestClass]
public class QuantityVolumeTests
{
    private QuantityService _service;

    [TestInitialize]
    public void Setup(){
        _service = new QuantityService();
    }

    // -------------------- Equality Tests --------------------

    [TestMethod] 
    public void TestEquality_LitreToLitre_SameValue()
     {
         Assert.IsTrue(_service.Compare(new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE), new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE))); 
         }
    [TestMethod] 
    public void TestEquality_LitreToMillilitre_EquivalentValue() 
    {
         Assert.IsTrue(_service.Compare(new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE), new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE)));
          }
    [TestMethod] 
    public void TestEquality_MillilitreToLitre_EquivalentValue() 
    { 
        Assert.IsTrue(_service.Compare(new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE), new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE))); 
        }

    // -------------------- Conversion Tests --------------------

    [TestMethod] 
    public void TestConversion_LitreToMillilitre() 
    {
         var res = _service.Convert(new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE), VolumeUnit.MILLILITRE);
          Assert.AreEqual(1000.0, res.Value, 0.000001); 
         Assert.AreEqual(VolumeUnit.MILLILITRE, res.Unit); 
         }
    [TestMethod] 
    public void TestConversion_GallonToLitre()
     {
         var res = _service.Convert(new QuantityModel<VolumeUnit>(1.0, VolumeUnit.GALLON), VolumeUnit.LITRE);
          Assert.AreEqual(3.78541, res.Value, 0.000001); 
          Assert.AreEqual(VolumeUnit.LITRE, res.Unit); }

    // -------------------- Addition Tests --------------------

    [TestMethod]
     public void TestAddition_SameUnit_LitrePlusLitre() 
     {
         var res = _service.Add(new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE), new QuantityModel<VolumeUnit>(2.0, VolumeUnit.LITRE)); 
         Assert.AreEqual(3.0, res.Value, 0.000001); 
         Assert.AreEqual(VolumeUnit.LITRE, res.Unit); 
         }
    [TestMethod]
     public void TestAddition_CrossUnit_LitrePlusMillilitre() 
     {
         var res = _service.Add(new QuantityModel<VolumeUnit>(1.0, VolumeUnit.LITRE), new QuantityModel<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE));
          Assert.AreEqual(2.0, res.Value, 0.000001);
           Assert.AreEqual(VolumeUnit.LITRE, res.Unit);
            }

    // -------------------- Division Tests --------------------

    [TestMethod] 
    public void TestDivision_LitreByMillilitre()
     {
         var result = _service.Divide(new QuantityModel<VolumeUnit>(2.0, VolumeUnit.LITRE), new QuantityModel<VolumeUnit>(500.0, VolumeUnit.MILLILITRE));
          Assert.AreEqual(4.0, result, 0.000001);
           }
}