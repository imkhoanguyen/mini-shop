using Shop.Application.DTOs.Vouchers;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class VoucherMapper
    {
        public static Voucher VoucherDtoToEntity(VoucherDto voucherDto)
        {
            return new Voucher
            {
                title = voucherDto.title,
                description = voucherDto.description,
                percentage = voucherDto.percentage,
                start_date = voucherDto.start_date,
                end_date = voucherDto.end_date,
                is_active = voucherDto.is_active,
                created_at = voucherDto.created_at,
                updated_at = voucherDto.updated_at
            };
        }

        // cho nay can sua!!!
        //public static void UpdateVoucher(Voucher voucher, UpdateVoucherDto updateVoucherDto)
        //{
        //    if (updateVoucherDto.title != null)
        //    {
        //        voucher.title = updateVoucherDto.title;
        //    }
        //    if (updateVoucherDto.description != null)
        //    {
        //        voucher.description = updateVoucherDto.description;
        //    }
        //    if (updateVoucherDto.percentage.HasValue)
        //    {
        //        voucher.percentage = updateVoucherDto.percentage.Value;
        //    }
        //    if (updateVoucherDto.start_date.HasValue)
        //    {
        //        voucher.start_date = updateVoucherDto.start_date.Value;
        //    }
        //    if (updateVoucherDto.end_date.HasValue)
        //    {
        //        voucher.end_date = updateVoucherDto.end_date.Value;
        //    }
        //    if (updateVoucherDto.is_active.HasValue)
        //    {
        //        voucher.is_active = updateVoucherDto.is_active.Value;
        //    }

        //    voucher.updated_at = DateTime.Now; // Always update the timestamp on any change
        //}

        public static VoucherDto EntityToVoucherDto(Voucher voucher)
        {
            return new VoucherDto
            {
                title = voucher.title,
                description = voucher.description,
                percentage = voucher.percentage,
                start_date = voucher.start_date,
                end_date = voucher.end_date,
                is_active = voucher.is_active,
                created_at = voucher.created_at,
                updated_at = voucher.updated_at
            };
        }
    }
}
