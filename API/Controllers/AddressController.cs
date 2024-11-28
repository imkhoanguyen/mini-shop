using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Address;
using Shop.Application.Mappers;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class AddressController : BaseApiController
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAddressByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            try
            {
                var address = await _addressService.GetAddressByUserIdAsync(userId);

                if (address == null || !address.Any())
                {
                    return NotFound("No orders found for this user.");
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAddressAsync([FromBody] AddressAddDto addressAddDto)
        {
            if (addressAddDto == null)
            {
                return BadRequest("Address information is required.");
            }

            try
            {
                var newAddress = await _addressService.AddAddressAsync(addressAddDto);
                // Trả về thông tin của địa chỉ mới đã thêm
                return Ok(newAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetByAddressIdAsync(int addressId)
        {
            try
            {
                var address = await _addressService.GetByAddressIdAsync(addressId);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update/{addressId}")]
        public async Task<IActionResult> UpdateAddressAsync(int addressId, [FromBody] AddressAddDto addressAddDto)
        {
            if (addressAddDto == null)
            {
                return BadRequest("Address information is required.");
            }

            try
            {
                var updatedAddress = await _addressService.UpdateAddressAsync(addressId, addressAddDto);
                return Ok(updatedAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{addressId}")]
        public async Task<IActionResult> DeleteAddressAsync(int addressId)
        {
            try
            {
                var result = await _addressService.DeleteAddressAsync(addressId);
                return result ? Ok("Address deleted successfully.") : NotFound("Address not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}