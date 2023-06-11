namespace OpenAPI2Word.CommunityToolkit.Generators;

public class OpenApimdGenerator
{
    public async Task ReadYaml(string? requestUri= "http://172.26.172.122:18100/swagger/2.3.0/swagger.json", string savePath="")
    {
        try
        {
            Found:
            {
                Console.WriteLine("请输入swagger.json的url:");
                requestUri = Console.ReadLine();
            }
            Stream stream;
            var client = new HttpClient();
            try
            {
                stream = await client.GetStreamAsync(requestUri);
            }
            catch (Exception e)
            {
                goto Found;
            }

            var openApiDocument = new OpenApiStreamReader().Read(stream, out _);
            var newFile2 = $@"swagger_{DateTime.Now.Ticks}.docx";
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}/swagger.docx"))
            {
                File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}/swagger.docx");
            }
           
            await using var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write);
            XWPFDocument doc = new XWPFDocument();
   

            //基本信息
            new TitileAndVersionGenerator().Generate(doc, $"  {openApiDocument.Info.Title}({openApiDocument.Info.Version}) ");
            new DescriptionGenerator().Generate(doc, $"  {openApiDocument.Info.Description}  ");
            new ContactNameGenerator().Generate(doc, $" {openApiDocument.Info?.Contact?.Name}  ");
            new ContactEmailGenerator().Generate(doc, $" {openApiDocument.Info?.Contact?.Email}  ");
            var tag = string.Empty;
            openApiDocument.Paths.ToList().ForEach(r =>
            {
                var operation = r.Value.Operations.Values.FirstOrDefault();
                if (!tag.Equals(operation?.Tags.FirstOrDefault()?.Name))
                {
                    tag = operation?.Tags.FirstOrDefault()?.Name;
                    new Header2Generator().Generate(doc, $" {tag}  ");
                }
                new Header3Generator().Generate(doc, $"{operation.OperationId}  ");
                var path = new PathTable()
                {
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
                    if (p.Schema.Type == "array" && p.Example != null)
                    {
                        dynamic d = p.Example;
                        var v = d[0].Value;
                        va = v.ToString();
                    }
                    else if (p.Example != null)
                    {
                        dynamic d = p.Example;
                        var v = d.Value;
                        va = v.ToString();
                    }

                    path.RequestParams.Add(new RequestParam()
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
                        path.RequestBodys.AddRange(c);
                }

                path.RequestBody = operation?.RequestBody?.Content?.FirstOrDefault().Value?.Schema;
                
                operation?.Responses.ToList().ForEach(keyValuePair =>
                {
                    new ExampleValueGenerator().Excute(keyValuePair.Value.Content.Count > 0 ? keyValuePair.Value.Content.FirstOrDefault().Value.Schema : default);
                    var type = new StringBuilder();
                    if (keyValuePair.Value.Content.Count > 0)
                    {
                        type = type.Append(keyValuePair.Value.Content.Count > 0 ? keyValuePair.Value.Content.FirstOrDefault().Value.Schema.Type : default);
                        if (Equals("array", type.ToString()))
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

                    path.Responses.Add(response);
                });
                path.Generate(doc);
            });

            doc.Write(fs);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}