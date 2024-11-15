using Core.Attributes;
using Core.DTOS.Shared;
using Core.Entities;
using Core.Enums;
using Core.Specifications.SpecificationBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.DTOS.Order
{
    public class OrderQueryDto : BaseSearchQueryDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int? UserId { get; set; }

        [EnumStringValidation(typeof(OrderStatus), ErrorMessage = "Invalid order status.")]
        public string? Status { get; set; }

        public string? Sort { get; set; }

        public OrderSpecificationBuilder BuildSpecification()
        {
            var builder = new OrderSpecificationBuilder();

            if (!string.IsNullOrEmpty(Status))
            {
                builder.WithStatus(Status);
            }

            if (!string.IsNullOrEmpty(ToDate))
            {
                builder.WithEndDate(ToDate);
            }

            if (!string.IsNullOrEmpty(FromDate))
            {
                builder.WithStartDate(FromDate);
            }

            if (UserId != null)
            {
                builder.WithUserId(UserId.Value);
            }


            if (!string.IsNullOrEmpty(Sort))
            {
                builder.WithOrderBy(Sort, Asc ?? true);
            }

            builder.WithLimit(Limit);
            builder.WithPage(Page);

            return builder;
        }

    }
}
