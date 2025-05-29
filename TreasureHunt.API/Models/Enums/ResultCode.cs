using System;
using System.ComponentModel;

namespace TreasureHunt.API.Models.Enums
{
    [Flags]
    public enum ResultCode
    {
        [Description("Thành công")]
        Success = 0,
        [Description("Lỗi không xác định")]
        UnknowError = -100,

        [Description("Lỗi Hệ thống")]
        SystemError = -500,

        [Description("Request không hợp lệ")]
        InvalidInput = -400
    }
}