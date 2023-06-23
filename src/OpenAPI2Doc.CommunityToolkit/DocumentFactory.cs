using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApi2Doc.CommunityToolkit
{
    abstract public class DocumentFactory
    {
      public  abstract Document CreateDocument();
    }
}
