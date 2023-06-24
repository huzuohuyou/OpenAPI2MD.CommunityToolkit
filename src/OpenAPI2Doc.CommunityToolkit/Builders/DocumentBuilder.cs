namespace OpenApi2Doc.CommunityToolkit.Builders
{
    public abstract class DocumentBuilder<T>
    {
        protected T doc;

        protected OpenApiDocument ApiDocument { get; set; }

        protected PathBuilder<T> pathBuilder { get; set; }

        public abstract void Reset();

        protected abstract void InitDoc();

        protected abstract void BuildInfo();

        private void BuildPaths()
        {
            pathBuilder.InitDoc(doc);
            pathBuilder.IniApiDocument(ApiDocument);
            pathBuilder.Build();
        }

        protected abstract void BuildTitle();

        protected abstract void BuildToc();

        protected abstract T OutputDoc(string savePath = "");

        private async Task BuildApiDocument(string? requestUri, string savePath = "")
        {
            var client = new HttpClient();
            var stream = await client.GetStreamAsync(requestUri);
            ApiDocument = new OpenApiStreamReader().Read(stream, out _);
        }

        public async Task<T> Build(string? requestUri, string savePath = "")
        {
            await BuildApiDocument(requestUri, savePath);
            InitDoc();
            BuildTitle();
            BuildToc();
            BuildInfo();
            BuildPaths();
            return OutputDoc(savePath);
        }
    }
}
