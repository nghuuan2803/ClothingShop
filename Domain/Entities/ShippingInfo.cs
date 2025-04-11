namespace Domain.Entities
{
    public class ShippingInfo
    {
        public int ShopId { get; set; }
        public int OrderId { get; set; }
        public int FromDistrict { get; set; }
        public int ToDistrict { get; set; }
        public int ServiceId { get; set; }
        public int InsuranceValue { get; set; }
        public string? Coupon { get; set; } = string.Empty;
        public string ToWardCode { get; set; } = string.Empty;
        public int ToDistrictId { get; set; }
        public int FromDistrictId { get; set; }
        public int Weight { get; set; } = 1000; // 1kg
        public int Length { get; set; } = 20; // 20cm
        public int Width { get; set; } = 10; // 10cm
        public int Height { get; set; } = 20; // 20cm
        public int Fee { get; set; }
        public string? FeeInfo { get; set; }
        public int Status { get; set; }

    }
}
