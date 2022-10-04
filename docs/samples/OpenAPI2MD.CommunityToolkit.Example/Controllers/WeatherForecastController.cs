using ApiConventions.CommunityToolKit;
using ApiConventions.CommunityToolKit.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OpenAPI2MD.CommunityToolkit.Example.Controllers
{
    /// <summary>
    /// 啦啦啦
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : CtkControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, JwtHelper jwtHelper)
        {
            _logger = logger;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// token
        /// </summary>
        /// <returns></returns>
        [HttpGet("token", Name = "GetToken")]
        [AllowAnonymous]
        public ActionResult<string> GetToken()
        {
            return _jwtHelper.CreateToken();
        }

        /// <summary>
        /// 查天气
        /// </summary>
        /// <param name="token">令牌</param>
        /// <param name="id">参数</param>
        /// <returns>返回值</returns>
        /// <remarks>
        /// 这是一个测试接口
        /// </remarks>
        /// <example>
        /// 这个接口这么要调用
        /// </example>
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IEnumerable<WeatherForecast>> Get([FromHeader(Name = "Authorization")]
            [Required]
            string token,
            [FromRoute] int id)
        {
            
            await new OpenAPIMDGenerator().ReadYaml();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}