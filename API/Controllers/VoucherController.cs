using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Vouchers;
using System.Text.Json;
namespace API.Controllers
{
    public class VoucherController : BaseApiController
    {
        //private readonly IUnitOfWork _unitOfWork;
        //public VoucherController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        //[HttpPost("AddVoucher")]
        //public async Task<IActionResult> AddVoucher([FromForm] VoucherDto voucherDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // return BadRequest("Dữ liệu không hợp lệ.");
        //        return BadRequest(ModelState);
        //    }

        //    var voucher = VoucherDto.toVoucher(voucherDto);

        //    _unitOfWork.VoucherRepository.AddVoucher(voucher);

        //    if (await _unitOfWork.Complete()){
        //        // return Ok("Update voucher successfully.");
        //        return Ok(new { message = "Update voucher successfully." });
        //    }

        //    return BadRequest("Failed to add voucher");
        //}

        //[HttpGet("GetAllVouchers")] 
        //public async Task<IActionResult> GetAllVouchers()
        //{
        //    var vouchers = _unitOfWork.VoucherRepository.GetAllVouchers();

        //    if (vouchers == null || !vouchers.Any())
        //    {
        //        return NotFound("No vouchers found.");
        //    }

        //    return Ok(vouchers);
        //}
        //[HttpGet("GetVoucher/{id}")]
        //public async Task<IActionResult> GetVoucher(int id)
        //{
        //    var voucher = _unitOfWork.VoucherRepository.GetVoucher(id);

        //    if (voucher == null)
        //    {
        //        return NotFound("Voucher not found.");
        //    }

        //    return Ok(voucher);
        //}

        //[HttpPut("UpdateVoucher/{id}")]
        //public async Task<IActionResult> UpdateVoucher(int id, [FromForm] UpdateVoucherDto updateVoucherDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Return detailed validation errors
        //    }

        //    var existingVoucher = _unitOfWork.VoucherRepository.GetVoucher(id);

        //    if (existingVoucher == null)
        //    {
        //        return NotFound("Voucher not found.");
        //    }

        //    Console.WriteLine($"Received update for Voucher ID {id}: {JsonSerializer.Serialize(updateVoucherDto)}");
        //    // Use the UpdateVoucher method to apply changes
        //    UpdateVoucherDto.UpdateVoucher(existingVoucher, updateVoucherDto);

        //    if (await _unitOfWork.Complete())
        //    {
        //        // return Ok("Voucher updated successfully.");
        //        return Ok(new { message = "Update voucher successfully." });
        //    }

        //    return BadRequest("Failed to update voucher.");
        //}

        //[HttpDelete("DeleteVoucher/{id}")]
        //public async Task<IActionResult> DeleteVoucher(int id)
        //{
        //    var existingVoucher = _unitOfWork.VoucherRepository.GetVoucher(id);

        //    if (existingVoucher == null)
        //    {
        //        return NotFound("Voucher not found.");
        //    }

        //    // Call the repository method to delete the voucher
        //    _unitOfWork.VoucherRepository.DeleteVoucher(id);

        //    if (await _unitOfWork.Complete())
        //    {
        //        // return Ok("Voucher deleted successfully.");
        //        return Ok(new { message = "Voucher deleted successfully" });
        //    }

        //    return BadRequest("Failed to delete voucher.");
        //}

        //[HttpDelete("RestoreVoucher/{id}")]
        //public async Task<IActionResult> RestoreVoucher(int id)
        //{
        //    var existingVoucher = _unitOfWork.VoucherRepository.GetVoucher(id);

        //    if (existingVoucher == null)
        //    {
        //        return NotFound("Voucher not found.");
        //    }

        //    // Call the repository method to delete the voucher
        //    _unitOfWork.VoucherRepository.RestoreVoucher(id);

        //    if (await _unitOfWork.Complete())
        //    {
        //        // return Ok("Voucher restored successfully.");
        //        return Ok(new { message = "Voucher restored successfully." });
        //    }

        //    return BadRequest("Failed to restore voucher.");
        //}



    }
}