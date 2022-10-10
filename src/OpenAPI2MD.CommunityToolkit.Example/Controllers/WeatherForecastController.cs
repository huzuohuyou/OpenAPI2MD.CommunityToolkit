using ApiConventions.CommunityToolKit;
using ApiConventions.CommunityToolKit.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using log4net;

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


        public WeatherForecastController(ILog logger, JwtHelper jwtHelper):base(logger)
        {
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
        /// <param name="token" example="asdfasfeadsf">令牌</param>
        /// <param name="id" value="" example="888">参数</param>
        /// <returns example="666">返回值</returns>
        /// <remarks>
        /// 这是一个测试接口
        /// </remarks>
        [HttpGet("{id}", Name = "GetById")]
        public  ActionResult<IEnumerable<WeatherForecast>> Get([FromHeader(Name = "Authorization")]
            [Required]
            string token,
            [FromRoute] int id)
        {
             new ClientCodeGenerator().Excute();
            _ =  new OpenApimdGenerator().ReadYaml();
            return Ok();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}