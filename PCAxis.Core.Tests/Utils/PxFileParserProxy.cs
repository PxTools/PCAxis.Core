using PCAxis.Paxiom.Parsers;
using System.Text;

namespace PCAxis.Core.Tests.Utils
{
    internal sealed class PxFileParserProxy(string fileData) : PXFileParser
    {
        protected override Stream GetStream()
        {
            var bytes = Encoding.UTF8.GetBytes(fileData);
            return new MemoryStream(bytes, writable: false);
        }

    }
}
