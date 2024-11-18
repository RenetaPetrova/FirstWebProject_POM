using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using FirstWebProject_POM.Pages;

namespace FirstWebProject_POM.Tests
{
    public class FirstWebProject_POMTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test, Order(1)]
        public void RegisterNewUserWithAlreadyRegisteredUsername()
        {
            // Arrange
            string username = "lambda";
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";  
            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //Assert
            Assert.True(registerPage.AlreadyUsedUsernameError.Displayed, "The 'username already taken' error message was not displayed.");
            Assert.That(registerPage.AlreadyUsedUsernameError.Text.Trim(), Is.EqualTo("Name lambda is already taken."), "The 'username already taken' error message was not displayed.");
        }

        [Test, Order(2)]
        public void RegisterNewUserWithoutEnteringAnyData()
        { 
            // Act + Assert
            registerPage.OpenRegisterPage();
            registerPage.RegisterButton.Click();
            registerPage.AssertRegisterAllErrorMessages();
        }

        [Test, Order(3)]
        public void RegisterNewUserWithUsernameWith5CharsOnly()
        {
            // Arrange
            string tooShortUsername = "Test1"; 
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(tooShortUsername, password, email);
            
            // Assert
            Assert.IsTrue(registerPage.UsernameTooShortRegisterError.Displayed, "The 'username too short' error message was not displayed.");
            Assert.That(registerPage.UsernameTooShortRegisterError.Text.Trim(), Is.EqualTo("The UserName must be at least 6 charecters long."), "The 'username too short' error message was not displayed.");
        }

        [Test, Order(4)]
        public void RegisterNewUserWithUsernameWith250Chars()
        {
            // Arrange
            string tooLongUsername = "thisisaverylongusernametotestthevalidationandthemaxlengthoftheusernamefieldandcheckwhetheritwillacceptsuchalongstringwithoutanyissuesorerrorsbeingthrownbytheformorbackendvalidationmechanismsandhowtheapplicationhandleslonginputintheusernamefieldwhensubmitted";
            string password = "Testing123";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(tooLongUsername, password, email);

            //Assert
            //The error message says that the username must be at least 6 charecters long instead that it is very long
            Assert.IsTrue(registerPage.UsernameTooShortRegisterError.Displayed, "The 'username too short' error message was not displayed.");
            Assert.That(registerPage.UsernameTooShortRegisterError.Text.Trim(), Is.EqualTo("The UserName must be at least 6 charecters long."), "The 'username too short' error message was not displayed.");
        }

        [Test, Order(5)]
        public void RegisterNewUserWithEmptyUserName()
        {
            // Arrange
            string username = "";
            string password = "Testing123";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);
            
            // Assert
            Assert.IsTrue(registerPage.UsernameRegisterMainError.Displayed, "The 'username required' error message was not displayed.");
            Assert.That(registerPage.UsernameRegisterMainError.Text.Trim(), Is.EqualTo("The UserName field is required."), "The 'username required' error message text is incorrect.");          
        }

        [Test, Order(6)]
        public void RegisterNewUserWithOnlySpecialCharsInUsername() 
        {
            // Arrange
            string username = registerPage.GenerateRandomSpecialCharsUsername();
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com"; 

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert
            Assert.That(driver.Url, Is.EqualTo(registerPage.BaseUrl), "The registration did not redirect to the expected page.");
            Assert.IsTrue(registerPage.WelcomeMessage.Displayed, "The 'welcome message' is not displayed correctly.");
            Assert.That(registerPage.WelcomeMessage.Text.Trim(), Is.EqualTo($"Hello {username}!"), "The 'welcome message' is not displayed correctly.");
        }

        [Test, Order(7)]
        public void RegisterNewUserWithUsernameInBulgarian()
        {
            // Arrange
            string randomBulgarianName = registerPage.GenerateRandomBulgarianName();
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(randomBulgarianName, password, email);

            // Assert: Check that the current URL is the base URL, indicating a successful redirect
            Assert.That(driver.Url, Is.EqualTo(registerPage.BaseUrl), "The registration did not redirect to the expected page.");
            Assert.IsTrue(registerPage.WelcomeMessage.Displayed, "The 'welcome message' is not displayed correctly.");
            Assert.That(registerPage.WelcomeMessage.Text.Trim(), Is.EqualTo($"Hello {randomBulgarianName}!"), "The 'welcome message' is not displayed correctly.");
        }

        [Test, Order(8)]
        public void RegisterNewUserWithOnlyDidgitsInUsername()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsernameContainingOnlyDigits();
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert: Check that the current URL is the base URL, indicating a successful redirect
            Assert.That(driver.Url, Is.EqualTo(registerPage.BaseUrl), "The registration did not redirect to the expected page.");
            Assert.IsTrue(registerPage.WelcomeMessage.Displayed, "The 'welcome message' is not displayed correctly.");
            Assert.That(registerPage.WelcomeMessage.Text.Trim(), Is.EqualTo($"Hello {username}!"), "The 'welcome message' is not displayed correctly.");
        }


        [Test, Order(9)]
        public void RegisterNewUserWithUnicodeSymbolInUsername()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername() + "😊";
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();

            try
            {
                // Try to perform registration with Unicode in the username
                registerPage.PerformRegistration(username, password, email);

                // If no exception occurs, fail the test since the exception is expected
                Assert.Fail("The test did not throw the expected WebDriverException.");
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                // Assert
                Assert.That(ex.Message, Does.Contain("ChromeDriver only supports characters in the BMP"),
                    "The WebDriverException was thrown, but the error message did not match the expected BMP limitation.");
            }
        }
        
        [Test, Order(10)]
        public void RegisterNewUserWithEmptyPassword()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert
            Assert.IsTrue(registerPage.PasswordEmptyRegisterError.Displayed, "The 'password required' error message was not displayed.");
            Assert.That(registerPage.PasswordEmptyRegisterError.Text.Trim(), Is.EqualTo("The Password field is required."), "The 'password required' error message was not displayed.");
        }

        [Test, Order (11)]
        public void RegisterNewUserWithTooShortPassword5Chars()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Tes1!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //Assert
            Assert.IsTrue(registerPage.PasswordTooShortError.Displayed, "The 'password  must be at least 6 characters long' error message was not displayed.");
            Assert.That(registerPage.PasswordTooShortError.Text.Trim(), Is.EqualTo("The Password must be at least 6 characters long."), "The 'password  must be at least 6 characters long' error message was not displayed.");
        }

        [Test, Order(12)]
        public void RegisterNewUserWithPasswordWithASpaceInIt()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing 123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert
            Assert.That(driver.Url, Is.EqualTo(registerPage.BaseUrl), "The registration did not redirect to the expected page.");
        }

        [Test, Order (13)]
        public void RegisterNewUserWithoutLowerCaseLettersIPassword()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "TESTING123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //Assert
            Assert.IsTrue(registerPage.PasswordWithoutLowercaseError.Displayed, "The 'password  must be at least one lowercase' error message was not displayed.");
            Assert.That(registerPage.PasswordWithoutLowercaseError.Text.Trim(), Is.EqualTo("Passwords must have at least one lowercase ('a'-'z')."), "The 'at least one lowercase' error message was not displayed.");
        }


        [Test, Order(14)]
        public void RegisterNewUserWithoutUpperCaseLettersInPassword()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //Assert
            Assert.IsTrue(registerPage.PasswordWithoutUppercaseError.Displayed, "The 'password  must be at least one uppercase' error message was not displayed.");
            Assert.That(registerPage.PasswordWithoutUppercaseError.Text.Trim(), Is.EqualTo("Passwords must have at least one uppercase ('A'-'Z')."), "The 'at least one uppercase' error message was not displayed.");
        }

        [Test, Order(15)]
        public void RegisterNewUserWithoutDigitsInPassword()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //Assert
            Assert.IsTrue(registerPage.PasswordWithoutDigitsError.Displayed, "The 'at least one non letter or digit character' error message was not displayed.");
            Assert.That(registerPage.PasswordWithoutDigitsError.Text.Trim(), Is.EqualTo("Passwords must have at least one digit ('0'-'9')."), "The 'at least one non letter or digit character' error message was not displayed.");
        }

        [Test, Order(16)]
        public void RegisterNewUserWithoutNonLetterCharsInPassword()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing123";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            //Assert
            Assert.IsTrue(registerPage.PasswordWithoutSpecialCharsRegisterError.Displayed, "The 'at least one non letter or digit character' error message was not displayed.");
            Assert.That(registerPage.PasswordWithoutSpecialCharsRegisterError.Text.Trim(), Is.EqualTo("Passwords must have at least one non letter or digit character."), "The 'at least one non letter or digit character' error message was not displayed.");
        }

        [Test, Order(17)]
        public void RegisterNewUserWithValidEmail()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing123!";
            string email = registerPage.GenerateRandomEmail();
            email = $"user_{email}@gmail.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert: Check that the current URL is the base URL, indicating a successful redirect
            Assert.That(driver.Url, Is.EqualTo(registerPage.BaseUrl), "The registration did not redirect to the expected page.");
            Assert.IsTrue(registerPage.WelcomeMessage.Displayed, "The 'welcome message' is not displayed correctly.");
            Assert.That(registerPage.WelcomeMessage.Text.Trim(), Is.EqualTo($"Hello {username}!"), "The 'welcome message' is not displayed correctly.");
        }

        [Test, Order(18)]
        public void RegisterNewUserWithAlreadyRegisteredEmail()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing123!";
            string email = "lambda@test.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);
            
            // Assert
            Assert.IsTrue(registerPage.EmailAlreadyUsedError.Displayed, "The 'already used email' error message was not displayed correctly.");
            Assert.That(registerPage.EmailAlreadyUsedError.Text.Trim(), Is.EqualTo($"Email '{email}' is already taken."), "The 'already used email'error message is not displayed correctly.");

        }

        [Test, Order(19)]
        public void RegisterNewUserWithEmptyEmail()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing123!";
            string email = "";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert
            Assert.IsTrue(registerPage.EmailEmptyRegisterError.Displayed, "The 'empty email' error message was not displayed correctly.");
            Assert.That(registerPage.EmailEmptyRegisterError.Text.Trim(), Is.EqualTo("The Email field is required."), "The 'empty email' error message is not displayed correctly.");
        }

        [Test, Order(20)]
        public void RegisterNewUserWithInvalidEmailWithoutAtSign()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing123!";
            string email = "testing.com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert
            Assert.IsTrue(registerPage.InvalidEmailError.Displayed, "The 'invalid email' error message was not displayed correctly.");
            Assert.That(registerPage.InvalidEmailError.Text.Trim(), Is.EqualTo("The Email field is not a valid e-mail address."), "The 'invalid email' error message is not displayed correctly.");
        }

        [Test, Order(21)]
        public void RegisterNewUserWithInvalidEmailWithoutDot()
        {
            // Arrange
            string username = registerPage.GenerateRandomUsername();
            string password = "Testing123!";
            string email = "testing@com";

            // Act
            registerPage.OpenRegisterPage();
            registerPage.PerformRegistration(username, password, email);

            // Assert
            Assert.IsTrue(registerPage.InvalidEmailError.Displayed, "The 'invalid email' error message was not displayed correctly.");
            Assert.That(registerPage.InvalidEmailError.Text.Trim(), Is.EqualTo("The Email field is not a valid e-mail address."), "The 'invalid email' error message is not displayed correctly.");
        }
    }
}
