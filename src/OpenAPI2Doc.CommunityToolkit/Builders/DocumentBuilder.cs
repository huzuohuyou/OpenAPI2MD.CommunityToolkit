
using System.Xml.XPath;

namespace OpenApi2Doc.CommunityToolkit.Builders
{
    public abstract class DocumentBuilder<T>
    {
        protected T Doc;

        protected OpenApiDocument ApiDocument { get; set; }

        protected string CurrentPathTag { get; set; }
        protected OpenApiPathItem CurrentPathItem { get; set; }
        protected OpenApiOperation CurrentOperation { get; set; }

        protected OpenApiResponse CurrentResponse { get; set; }
        protected string CurrentResponseCode { get; set; }

        public abstract void Reset();

        protected abstract void InitDoc();

        protected abstract void BuildInfo();

        private void BuildPathItem()
        {
            BuildTag();
            BuildSummary();
            BeginBuildPathItem();
            BuildOperationId();
            BuildDescription();
            BuildRequestMethod();
            BuildRequestType();
            BuildRequestParams();
            BuildRequestBodies();
            BuildRequestBodyExample();
            foreach (var currentOperationResponse in CurrentOperation.Responses)
            {
                CurrentResponse = currentOperationResponse.Value;
                CurrentResponseCode = currentOperationResponse.Key;
                BuildResponse();
                BuildResponseFields();
                BuildResponseExample();
            }

            AfterBuildPathItem();
        }

        protected abstract void BeginBuildPathItem();
        protected abstract void AfterBuildPathItem();

        protected abstract void BuildTag();

        protected abstract void BuildSummary();

        protected abstract void BuildOperationId();

        protected abstract void BuildDescription();

        protected abstract void BuildRequestMethod();

        protected abstract void BuildRequestType();

        protected abstract void BuildRequestParams();

        protected abstract void BuildRequestBodies();

        protected abstract void BuildRequestBodyExample();

        protected abstract void BuildResponse();
        protected abstract void BuildResponseFields();

        protected abstract void BuildResponseExample();

        protected abstract void BuildTitle();

        protected abstract void BuildToc();

        protected abstract T OutputDoc(string savePath = "");

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




        public async Task<T> Build(string? requestUri, string savePath = "")
        {
            await BuildApiDocument(requestUri, savePath);
            InitDoc();
            BuildTitle();
            BuildToc();
            BuildInfo();
            ApiDocument.Paths.ToList().ForEach(r =>
            {
                CurrentPathItem = r.Value;
                CurrentOperation = CurrentPathItem.Operations.Values.FirstOrDefault();
                BuildPathItem();
            });

            return OutputDoc(savePath);
        }

    }
}