using Core.Entities;
using Core.Enums;
using Core.Extensions;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Core.Specifications.Products
{
    public class OrderFilterSpecifications : BaseSpecifications<Order>
    {
        public OrderFilterSpecifications(Expression<Func<Order, bool>> expression, string[] includes = null)
            : base(expression, includes) { }

        public OrderFilterSpecifications(
            string orderBy = null,
            string? fromDate = null,
            string? toDate = null,
            int? userId = null,
            string? status = null,
            int? limit = null,
            int? page = null,
            string[] includes = null,
            bool ascending = false)
            : base(BuildCriteria(fromDate, toDate, userId , status), includes, orderBy, ascending, limit, page) { }

        // Helper method to build filter criteria expression
        private static Expression<Func<Order, bool>> BuildCriteria(string? fromDate, string? toDate, int? userId , string? status)
        {

            var dt = new DateTimeOffset();
            DateTimeOffset? fromDateParsed = dt.ParseStringDate(fromDate);
            DateTimeOffset? toDateParsed = dt.ParseStringDateToEndOfDay(toDate);
            OrderStatusEnum? parsedStatus = null;

            if (Enum.TryParse<OrderStatusEnum>(status, true, out var tempStatus))
            {
                parsedStatus = tempStatus;

            }

            Console.WriteLine(string.IsNullOrEmpty(status));

            return x =>
             (!userId.HasValue || x.UserId == userId) &&
             (fromDateParsed == null || x.OrderDate >= fromDateParsed) &&
             (toDateParsed == null || x.OrderDate <= toDateParsed)&&
             (string.IsNullOrEmpty(status) || x.OrderStatus == parsedStatus);
        }
    }
}
