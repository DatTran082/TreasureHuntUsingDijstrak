
using System;
using System.Web;
using TreasureHunt.API.Models.Enums;

namespace TreasureHunt.API.Models.Requests
{

    public class Request
    {

        public virtual string data { get; set; }

        /// <summary>
        /// Thời gian gửi lên, format theo định dạng 'yyyyMMddHHmmss'
        /// </summary>          
        public string request_time { get; set; }


        public Request()
        {
        }

        public Result<object> checkParam(int count = 0)
        {
            var result = new Result<object> { Code = ResultCode.Success };
            try
            {
                // if (string.IsNullOrEmpty(data))
                // {
                //     result.Code = ResultCode.InvalidInput;
                //     return result;
                // }
                return CheckMoreCondition(data);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.UnknowError;
                return result;
            }
        }

        /// <summary>
        /// Check các điều kiện khác ngoài điều kiện chung
        /// </summary>        
        /// <param name="decryptData">Dữ liệu data đã giải mã</param>        
        /// <returns></returns>
        protected virtual Result<object> CheckMoreCondition(string decryptData)
        {
            return new Result<object> { Code = (int)ResultCode.Success, Data = decryptData };
        }

    }
}