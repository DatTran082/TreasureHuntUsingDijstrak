using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TreasureHunt.API.Models;
using TreasureHunt.API.Models.Enums;
using TreasureHunt.Application.Models;
using TreasureHunt.Application.Services;


namespace TreasureHunt.API.Controllers
{
    public class APIHelper : ControllerBase
    {

        /// <summary>
        /// Trả về định dạng format theo từng API 
        /// </summary>
        /// <typeparam name="T">kiểu muốn trả về</typeparam>
        /// <param name="result">dữ liệu ban đầu</param>
        /// <param name="apiName">tên API - phục vụ log</param>        
        /// <returns></returns>
        protected T ContentReturn<T>(Result<object> result, string apiName)
        {
            result.Message = result.Message ?? result.Code.ToString();

            if (result.Code != ResultCode.Success)
            {
                result.Data = string.Empty;
                Console.WriteLine($"{apiName}: {JsonSerializer.Serialize(result.Data)}");
            }
            else
            {
                Console.WriteLine($"{apiName}: {JsonSerializer.Serialize(result.Data)}");
            }

            return (T)Activator.CreateInstance(typeof(T), result);
        }

    }
}