using TreasureHunt.API.Models.Requests;
using TreasureHunt.Application.Models;

namespace TreasureHunt.API.Models;

public class TreasureMapRequest : Request
{
    public TreasureInput data { get; set; }
}