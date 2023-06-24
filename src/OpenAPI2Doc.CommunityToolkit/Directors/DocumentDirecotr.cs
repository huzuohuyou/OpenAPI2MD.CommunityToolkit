using OpenApi2Doc.CommunityToolkit.Builders;

namespace OpenApi2Doc.CommunityToolkit.Directors
{
    internal class DocumentDirecotr
    {
        private  DocumentBuilder _builder;

        public DocumentDirecotr(DocumentBuilder builder)
        {
          _builder = builder;
        }

        public void ChangeBuilder(DocumentBuilder builder)
        {
            _builder = builder;
        }

        public void Make()
        {

        }
    }
}
