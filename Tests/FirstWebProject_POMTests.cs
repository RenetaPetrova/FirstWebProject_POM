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
            registerPage.AssertErrorUsernameTooShort();
        }

        [Test, Order(4)]
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

        [Test, Order(5)]
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

        //test 6 - register with already used username

    }
}
