using PCAxis.Paxiom;
using System.Text;

namespace PCAxis.Core.Tests;

[TestClass]
public class EncodingUtilTests
{
    [TestMethod]
    public void ShouldGetEncoding()
    {
        // Arrange
        //Get a UTF-32 encoding by codepage.
        var encodingName = 12000;
        var encoding1 = Encoding.GetEncoding(encodingName);


        // Act
        var encoding2 = EncodingUtil.GetEncoding("utf-32");

        // Assert
        Assert.AreEqual(encoding1, encoding2);
    }
}
