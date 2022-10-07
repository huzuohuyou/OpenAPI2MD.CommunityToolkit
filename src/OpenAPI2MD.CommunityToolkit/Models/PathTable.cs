using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAPI2MD.CommunityToolkit.Models
{
    public class PathTable
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public string URL { get; set; }
        public string RequestMethod { get; set; }
        public string RequestType { get; set; }
        public string ResponseType { get; set; }
        public List<RequestParam> RequestParams { get; set; }

        public override string ToString()
        {
            return $@"<table><tr>
                        <td colspan=""5"">{Title}</td>
                      </tr>
                      <tr>
                        <td >接口名称</td>
                        <td colspan=""4"">{Name}</td>
                      </tr>
                      <tr>
                        <td >接口描述</td>
                        <td colspan=""4"">{Des}</td>
                      </tr>
                      <tr>
                        <td >URL</td>
                        <td colspan=""4"">{URL}</td>
                      </tr>
                      <tr>
                        <td >请求方式</td>
                        <td colspan=""4"">{RequestMethod}</td>
                      </tr>
                      <tr>
                        <td >请求类型</td>
                        <td colspan=""4"">{RequestType}</td>
                      </tr>
                      <tr>
                        <td >返回类型</td>
                        <td colspan=""4"">{ResponseType}</td>
                      </tr>
                     </table> ";
        }
    }
    public class RequestParam
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string ParamType { get; set; }
        public string IsRequired { get; set; }
        public string Des { get; set; }
        public override string ToString()
        {
            return $@"<tr>
                        <td >参数名</td>
                        <td >数据类型</td>
                        <td >参数类型</td>
                        <td >是否必填</td>
                        <td >说明</td>
                      </tr>
                      ";
        }
    }

    public class Response
    {
        public string ResponseCode { get; set; }
        public string Des { get; set; }
        public string Remark { get; set; }
        public override string ToString()
        {
            return $@"<tr>
                        <td >状态码</td>
                        <td colspan=""2"">描述</td>
                        <td colspan=""2"">说明</td>
                      </tr>
                      ";
        }
    }

    public class Schema
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string Remark { get; set; }
        public override string ToString()
        {
            return $@"<tr>
                        <td >返回属性名</td>
                        <td colspan=""2"">类型</td>
                        <td colspan=""2"">说明</td>
                      </tr>
                      ";
        }
    }

    public class Example
    {
        public string RequestParam { get; set; }
        public string ResponseResult { get; set; }
        public override string ToString()
        {
            return $@"<tr>
                        <td colspan=""5"">示例</td>
                      </tr>
                      <tr>
                        <td >请求参数</td>
                        <td colspan=""4"">示例</td>
                      </tr>
                     <tr>
                        <td >返回值</td>
                        <td colspan=""4"">示例</td>
                      </tr>";
        }
    }
}
