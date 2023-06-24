namespace OpenApi2Doc.CommunityToolkit.Builders
{
    public abstract class DocumentBuilder
    {
        protected OpenApiDocument ApiDocument;

        public abstract void Reset();

        public abstract Info BuildInfo();

        public abstract Services BuildServices();

        public abstract Title BuildTitle();

        public abstract Toc BuildToc();

        public abstract T GetResult();

        private async Task BuildApiDocument(string? requestUri, string savePath = "")
        {
            var client = new HttpClient();
            var stream = await client.GetStreamAsync(requestUri);
            ApiDocument = new OpenApiStreamReader().Read(stream, out _);
        }
    }
}
