using TreasureHunt.API.Models.Requests;
using TreasureHunt.Application.Models;

namespace TreasureHunt.API.Models;

public class TreasureHuntRequest : Request
{
    // public TreasureInput data { get; set; }
    public int data { get; set; }
}