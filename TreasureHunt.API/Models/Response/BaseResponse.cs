using TreasureHunt.API.Models;
using TreasureHunt.API.Models.Enums;

public class BaseResponse
{
    /// <summary>
    /// Mã lỗi trả về
    /// </summary>           
    public ResultCode Code { get; set; }

    /// <summary>
    /// Message trả về
    /// </summary>            
    public string Message { get; set; }

    /// <summary>
    /// Khởi tạo giá trị cho Response vs giá trị Result truyền vào
    /// </summary>
    /// <param name="result">giá trị trả về</param>
    public BaseResponse(Result<object> result)
    {
        this.Code = result.Code;
        this.Message = result.Message;
    }

    /// <summary>
    /// khởi tạo class
    /// </summary>
    public BaseResponse()
    {
    }

}