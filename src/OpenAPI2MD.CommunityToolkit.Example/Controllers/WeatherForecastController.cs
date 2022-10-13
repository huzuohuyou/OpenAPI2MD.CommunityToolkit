using ApiConventions.CommunityToolKit;
using ApiConventions.CommunityToolKit.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using log4net;
using OpenAPI2MD.CommunityToolkit.Generators;

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
        /// ������
        /// </summary>
        /// <param name="token" example="asdfasfeadsf">����</param>
        /// <param name="id" value="" example="888">����</param>
        /// <returns example="666">����ֵ</returns>
        /// <remarks>
        /// ����һ�����Խӿ�
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

        /// <summary>
        /// �޸�������¼
        /// </summary>
        /// <remarks>
        ///����һ�����Խӿ�
        /// </remarks>
        /// <param name="token" example="3.1415926">��¼i����</param>
        /// <param name="entity" example="111"></param>
        /// <returns></returns>
        [HttpPost("Update", Name = "Update")]
        public ActionResult<WeatherForecast> Update(
            [FromHeader(Name = "Authorization")]
            [Required]
            string token,
            [FromBody][Required] WeatherForecast entity)
        {

            new ClientCodeGenerator().Excute();
            _ = new OpenApimdGenerator().ReadYaml();
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