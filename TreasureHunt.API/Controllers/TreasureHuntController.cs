
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TreasureHunt.API.Models;
using TreasureHunt.API.Models.Enums;
using TreasureHunt.Application.Interfaces;
using TreasureHunt.Application.Models;
using TreasureHunt.Infrastructure.Data.Entities;
using TreasureHunt.Infrastructure.Interfaces;
using System.Reflection;


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
    public async Task<TreasureHuntResponse> Solve(string? rawrequest)
    {
        var result = new Result<object>();
        try
        {
            if (string.IsNullOrEmpty(rawrequest))
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntResponse>(result, "Solve");
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
                return ContentReturn<TreasureHuntResponse>(result, "Solve");
            }

            if (request == null)
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntResponse>(result, "Solve");
            }

            var checkRequest = request.checkParam();

            var inputre = (string)checkRequest.Data;

            if (string.IsNullOrEmpty(inputre))
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntResponse>(result, "Solve");
            }


            var inputTreasure = JsonSerializer.Deserialize<TreasureInput>(request.data);
            if (inputTreasure == null)
            {
                result.Code = ResultCode.InvalidInput;
                result.Message = "Invalid TreasureInput format.";
                return ContentReturn<TreasureHuntResponse>(result, "Solve");
            }
            else
            {
                var resultc = _solverService.Solve(inputTreasure);
                result.Data = resultc;
                return ContentReturn<TreasureHuntResponse>(result, "Solve");
            }
        }

        catch (Exception ex)
        {
            result.Code = ResultCode.SystemError;
            result.Message = ex.Message;
            result.Data = null;
            return ContentReturn<TreasureHuntResponse>(result, "Solve");
        }

    }

    [HttpGet("GetAllMaps")]
    public async Task<TreasureHuntMapsResponse> GetAllMaps()
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
            result.Data = string.Empty;

            return ContentReturn<TreasureHuntMapsResponse>(result, "GetAllMaps");
        }
    }

    [HttpPost("CreateMap")]
    public async Task<TreasureHuntMapsResponse> CreateMap(TreasureInput request)
    {
        var result = new Result<object>();
        try
        {

            if (request == null)
            {
                result.Code = ResultCode.InvalidInput;
                return ContentReturn<TreasureHuntMapsResponse>(result, "CreateMap");
            }

            var map = await _mapService.CreateMapAsync(request);
            result.Data = new List<TreasureMap>() { map };
            result.Message = "Map created successfully";
            return ContentReturn<TreasureHuntMapsResponse>(result, "CreateMap");
        }
        catch (Exception ex)
        {
            result.Code = ResultCode.SystemError;
            result.Message = ex.Message;
            result.Data = string.Empty;
            return ContentReturn<TreasureHuntMapsResponse>(result, "CreateMap");
        }
    }



}