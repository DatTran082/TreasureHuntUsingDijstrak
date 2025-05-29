

using TreasureHunt.API.Models.Enums;

namespace TreasureHunt.API.Models
{

    public class Result : IResult
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public Result()
        {
        }
    }

    public class Result<T> : Result, IResult<T>
    {

        public T Data { get; set; }


        public string ResponseTime { get; private set; }


        public string Sign { get; private set; }


        public string RequestNo { get; set; }


        public Result() : base()
        {
            ResponseTime = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }
    }


    // public class Result<T, TH> : Result<T>, IResult<T, TH>
    // {
    //     private TH _extensiveInformation;

    //     public TH ExtensiveInformation
    //     {
    //         get { return _extensiveInformation; }
    //         set { _extensiveInformation = value; }
    //     }
    // }
}