using Order = Core.Entities.Order;
using System.Linq.Expressions;

using Core.Helper;
using Core.Extensions;
using Core.Enums;

namespace Core.Specifications.SpecificationBuilder
{
    // Builder class for constructing ProductFilterSpecifications
    public class OrderSpecificationBuilder:BaseSpecificationBuilder<Order , OrderSpecificationBuilder>
    {
        public OrderSpecificationBuilder WithUserId(int userId)
        {
            
            _criteria = PredicateBuilder.And(_criteria, x => x.UserId == userId);

            return this;
        }


        public OrderSpecificationBuilder WithStartDate(string? startDate)
        {
            var dt = new DateTimeOffset();

            DateTimeOffset? dateParsed = dt.ParseStringDate(startDate);

            if (dateParsed != null) {

                _criteria = PredicateBuilder.And(_criteria,x => x.OrderDate >= dateParsed );

            }
            return this;

        }

        public OrderSpecificationBuilder WithPaymentIntentId(string paymentIntentId)
        {
           
            _criteria = PredicateBuilder.And(_criteria, x => x.PaymentIntentId == paymentIntentId);

            return this;

        }

        public OrderSpecificationBuilder WithEndDate(string? endDate)
        {
            var dt = new DateTimeOffset();

            DateTimeOffset? dateParsed = dt.ParseStringDateToEndOfDay(endDate);

            if (dateParsed != null)
            {

                _criteria = PredicateBuilder.And(_criteria, x => x.OrderDate <= dateParsed);

            }
            return this;

        }

        public OrderSpecificationBuilder WithStatus(string status)
        {
            if (Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
            {
                _criteria = PredicateBuilder.And(_criteria, x => x.OrderStatus == parsedStatus);

            }
            return this;

        }
      

    }
}
