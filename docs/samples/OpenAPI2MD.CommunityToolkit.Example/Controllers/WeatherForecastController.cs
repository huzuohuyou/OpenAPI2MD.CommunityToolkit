using ApiConventions.CommunityToolKit;
using ApiConventions.CommunityToolKit.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OpenAPI2MD.CommunityToolkit.Example.Controllers
{
    /// <summary>
    /// ������
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
        /// ������
        /// </summary>
        /// <param name="token">����</param>
        /// <param name="id">����</param>
        /// <returns>����ֵ</returns>
        /// <remarks>
        /// ����һ�����Խӿ�
        /// </remarks>
        /// <example>
        /// ����ӿ���ôҪ����
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