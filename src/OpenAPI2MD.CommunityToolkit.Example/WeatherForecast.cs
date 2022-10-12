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
        /// <example>100</example>
        public int TemperatureC { get; set; }
       
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// ����
        /// </summary>
        /// <example>
        /// ����������д����
        /// </example>
        public string? Summary { get; set; }

        public Remark Mark { get; set; }

        public List<Remark> Remarks { get; set; }
    }

    public class Remark
    {
        /// <summary>
        /// idc
        /// </summary>
        /// <example> 3.1415932</example>
        public float percent { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <example>�Ѵ���װ����ּ�����</example>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <example>true</example>
        public bool OK { get; set; }
    }
}