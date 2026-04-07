using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementWebAPI.Controllers
{
    [Authorize(Roles ="admin,user")]
    [ApiController]
    [Route("api/v1/quantities")]
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        // Compare two quantities
       
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] CompareRequestDTO request)
        {
            try
            {
                var result = _service.CompareQuantities(request.ThisQuantityDTO, request.ThatQuantityDTO);
                return Ok(new { result });
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Add two quantities
        [HttpPost("add")]
        public IActionResult Add([FromBody] OperationRequestDTO request)
        {
            try
            {
                var result = _service.AddQuantities(request.ThisQuantityDTO, request.ThatQuantityDTO);
                return Ok(result);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Subtract two quantities
        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] OperationRequestDTO request)
        {
            try
            {
                var result = _service.SubtractQuantities(request.ThisQuantityDTO, request.ThatQuantityDTO);
                return Ok(result);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Divide two quantities
        [HttpPost("divide")]
        public IActionResult Divide([FromBody] OperationRequestDTO request)
        {
            try
            {
                var result = _service.DivideQuantities(request.ThisQuantityDTO, request.ThatQuantityDTO);
                return Ok(result);
            }
            catch (DivideByZeroException ex)
            {
                return StatusCode(500, new { error = "Divide by zero", message = ex.Message });
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Convert a quantity to target unit
        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConvertRequestDTO request)
        {
            try
            {
                var result = _service.ConvertQuantity(request.QuantityDTO, request.TargetUnit);
                return Ok(result);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Get all operations with data source info
       
        [HttpGet("history/all")]
        public IActionResult GetAllOperations()
        {
            var dataWithSource = _service.GetAllOperationsWithSource();
            
            return Ok(new 
            { 
                Source = dataWithSource.source, 
                Data = dataWithSource.data 
            });
        }

        // Get all errored operations
        [HttpGet("history/errored")]
        public IActionResult GetErroredHistory()
        {
            var result = _service.GetErroredOperations();
            return Ok(result);
        }

        // Get total count of operations by type
        [HttpGet("count/{operationType}")]
        public IActionResult GetOperationCount(string operationType)
        {
            var count = _service.GetOperationCount(operationType);
            return Ok(new { operationType, count });
        }
    }
}
   