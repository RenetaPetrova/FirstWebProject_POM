namespace FirstWebProject_POM.Tests
{
    public class FirstWebProject_POMTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test, Order (1)]
        public void LoginWithEmptyUsernameAndPassword()
        {
            string invalidName = "";
            string invalidPassword = "";

            loginPage.OpenLoginPage();
            loginPage.PerformLogin(invalidName, invalidPassword);
            loginPage.AssertLoginErrorMessages();
        }

        [Test, Order(2)]
        public void RegisterNewUserWithMissingData()
        { 
            registerPage.OpenRegisterPage();
            registerPage.RegisterButton.Click();
            registerPage.AssertRegisterAllErrorMessages();
        }

        [Test, Order(3)]
        public void RegisterNewUserWithUsernameLessThan6characters()
        {
            string username = "testt";
            string password = "123456";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);
            registerPage.AssertErrorUsernameTooShort();
        }

        [Test, Order(4)]
        public void RegisterNewUserWithOnlyNumberInTheUsernamField()
        {
            string username = "123456";
            string password = "123456";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);
            registerPage.AssertErrorUsernameRequirenments();
        }

        [Test, Order(5)]
        public void RegisterNewUserWithVeryLongUsername250Chars()
        {
            string username = "thisisaverylongusernametotestthevalidationandthemaxlengthoftheusernamefieldandcheckwhetheritwillacceptsuchalongstringwithoutanyissuesorerrorsbeingthrownbytheformorbackendvalidationmechanismsandhowtheapplicationhandleslonginputintheusernamefieldwhensubmitted";
            string password = "123456";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //The error message should say that the requirenmets for the username are not met
            registerPage.AssertErrorUsernameTooShort();
        }

        [Test, Order(6)]
        public void RegisterNewUserWithOnlySpecialCharsInUsername()
        {
            string username = "!@#$%^&*&*%$#@";
            string password = "123456";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //The error message should say that the requirenmets for the username are not met
            registerPage.AssertErrorUsernameTooShort();
        }

        [Test, Order (7)]
        public void RegisterNewUserWithoutDigitInUsername()
        {
            string username = "Testing";
            string password = "123456";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            registerPage.AssertErrorUsernameRequirenments();
        }
    }
}
