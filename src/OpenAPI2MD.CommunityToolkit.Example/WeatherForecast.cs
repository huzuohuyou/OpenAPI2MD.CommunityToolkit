using Swashbuckle.AspNetCore.Annotations;

namespace OpenAPI2MD.CommunityToolkit.Example
{
    /// <summary>
    /// 天气信息实体
    /// </summary>
    [SwaggerSchemaFilter(typeof(WeatherForecastSchemaFilter))]
    public class WeatherForecast
    {
        /// <summary >
        /// 日期
        /// </summary>
        /// <example>
        /// 2020-02-02
        /// </example>
        [SwaggerSchema("The date it was created", Format = "date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }
       
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// 汇总
        /// </summary>
        /// <example>
        /// 汇总是这样写的吗
        /// </example>
        public string? Summary { get; set; }
    }
}