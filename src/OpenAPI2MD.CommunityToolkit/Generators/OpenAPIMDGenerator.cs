namespace OpenAPI2MD.CommunityToolkit.Generators;

public class OpenApimdGenerator
{
    public async Task<string> ReadYaml()
    {
        try
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:18101")
            };
            var stream = await client.GetStreamAsync("swagger/1.0.0/swagger.json");
            var doc = new OpenApiStreamReader().Read(stream, out _);

            var sb = new StringBuilder();
            sb.Append($"# {doc.Info.Title}({doc.Info.Version}) \n");
            var tag = string.Empty;
            doc.Paths.ToList().ForEach(r =>
            {
                var operation = r.Value.Operations.Values.FirstOrDefault();
                if (!tag.Equals(operation?.Tags.FirstOrDefault()?.Name))
                {
                    tag = operation?.Tags.FirstOrDefault()?.Name;
                    sb.Append($"## {tag} \n");
                }

                var t = new PathTable()
                {
                    Summary = operation.Summary,
                    Description = operation.Description,
                    Name = operation.Summary,
                    URL = r.Key,
                    RequestMethod = r.Value.Operations.Keys.FirstOrDefault().ToString()
                };
                operation.Parameters.ToList().ForEach(p =>
                {
                    if (p.Example != null)
                    {
                        dynamic d = p.Example as dynamic;
                        var v = d.Value;
                    }
                    t.RequestParams.Add(new RequestParam()
                    {
                        PropertyName = p.Name,
                        Description = p.Description,
                        ParamType = p.In.ToString(),
                        PropertyType = p.Schema.Type,
                        IsRequired = p.Required.ToString(),
                        Example = (p.Example == null ? default : (p.Example as dynamic).Value)?.ToString()
                    });
                });
                if (!Equals(null, operation.RequestBody))
                    t.RequestBodys.AddRange(new RequestProperiesGenerator().Excute(operation.RequestBody?.Content?.FirstOrDefault().Value?.Schema));


                t.RequestBody = operation.RequestBody?.Content?.FirstOrDefault().Value?.Schema;
                operation.Responses.ToList().ForEach(r =>
                {
                   var res= new ExampleValueGenerator().Excute(r.Value.Content.Count > 0 ? r.Value.Content.FirstOrDefault().Value.Schema:default);
                   var type = new StringBuilder();
                   if (r.Value.Content.Count > 0)
                   {
                       type = type.Append(r.Value.Content.Count > 0 ? r.Value.Content.FirstOrDefault().Value.Schema.Type : default);
                       if (Equals("array",type.ToString()))
                           type.Append($@":{r.Value.Content.FirstOrDefault().Value.Schema.Items.Reference.Id}");
                       if (Equals("object", type.ToString()))
                           type.Append($@":{r.Value.Content.FirstOrDefault().Value.Schema.Reference.Id}");
                    }
                    var response = new Response()
                    {
                        Code = r.Key,
                        Des = r.Value.Description,
                        ResponseType = r.Value.Content.Count > 0 ? r.Value.Content.FirstOrDefault().Key : default,
                        ResponseDataType = type.ToString(),
                        OpenApiSchema = r.Value.Content.Count > 0 ? r.Value.Content.FirstOrDefault().Value.Schema : default

                    };
                    response.Schemas.AddRange(new ProperiesGenerator().Excute(r.Value.Content.FirstOrDefault().Value.Schema));
                    t.Responses.Add(response);
                });
                var s = t.ToString();
                sb.Append($"## {operation?.OperationId} \n");
                sb.Append($"{s} \n");
            });
            var s = sb.ToString();
            File.WriteAllText("dev.md", s);
            return sb.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}