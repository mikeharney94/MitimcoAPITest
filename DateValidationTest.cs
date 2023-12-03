namespace MitimcoAPITest;

[TestClass]
public class DateValidationTest
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        // Run before all test cases
        Global.IsTesting = true; // overriden so hitting error messages do not trigger emails
    }

    [TestMethod]
    public void FromDateToDateValidTest()
    {
        string fromDate = "2022-01-01";
        string toDate = "2022-12-30";
        List<DateTime> result = DateValidation.fromDate_toDate_validation(fromDate, toDate);
        Assert.AreEqual(DateValidation.parseDate(fromDate), result[0]);
        Assert.AreEqual(DateValidation.parseDate(toDate), result[1]);
    }

    [TestMethod]
    public void EarlyFromDateTest()
    {
        try{
            DateValidation.fromDate_toDate_validation("1998-01-01", "1998-01-02");
            Assert.Equals("Should not reach", "Has reached");
        } catch (ArgumentException e)
        {
            StringAssert.Contains(e.Message, ErrorMessages.fromDatePrecedesAPI);
        }
    }

    [TestMethod]
    public void ToDateRequiredTest()
    {
        try{
            DateValidation.fromDate_toDate_validation("2020-01-01", null);
            Assert.Equals("Should not reach", "Has reached");
        } catch (ArgumentException e)
        {
            StringAssert.Contains(e.Message, ErrorMessages.fromDateRequiredWithToDate);
        }
    }

    [TestMethod]
    public void ToDateIsAfterFromDateTest()
    {
        try{
            DateValidation.fromDate_toDate_validation("2022-01-01", "2021-12-30");
            Assert.Equals("Should not reach", "Has reached");
        } catch (ArgumentException e)
        {
            StringAssert.Contains(e.Message, ErrorMessages.fromDatePrecedesToDate);
        }
    }

    [TestMethod]
    public void DateInFutureTest()
    {
        try{
            DateValidation.fromDate_toDate_validation("3022-01-01", "3022-12-30");
            Assert.Equals("Should not reach", "Has reached");
        } catch (ArgumentException e)
        {
            StringAssert.Contains(e.Message, ErrorMessages.dateInFuture);
        }
    }
}