using PCAxis.Paxiom;

namespace PCAxis.Core.Tests;

[TestClass]
public class VariableTests
{
    [TestMethod]
    public void ShouldAddValue()
    {
        // Arrange
        var variable = new Variable();
        var value = new Value("TestValue");

        // Act
        variable.Values.Add(value);

        // Assert
        Assert.AreEqual(1, variable.Values.Count);
    }
}
