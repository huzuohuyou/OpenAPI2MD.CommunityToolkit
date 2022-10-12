# OpenAPI2MD.CommunityToolkit.Example(1.0.0) 
## HelloWorld 
## SayHello 
<table>
    
<tr>
    <td colspan="6" bgcolor="#ddd">SayHello</td>
</tr>
<tr>
    <td >接口名称</td>
    <td colspan="5">SayHello</td>
</tr>
<tr>
    <td >接口描述</td>
    <td colspan="5"></td>
</tr>
<tr>
    <td >URL</td>
    <td colspan="5">/HelloWorld/SayHello</td>
</tr>
<tr>
    <td >请求方式</td>
    <td colspan="5">Get</td>
</tr>
<tr>
    <td >请求类型</td>
    <td colspan="5"></td>
</tr>
    <tr>
    <td bgcolor="#ddd">参数名</td>
    <td bgcolor="#ddd">数据类型</td>
    <td bgcolor="#ddd">参数类型</td>
    <td bgcolor="#ddd">是否必填</td>
    <td bgcolor="#ddd">说明</td>
    <td bgcolor="#ddd">示例</td>
</tr>
    <tr>
    <td bgcolor="#ddd">状态码</td>
    <td colspan="1" bgcolor="#ddd">描述</td>
    <td colspan="2" bgcolor="#ddd">类型</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
</tr>
<tr>
    <td >200</td>
    <td >Success</td>
    <td colspan="2" >application/json</td>
    <td colspan="2" >string</td>
</tr>
<tr>
    <td bgcolor="#ddd">返回属性名</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
    <td colspan="3" bgcolor="#ddd">说明</td>
    
</tr>
    
    
</table> 
## WeatherForecast 
## GetToken 
<table>
    
<tr>
    <td colspan="6" bgcolor="#ddd">token</td>
</tr>
<tr>
    <td >接口名称</td>
    <td colspan="5">token</td>
</tr>
<tr>
    <td >接口描述</td>
    <td colspan="5"></td>
</tr>
<tr>
    <td >URL</td>
    <td colspan="5">/WeatherForecast/token</td>
</tr>
<tr>
    <td >请求方式</td>
    <td colspan="5">Get</td>
</tr>
<tr>
    <td >请求类型</td>
    <td colspan="5"></td>
</tr>
    <tr>
    <td bgcolor="#ddd">参数名</td>
    <td bgcolor="#ddd">数据类型</td>
    <td bgcolor="#ddd">参数类型</td>
    <td bgcolor="#ddd">是否必填</td>
    <td bgcolor="#ddd">说明</td>
    <td bgcolor="#ddd">示例</td>
</tr>
    <tr>
    <td bgcolor="#ddd">状态码</td>
    <td colspan="1" bgcolor="#ddd">描述</td>
    <td colspan="2" bgcolor="#ddd">类型</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
</tr>
<tr>
    <td >200</td>
    <td >Success</td>
    <td colspan="2" >application/json</td>
    <td colspan="2" >string</td>
</tr>
<tr>
    <td bgcolor="#ddd">返回属性名</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
    <td colspan="3" bgcolor="#ddd">说明</td>
    
</tr>
    
    
</table> 
## GetById 
<table>
    
<tr>
    <td colspan="6" bgcolor="#ddd">查天气</td>
</tr>
<tr>
    <td >接口名称</td>
    <td colspan="5">查天气</td>
</tr>
<tr>
    <td >接口描述</td>
    <td colspan="5">这是一个测试接口</td>
</tr>
<tr>
    <td >URL</td>
    <td colspan="5">/WeatherForecast/{id}</td>
</tr>
<tr>
    <td >请求方式</td>
    <td colspan="5">Get</td>
</tr>
<tr>
    <td >请求类型</td>
    <td colspan="5"></td>
</tr>
    <tr>
    <td bgcolor="#ddd">参数名</td>
    <td bgcolor="#ddd">数据类型</td>
    <td bgcolor="#ddd">参数类型</td>
    <td bgcolor="#ddd">是否必填</td>
    <td bgcolor="#ddd">说明</td>
    <td bgcolor="#ddd">示例</td>
</tr><tr>
    <td >Authorization</td>
    <td >string</td>
    <td >Header</td>
    <td >True</td>
    <td >令牌</td>
    <td >asdfasfeadsf</td>
</tr><tr>
    <td >id</td>
    <td >integer</td>
    <td >Path</td>
    <td >True</td>
    <td >参数</td>
    <td >888</td>
</tr>
    <tr>
    <td bgcolor="#ddd">状态码</td>
    <td colspan="1" bgcolor="#ddd">描述</td>
    <td colspan="2" bgcolor="#ddd">类型</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
</tr>
<tr>
    <td >200</td>
    <td >Success</td>
    <td colspan="2" >application/json</td>
    <td colspan="2" >array</td>
</tr>
<tr>
    <td bgcolor="#ddd">返回属性名</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
    <td colspan="3" bgcolor="#ddd">说明</td>
    
</tr><tr>
    <td >date</td>
    <td colspan="2">string</td>
    <td colspan="3" >日期</td>
   
</tr><tr>
    <td >temperatureC</td>
    <td colspan="2">integer</td>
    <td colspan="3" >温度</td>
   
</tr><tr>
    <td >temperatureF</td>
    <td colspan="2">integer</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >summary</td>
    <td colspan="2">string</td>
    <td colspan="3" >汇总</td>
   
</tr><tr>
    <td >mark</td>
    <td colspan="2">object</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >remarks</td>
    <td colspan="2">array</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td bgcolor="#ddd">状态码</td>
    <td colspan="1" bgcolor="#ddd">描述</td>
    <td colspan="2" bgcolor="#ddd">类型</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
</tr>
<tr>
    <td >400</td>
    <td >Bad Request</td>
    <td colspan="2" >application/json</td>
    <td colspan="2" >object</td>
</tr>
<tr>
    <td bgcolor="#ddd">返回属性名</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
    <td colspan="3" bgcolor="#ddd">说明</td>
    
</tr><tr>
    <td >code</td>
    <td colspan="2">integer</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >message</td>
    <td colspan="2">string</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >details</td>
    <td colspan="2"></td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td bgcolor="#ddd">状态码</td>
    <td colspan="1" bgcolor="#ddd">描述</td>
    <td colspan="2" bgcolor="#ddd">类型</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
</tr>
<tr>
    <td >404</td>
    <td >Not Found</td>
    <td colspan="2" >application/json</td>
    <td colspan="2" >object</td>
</tr>
<tr>
    <td bgcolor="#ddd">返回属性名</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
    <td colspan="3" bgcolor="#ddd">说明</td>
    
</tr><tr>
    <td >code</td>
    <td colspan="2">integer</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >message</td>
    <td colspan="2">string</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >details</td>
    <td colspan="2"></td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td bgcolor="#ddd">状态码</td>
    <td colspan="1" bgcolor="#ddd">描述</td>
    <td colspan="2" bgcolor="#ddd">类型</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
</tr>
<tr>
    <td >500</td>
    <td >Server Error</td>
    <td colspan="2" >application/json</td>
    <td colspan="2" >object</td>
</tr>
<tr>
    <td bgcolor="#ddd">返回属性名</td>
    <td colspan="2" bgcolor="#ddd">数据类型</td>
    <td colspan="3" bgcolor="#ddd">说明</td>
    
</tr><tr>
    <td >message</td>
    <td colspan="2">string</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >exceptionMessage</td>
    <td colspan="2">string</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >exceptionType</td>
    <td colspan="2">string</td>
    <td colspan="3" ></td>
   
</tr><tr>
    <td >stackTrace</td>
    <td colspan="2">string</td>
    <td colspan="3" ></td>
   
</tr>
    
    
</table> 
