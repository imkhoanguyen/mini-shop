using API.DTOs;
using API.Interfaces;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        [HttpPost("AddPayments")]
        public async Task<IActionResult> AddPayments([FromBody] PaymentsAddDto paymentsAddDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new{message="Dữ liệu không hợp lệ!"});
            }
            var payments=PaymentsAddDto.toPayments(paymentsAddDto);
            _unitOfWork.PaymentsRepository.AddPayments(payments);
            if (await _unitOfWork.Complete()) return Ok(new{message="Thanh toán thành công!"});
            //var paymentsDb= _unitOfWork.PaymentsRepository.AddPayments(cartItemsAddDto);
            return BadRequest(new {message="Thanh toán thất bại!"});
        }
        [HttpPut("UpdatePayments")]
        public async Task<IActionResult> UpdatePayments([FromBody]PaymentsUpdateDto paymentsUpdateDto)
        {
            if(!ModelState.IsValid) return BadRequest(new{message="Dữ liệu không hợp lệ!"});
            var payments=PaymentsUpdateDto.toPayments(paymentsUpdateDto);
            _unitOfWork.PaymentsRepository.UpdatePayments(payments);
            if(await _unitOfWork.Complete()) return Ok(new{message="Chỉnh sửa thành công!"});
            return BadRequest(new{message="Chỉnh sửa thất bại!"});
        }
        [HttpDelete("DeletePayments")]
        public async Task<IActionResult> DetelePayments([FromBody]PaymentsDeleteDto paymentsDeleteDto)
        {
            if(!ModelState.IsValid) return BadRequest(new{message="Dữ liệu không hợp lệ!"});
            var payments=PaymentsDeleteDto.toPayments(paymentsDeleteDto);
            _unitOfWork.PaymentsRepository.DeletePayments(payments);
            if(await _unitOfWork.Complete()) return Ok(new{message="Xóa thành công!"});
            return BadRequest(new{message="Xóa thất bại!"});
        }
        
    }
}