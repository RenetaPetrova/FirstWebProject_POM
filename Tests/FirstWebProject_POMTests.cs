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
        public void RegisterNewUserWithoutEnteringAnyData()
        { 
            registerPage.OpenRegisterPage();
            registerPage.RegisterButton.Click();
            registerPage.AssertRegisterAllErrorMessages();
        }

        [Test, Order(3)]
        public void RegisterNewUserWithUsernameWith5CharsOnly()
        {
            string tooShortUsername = "Test1"; 
            string validPassword = "Testing123";
            string validEmail = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(tooShortUsername, validPassword, validEmail);
            Assert.IsTrue(registerPage.UsernameTooShortRegisterError.Displayed);
            Assert.That(registerPage.UsernameTooShortRegisterError.Text.Trim(), Is.EqualTo("The UserName must be at least 6 charecters long."));
        }

        [Test, Order(4)]
        public void RegisterNewUserWithUsernameWith250Chars()
        {
            string tooLongUsername = "thisisaverylongusernametotestthevalidationandthemaxlengthoftheusernamefieldandcheckwhetheritwillacceptsuchalongstringwithoutanyissuesorerrorsbeingthrownbytheformorbackendvalidationmechanismsandhowtheapplicationhandleslonginputintheusernamefieldwhensubmitted";
            string validPassword = "Testing123";
            string validEmail = "test123@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(tooLongUsername, validPassword, validEmail);

            //The error message says that the username must be at least 6 charecters long instead that it is very long
            Assert.IsTrue(registerPage.UsernameTooShortRegisterError.Displayed);
            Assert.That(registerPage.UsernameTooShortRegisterError.Text.Trim(), Is.EqualTo("The UserName must be at least 6 charecters long."));
        }

        [Test, Order(5)]
        public void RegisterNewUserWithEmptyUserName()
        {
            string username = "";
            string password = "Testing123";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);
            Assert.IsTrue(registerPage.UsernameRegisterMainError.Displayed);
            Assert.That(registerPage.UsernameRegisterMainError.Text.Trim(), Is.EqualTo("The UserName field is required."));          
        }

        [Test, Order(6)]
        public void RegisterNewUserWithOnlySpecialCharsInUsername() 
        {
            string username = "newuser";
            string password = "Testing123";
            string email = "test@gmail.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);
            Assert.IsTrue(registerPage.PasswordWithoutSpecialCharsRegisterError.Displayed);
            Assert.That(registerPage.PasswordWithoutSpecialCharsRegisterError.Text.Trim(), Is.EqualTo("Passwords must have at least one non letter or digit character."));
        }

        [Test, Order (7)]
        public void RegisterNewUserWithAlreadyRegisteredUsername()
        {
            string username = "lambda";
            string password = "Testing123!";
            string email = "something@yahoo.com";

            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email); 
            Assert.True(registerPage.AlreadyUserUsernameError.Displayed);
            Assert.That(registerPage.AlreadyUserUsernameError.Text.Trim(), Is.EqualTo("Name lambda is already taken."));
        }
    }
}
