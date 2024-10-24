using System;
using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class VoucherDto
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public decimal percentage { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool is_active { get; set; }
        
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public static Voucher toVoucher(VoucherDto voucherDto)
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
    }
    
    public class UpdateVoucherDto
    {

        public string? title { get; set; } // Optional, can be null for no change
        public string? description { get; set; } // Optional
        public decimal? percentage { get; set; } // Optional
        public DateTime? start_date { get; set; } // Optional
        public DateTime? end_date { get; set; } // Optional
        public bool? is_active { get; set; } // Optional

        public static void UpdateVoucher(Voucher voucher, UpdateVoucherDto updateVoucherDto)
        {
            if (updateVoucherDto.title != null)
            {
                voucher.title = updateVoucherDto.title;
            }
            if (updateVoucherDto.description != null)
            {
                voucher.description = updateVoucherDto.description;
            }
            if (updateVoucherDto.percentage.HasValue)
            {
                voucher.percentage = updateVoucherDto.percentage.Value;
            }
            if (updateVoucherDto.start_date.HasValue)
            {
                voucher.start_date = updateVoucherDto.start_date.Value;
            }
            if (updateVoucherDto.end_date.HasValue)
            {
                voucher.end_date = updateVoucherDto.end_date.Value;
            }
            if (updateVoucherDto.is_active.HasValue)
            {
                voucher.is_active = updateVoucherDto.is_active.Value;
            }

            voucher.updated_at = DateTime.Now; // Always update the timestamp on any change
        }


    }
    
}