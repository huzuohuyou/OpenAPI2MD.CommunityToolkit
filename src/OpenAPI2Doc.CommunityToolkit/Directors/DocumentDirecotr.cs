using OpenApi2Doc.CommunityToolkit.Builders;

namespace OpenApi2Doc.CommunityToolkit.Directors
{
    public class DocumentDirecotr
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

        public void Make(string type)
        {
            if (Equals(type,"default"))
            {
                _builder.BuildTitle();
                _builder.BuildToc();
                _builder.BuildToc();
                _builder.BuildServices();
            }

        }
    }
}
