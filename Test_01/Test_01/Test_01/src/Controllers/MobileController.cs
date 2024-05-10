using Microsoft.AspNetCore.Mvc;
using MobileOperatorApi.Data;
using Test_01.Repositories;

namespace Test_01.Controllers
{
    [Route("api/mobile")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly OperatorRepository _dbHelper = new OperatorRepository();

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateClient([FromBody] Client client)
        {
            if (!client.MobileNumber.StartsWith("+48"))
                return BadRequest("Mobile Number Must Start With +48.");

            await _dbHelper.AddOrUpdateClientAsync(client);
            return Ok();
        }

        [HttpGet("{mobileNumber}")]
        public async Task<IActionResult> GetClient(string mobileNumber)
        {
            var client = await _dbHelper.GetClientByMobileNumberAsync(mobileNumber);
            if (client == null) return NotFound("Client Not Found!!");

            return Ok(client);
        }

        [HttpDelete("{mobileNumber}")]
        public async Task<IActionResult> DeleteClient(string mobileNumber)
        {
            bool deleted = await _dbHelper.DeleteClientByMobileNumberAsync(mobileNumber);
            if (!deleted) return NotFound("Client Not Found!!");

            return Ok();
        }
    }
}