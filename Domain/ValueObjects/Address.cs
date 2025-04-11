namespace Domain.ValueObjects
{
    public class Address
    {
        public string? HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; } //xã/phường
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }

        public override string ToString()
        {
            return $"{HouseNumber} {Street}, {Ward}, {District}, {Province}, {Country}".Trim().Replace("  ", " ");
        }
    }
}
