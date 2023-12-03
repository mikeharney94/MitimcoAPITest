namespace MitimcoAPITest;

[TestClass]
public class StrategyTest
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        // Run before all test cases
        Global.IsTesting = true; // overriden so hitting error messages does not trigger emails
    }

    [TestMethod]
    public void OutputTypeTest()
    {
        DateTime today = DateTime.Today;
        DateTime yesterday = today.AddDays(-1);
        DateTime longAgo = today.AddDays(-200);
        Assert.AreEqual("compact", Strategy.GetOutputStrategy(yesterday, today));
        Assert.AreEqual("full", Strategy.GetOutputStrategy(longAgo, today));
    }
}