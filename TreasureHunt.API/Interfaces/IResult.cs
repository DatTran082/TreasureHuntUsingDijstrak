namespace TreasureHunt.API.Models
{
    public interface IResult
    {
        //ResultCode Code { get; set; }

        //string Message { get; set; }
    }

    public interface IResult<T> : IResult
    {
        //T Data { get; set; }
    }

    public interface IResult<T, TH> : IResult<T>
    {
        //TH ExtensiveInformation { get; set; }
    }
};