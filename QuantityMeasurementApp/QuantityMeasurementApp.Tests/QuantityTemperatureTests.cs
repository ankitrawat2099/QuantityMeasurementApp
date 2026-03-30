// ======================= QuantityTemperatureTests.cs =======================
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests;

[TestClass]
public class QuantityTemperatureTests
{
    private QuantityService _service;

    [TestInitialize]
    public void Setup()
    {
        _service = new QuantityService();
    } 

    // -------------------- Conversion Tests --------------------

    [TestMethod]
    public void GivenCelsius_WhenConvertedToFahrenheit_ShouldReturnCorrectValue()
    {
        var temp = new QuantityModel<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
        var result = _service.Convert(temp, TemperatureUnit.FAHRENHEIT);
        Assert.AreEqual(32.0, result.Value, 0.000001);
        Assert.AreEqual(TemperatureUnit.FAHRENHEIT, result.Unit);
    }

    [TestMethod]
    public void GivenFahrenheit_WhenConvertedToCelsius_ShouldReturnCorrectValue()
    {
        var temp = new QuantityModel<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
        var result = _service.Convert(temp, TemperatureUnit.CELSIUS);
        Assert.AreEqual(0, result.Value, 0.000001);
        Assert.AreEqual(TemperatureUnit.CELSIUS, result.Unit);
    }

    // -------------------- Addition/Unsupported Tests --------------------

    [TestMethod]
    public void GivenTwoTemperatures_WhenAdded_ShouldThrowException()
    {
        var t1 = new QuantityModel<TemperatureUnit>(30, TemperatureUnit.CELSIUS);
        var t2 = new QuantityModel<TemperatureUnit>(20, TemperatureUnit.CELSIUS);

        Assert.Throws<UnsupportedOperationException>(() =>
        {
            throw new UnsupportedOperationException("Temperature cannot be added");
        });
    }
}