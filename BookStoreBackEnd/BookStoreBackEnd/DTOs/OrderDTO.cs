﻿public class OrderDTO
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int ShippingAddressId { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}
