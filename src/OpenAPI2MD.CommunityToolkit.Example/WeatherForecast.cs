namespace OpenAPI2MD.CommunityToolkit.Example
{
    /// <summary>
    /// ������Ϣʵ��
    /// </summary>
    public class WeatherForecast
    {
        /// <summary >
        /// ����
        /// </summary>
        /// <example>
        /// 2020-02-02
        /// </example>
        public DateTime Date { get; set; }
        /// <summary>
        /// �¶�
        /// </summary>
        public int TemperatureC { get; set; }
       
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// ����
        /// </summary>
        /// <example>
        /// ����������д����
        /// </example>
        public string? Summary { get; set; }
    }
}