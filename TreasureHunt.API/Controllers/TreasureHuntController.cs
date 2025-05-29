using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TreasureHunt.API.Models;
using TreasureHunt.API.Models.Enums;
using TreasureHunt.Application.Interfaces;
using TreasureHunt.Application.Models;
using TreasureHunt.Infrastructure.Data.Entities;
using TreasureHunt.Infrastructure.Interfaces;

namespace TreasureHunt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreasureHuntController : APIHelper
{
    private readonly ITreasureSolverService _solverService;
    private readonly ITreasureMapService _mapService;


    public TreasureHuntController(ITreasureSolverService solverService, ITreasureMapService mapService)
    {
        _solverService = solverService;
        _mapService = mapService;
    }

    [HttpPost("solve")]
    public async Task<TreasureHuntResponse> Post(string? rawrequest)
    {
        var result = new Result<object>();
        try
        {
            if (string.IsNullOrEmpty(rawrequest))
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntResponse>(result, "Treasure");
            }
            TreasureHuntRequest request = null;
            try
            {
                request = JsonSerializer.Deserialize<TreasureHuntRequest>(rawrequest);
            }
            catch (JsonException)
            {
                result.Code = ResultCode.InvalidInput;
                result.Message = "Invalid JSON format.";
                return ContentReturn<TreasureHuntResponse>(result, "Treasure");
            }

            if (request == null)
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntResponse>(result, "Treasure");
            }

            var checkRequest = request.checkParam();

            var inputre = (string)checkRequest.Data;

            if (string.IsNullOrEmpty(inputre))
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntResponse>(result, "Treasure");
            }


            var inputTreasure = JsonSerializer.Deserialize<TreasureInput>(request.data);
            if (inputTreasure == null)
            {
                result.Code = ResultCode.InvalidInput;
                result.Message = "Invalid TreasureInput format.";
                return ContentReturn<TreasureHuntResponse>(result, "Treasure");
            }
            else
            {
                var resultc = _solverService.Solve(inputTreasure);
                result.Data = resultc;
                return ContentReturn<TreasureHuntResponse>(result, "Treasure");
            }
        }

        catch (Exception ex)
        {
            result.Code = ResultCode.SystemError;
            result.Message = ex.Message;
            result.Data = null;
            return ContentReturn<TreasureHuntResponse>(result, "Treasure");
        }

    }

    [HttpGet("GetAllMaps")]
    public async Task<TreasureHuntMapsResponse> Get()
    {
        var result = new Result<object>();
        try
        {
            var maps = await _mapService.GetAllMapsAsync();

            result.Data = maps;

            if (maps == null || maps.Count == 0)
            {
                result.Message = "can not find any treasure maps";
            }

            return ContentReturn<TreasureHuntMapsResponse>(result, "GetAllMaps");
        }

        catch (Exception ex)
        {
            result.Code = ResultCode.SystemError;
            result.Message = ex.Message;
            result.Data = null;
            return ContentReturn<TreasureHuntMapsResponse>(result, "GetAllMaps");
        }
    }

    [HttpPost("CreateMap")]
    public async Task<TreasureHuntMapsResponse> CreateMap(TreasureInput input)
    {
        var result = new Result<object>();
        try
        {
            var map = await _mapService.CreateMapAsync(input);
            result.Data = new List<TreasureMap>() { map };

            return ContentReturn<TreasureHuntMapsResponse>(result, "Treasure");
        }

        catch (Exception ex)
        {
            result.Code = ResultCode.SystemError;
            result.Message = ex.Message;
            result.Data = null;
            return ContentReturn<TreasureHuntMapsResponse>(result, "Treasure");
        }
    }



}