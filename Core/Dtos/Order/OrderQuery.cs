using Core.Attributes;
using Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Core.Dtos.Order
{
    public class OrderQueryDto:BaseSearchQueryDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int? UserId { get; set; }

        [EnumStringValidation(typeof(OrderStatusEnum), ErrorMessage = "Invalid order status.")]
        public string? Status { get; set; }
    }
}
