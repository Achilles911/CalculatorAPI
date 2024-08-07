using CalculatorAPI.Models;
using Jace;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly CalculationEngine _calculationEngine;

        public CalculatorController()
        {
            _calculationEngine = new CalculationEngine();
        }

        [HttpGet("add")]
        public ActionResult<CalculationResult> Add(double a, double b)
        {
            try
            {
                return new CalculationResult { Result = a + b };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("subtract")]
        public ActionResult<CalculationResult> Subtract(double a, double b)
        {
            try
            {
                return new CalculationResult { Result = a - b };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("multiply")]
        public ActionResult<CalculationResult> Multiply(double a, double b)
        {
            try
            {
                return new CalculationResult { Result = a * b };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("divide")]
        public ActionResult<CalculationResult> Divide(double a, double b)
        {
            try
            {
                if (b == 0)
                {
                    return BadRequest(new CalculationResult { ErrorMessage = "Division by zero." });
                }
                return new CalculationResult { Result = a / b };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("pow")]
        public ActionResult<CalculationResult> Power(double a, double b)
        {
            try
            {
                return new CalculationResult { Result = Math.Pow(a, b) };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("root")]
        public ActionResult<CalculationResult> Root(double a, double b)
        {
            try
            {
                if (a < 0)
                {
                    return BadRequest(new CalculationResult { ErrorMessage = "Cannot extract root of a negative number." });
                }
                return new CalculationResult { Result = Math.Pow(a, 1 / b) };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("evaluate")]
        public ActionResult<CalculationResult> Evaluate(string expression)
        {
            try
            {
                if (ContainsDivisionByZero(expression))
                {
                    return BadRequest(new CalculationResult { ErrorMessage = "Division by zero." });
                }

                var result = EvaluateExpression(expression);
                return new CalculationResult { Result = result };
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResult { ErrorMessage = ex.Message });
            }
        }

        private bool ContainsDivisionByZero(string expression)
        {
            // Regular expression to detect any division by zero in the expression
            var regex = new Regex(@"/\s*0(\D|$)");
            return regex.IsMatch(expression);
        }

        private double EvaluateExpression(string expression)
        {
            return _calculationEngine.Calculate(expression);
        }
    }
}
