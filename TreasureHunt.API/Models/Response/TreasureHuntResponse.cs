using TreasureHunt.API.Models;
using TreasureHunt.API.Models.Enums;
using TreasureHunt.Infrastructure.Data.Entities;

public class TreasureHuntResponse : BaseResponse
{
    public double? Data { get; set; }

    public TreasureHuntResponse(double data, ResultCode code, string message) : base()
    {
        Data = data;
        Code = code;
        Message = message;
    }
    public TreasureHuntResponse(Result<object> result) : base(result)
    {
        if (result.Data != null)
        {
            Data = (double)result.Data;
        }
        else
        {
            Data = null; // Default value if Data is not a double
        }
    }
    public TreasureHuntResponse()
    {
    }

}
public class TreasureHuntMapsResponse : BaseResponse
{
    public List<TreasureMap> Data { get; set; }

    public TreasureHuntMapsResponse(List<TreasureMap> data, ResultCode code, string message) : base()
    {
        Data = data;
        Code = code;
        Message = message;
    }
    public TreasureHuntMapsResponse(Result<object> result) : base(result)
    {
        if (result.Data != null && result.Data is List<TreasureMap>)
        {
            Data = (List<TreasureMap>)result.Data;
        }
        else
        {
            Data = new List<TreasureMap>(); // Default value if Data is not a double
        }
    }
    public TreasureHuntMapsResponse()
    {
    }

}