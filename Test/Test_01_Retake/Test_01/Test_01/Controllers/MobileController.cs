using Microsoft.AspNetCore.Mvc;
using Test_01.Models.DTO;
using Test_01.Services;
using System;
using System.Threading.Tasks;

namespace Test_01.Controllers
{
    [ApiController]
    [Route("api/mobile")]
    public class MobileController : ControllerBase
    {
        private readonly IOperatorService _operatorService;

        public MobileController(IOperatorService operatorService)
        {
            _operatorService = operatorService;
        }

        [HttpPost]
        [Route("api/mobile")]
        public async Task<IActionResult> AddOrUpdateClient([FromBody] ClientPhoneNumberDTO dto)
        {
            if (!dto.PhoneNumber.StartsWith("+48"))
            {
                return BadRequest("Mobile number must start with +48.");
            }

            try
            {
                var result = await _operatorService.AddOrUpdateClientAndPhoneNumberAsync(dto);
                if (result == null)
                {
                    return BadRequest("Client with the given email does not exist or data incomplete.");
                }

                return CreatedAtAction(nameof(GetClientDetailsByPhoneNumber), new { phoneNumber = dto.PhoneNumber },
                    dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{phoneNumber}")]
        [Route("api/mobile/{phoneNumber}")]
        public async Task<IActionResult> GetClientDetailsByPhoneNumber(string phoneNumber)
        {
            if (!phoneNumber.StartsWith("+48"))
            {
                return BadRequest("Mobile number must start with +48.");
            }

            var details = await _operatorService.GetClientDetailsByPhoneNumberAsync(phoneNumber);
            if (details == null)
            {
                return NotFound("Mobile number does not exist.");
            }

            return Ok(details);
        }


        [HttpDelete("{phoneNumber}")]
        [Route("api/mobile/{phoneNumber}")] 
        public async Task<IActionResult> DeletePhoneNumberAndClient(string phoneNumber)
        {
            if (!phoneNumber.StartsWith("+48"))
            {
                return BadRequest("Mobile number must start with +48.");
            }

            var result = await _operatorService.DeletePhoneNumberAndClientAsync(phoneNumber);
            if (!result)
            {
                return NotFound("Mobile number does not exist or could not delete.");
            }

            return NoContent();
        }
    }
}
