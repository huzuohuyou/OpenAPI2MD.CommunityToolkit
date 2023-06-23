namespace OpenAPI2MD.CommunityToolkit.Generators;

public class OpenApiMdGenerator
{
    public async Task<string> Generate(string? requestUri, string savePath="")
    {
        try
        {
            var client = new HttpClient
            {
                //BaseAddress = new Uri("https://localhost:18101")
            };
            var stream = await client.GetStreamAsync(requestUri);
            var doc = new OpenApiStreamReader().Read(stream, out _);

            var sb = new StringBuilder();
            sb.Append($" # {doc.Info.Title}({doc.Info.Version}) \n" );
            sb.Append($"{doc.Info.Description} \n");
            sb.Append($"{doc.Info?.Contact?.Name} \n");
            sb.Append($"{doc.Info?.Contact?.Email} \n");
            sb.Append($"<!-- @import \"[TOC]\" {{cmd=\"toc\" depthFrom=2 depthTo=3 orderedList=false}} -->\n");
            var tag = string.Empty;
            doc.Paths.ToList().ForEach(r =>
            {
                var operation = r.Value.Operations.Values.FirstOrDefault();
                if (!tag.Equals(operation?.Tags.FirstOrDefault()?.Name))
                {
                    tag = operation?.Tags.FirstOrDefault()?.Name;
                    sb.Append($" \n## {tag} \n");
                }

                var t = new PathTable()
                {
                    OperationId = operation.OperationId,
                    Summary = operation?.Summary,
                    Description = operation?.Description,
                    Name = operation?.Summary,
                    Url = r.Key,
                    Deprecated = operation.Deprecated,
                    RequestMethod = r.Value.Operations.Keys.FirstOrDefault().ToString()
                };
                operation?.Parameters.ToList().ForEach(p =>
                {
                    var va = string.Empty;
                    if(p.Schema.Type == "array" && p.Example != null)
                    {
                        dynamic d = p.Example;
                        var v = d[0].Value;
                        va=v.ToString();
                    }
                    else if( p.Example != null)
                    {
                        dynamic d = p.Example;
                        var v = d.Value;
                        va = v.ToString();
                    }

                    t.RequestParams.Add(new RequestParam()
                    {
                        PropertyName = p.Name,
                        Description = p.Description,
                        ParamType = p.In.ToString(),
                        PropertyType = p.Schema.Type,
                        IsRequired = p.Required.ToString(),
                        Example = va
                    });
                });
                if (!Equals(null, operation?.RequestBody))
                {
                    var c = new RequestProperiesGenerator().Excute(operation.RequestBody?.Content?.FirstOrDefault()
                        .Value?.Schema);
                    if (!Equals(null, c))
                        t.RequestBodys.AddRange(c);
                }

                t.RequestBody = operation?.RequestBody?.Content?.FirstOrDefault().Value?.Schema;
                operation?.Responses.ToList().ForEach(keyValuePair =>
                {
                   new ExampleValueGenerator().Excute(keyValuePair.Value.Content.Count > 0 ? keyValuePair.Value.Content.FirstOrDefault().Value.Schema:default);
                   var type = new StringBuilder();
                   if (keyValuePair.Value.Content.Count > 0)
                   {
                       type = type.Append(keyValuePair.Value.Content.Count > 0 ? keyValuePair.Value.Content.FirstOrDefault().Value.Schema.Type : default);
                       if (Equals("array",type.ToString()))
                           type.Append($@":{keyValuePair.Value.Content.FirstOrDefault().Value.Schema?.Items?.Reference?.Id}");
                       if (Equals("object", type.ToString()))
                           type.Append($@":{keyValuePair.Value.Content.FirstOrDefault().Value.Schema?.Reference?.Id}");
                   }
                   var response = new Response()
                   {
                       Code = keyValuePair.Key,
                       Des = keyValuePair.Value.Description,
                       ResponseType = keyValuePair.Value.Content.Count > 0 ? keyValuePair.Value.Content.FirstOrDefault().Key : default,
                       ResponseDataType = type.ToString(),
                       OpenApiSchema = keyValuePair.Value.Content.Count > 0 ? keyValuePair.Value.Content.FirstOrDefault().Value.Schema : default

                   };
                   var c = new ResponseProperiesGenerator().Excute(keyValuePair.Value.Content.FirstOrDefault().Value?.Schema);
                   if (!Equals(null, c))
                       response.Schemas.AddRange(c);

                   t.Responses.Add(response);
                });
                var s = t.ToString();
                var displayDeprecated = operation.Deprecated ? "【已过时】" : "";
                sb.Append($"\n### {operation?.Summary}{displayDeprecated} \n");
                sb.Append($"{s} \n");
            });
            var s = sb.ToString();
            File.WriteAllText(Path.Combine(savePath, "swagger.md"), s);
            if (File.Exists(Path.Combine(savePath, "swagger.md")))
            {
                return Path.Combine(savePath, "swagger.md");
            }
            return "文档生成失败";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}