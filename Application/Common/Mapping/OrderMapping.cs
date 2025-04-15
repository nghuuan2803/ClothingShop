using Domain.Entities;
using Shared.Orders;

namespace Application.Common.Mapping
{
    public static class OrderMapping
    {
        public static OrderItem ToOrderItem(this OrderItemReq item, string orderId)
        {
            return new OrderItem
            {
                OrderId = orderId,
                VariantId = item.VariantId,
                SizeId = item.SizeId,
                Quantity = item.Quantity
            };
        }
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                PhoneNumber = order.PhoneNumber,
                ShippingAdress = order.ShippingAdress,
                PaymentMethod = order.PaymentMethod,
                PaymentMethodName = order.PaymentMethod.ToString(),
                Discount = order.Discount,
                ShipFee = order.ShipFee,
                Amount = order.Amount,
                PromotionId = order.PromotionId,
                VoucherId = order.VoucherId,
                Status = order.Status,
                HandlerId = order.HandlerId,
                Handler = order.Handler?.FullName,
                Items = order.Items?.Select(p => p.ToItemDto()).ToArray()
            };
        }
        public static OrderItemDto ToItemDto(this OrderItem item)
        {
            return new OrderItemDto
            {
                VariantId = item.VariantId,
                Variant = item.Variant?.Product?.Name + " "+ item.Variant?.Color?.Name,
                SizeId = item.SizeId,
                Size = item.Size?.Name,
                Quantity = item.Quantity,
                Price = item.Price
            };
        }
    }
}
