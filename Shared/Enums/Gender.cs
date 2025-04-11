using System.ComponentModel;

namespace Shared.Enums
{
    public enum Gender
    {
        [Description("Nam")]
        Male = 0,
        [Description("Nữ")]
        Female = 1,
        [Description("Khác")]
        Orther = 2
    }
}
