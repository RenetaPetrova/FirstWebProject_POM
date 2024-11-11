using OpenQA.Selenium;

namespace FirstWebProject_POM.Pages
{
    public class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver) 
        {
            
        }

        public string Url => BaseUrl + "Account/Register";

        public IWebElement userNameInput => driver.FindElement(By.Id("UserName"));
        public IWebElement PasswordInput => driver.FindElement(By.Id("Password"));
        public IWebElement ConfirmPasswordInput => driver.FindElement(By.Id("ConfirmPassword"));
        public IWebElement EmailInput => driver.FindElement(By.Id("Email"));
        public IWebElement RegisterButton => driver.FindElement(By.XPath("//input[@class='btn btn-default']"));
        public IWebElement WelcomeMessage => driver.FindElement(By.XPath("//a[@title='Manage']"));
        public IWebElement UsernameRegisterMainError => driver.FindElement(By.XPath("//ul/li[text()='The UserName field is required.']"));
        public IWebElement UsernameTooShortRegisterError => driver.FindElement(By.XPath("//ul/li[text()='The UserName must be at least 6 charecters long.']"));
        public IWebElement PasswordEmptyRegisterError => driver.FindElement(By.XPath("//ul/li[text()='The Password field is required.']"));
        public IWebElement PasswordTooShortError => driver.FindElement(By.XPath("//ul/li[text()='The Password must be at least 6 characters long.']"));
        public IWebElement PasswordWithoutLowercaseError => driver.FindElement(By.XPath("//ul/li[text()=\"Passwords must have at least one lowercase ('a'-'z').\"]"));
        public IWebElement PasswordWithoutSpecialCharsRegisterError => driver.FindElement(By.XPath("//ul/li[text()='Passwords must have at least one non letter or digit character.']"));
        public IWebElement EmailEmptyRegisterError => driver.FindElement(By.XPath("//ul/li[text()='The Email field is required.']"));
        public IWebElement AlreadyUserUsernameError => driver.FindElement(By.XPath("//ul/li[text()='Name lambda is already taken.']"));


        public void OpenRegisterPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void PerformRegistration(string newUsername, string newPassword, string newEmail)
        {
            userNameInput.Clear();
            userNameInput.SendKeys(newUsername);
            PasswordInput.Clear();
            PasswordInput.SendKeys(newPassword);
            ConfirmPasswordInput.Clear();
            ConfirmPasswordInput.SendKeys(newPassword);
            EmailInput.Clear();
            EmailInput.SendKeys(newEmail);
            RegisterButton.Click();
        }

        public void AssertRegisterAllErrorMessages()
        {
            Assert.That(UsernameRegisterMainError.Text.Trim(), Is.EqualTo("The UserName field is required."));
            Assert.That(PasswordEmptyRegisterError.Text.Trim(), Is.EqualTo("The Password field is required."));
            Assert.That(EmailEmptyRegisterError.Text.Trim(), Is.EqualTo("The Email field is required."));
        }

        public string GenerateRandomBulgarianName()
        {
            Random random = new Random();
            string bulgarianLetters = "абвгдежзийклмнопрстуфхцчшщъьюяАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЮЯ";
            int nameLength = random.Next(6, 12); // Length of the name between 6 and 12 characters
            char[] randomName = new char[nameLength];

            for (int i = 0; i < nameLength; i++)
            {
                randomName[i] = bulgarianLetters[random.Next(bulgarianLetters.Length)];
            }

            return new string(randomName);
        }

        public string GenerateRandomEmail()
        {
            Random random = new Random();
            string emailChars = "ABCDefghigk12345!?#";
            int emailLength = random.Next(6, 12);
            char[] randomEmail = new char[emailLength];
            for (int i = 0; i < emailLength; i++)
            {
                randomEmail[i] = emailChars[random.Next(emailChars.Length)];
            }
            return new string(randomEmail);
        }

        public string GenerateRandomSpecialCharsUsername()
        {
            Random random = new Random();
            string specialChars = "!@#$%^&*";
            int usernameLength = random.Next(6, 12);
            char[] randomSpecialCharsName = new char[usernameLength];
            for (int i = 0; i < usernameLength; i++)
            {
                randomSpecialCharsName[i] = specialChars[random.Next(specialChars.Length)];
            }
            return new string(randomSpecialCharsName);
        }

        public string GenerateRandomUsernameContainingOnlyDigits()
        {
            Random random = new Random();
            string numbers = "1234567890";
            int usernameLength = random.Next(6, 12);
            char[] randomDigitsUsername = new char[usernameLength];
            for (int i = 0; i < usernameLength; i++)
            {
                randomDigitsUsername[i] = numbers[random.Next(numbers.Length)];
            }
            return new string(randomDigitsUsername);
        }

        public string GenerateRandomUsername()
        {
            Random random = new Random();
            string latinLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int nameLength = random.Next(6, 12); // Length of the name between 6 and 12 characters
            char[] randomName = new char[nameLength];

            for (int i = 0; i < nameLength; i++)
            {
                randomName[i] = latinLetters[random.Next(latinLetters.Length)];
            }

            return new string(randomName);
        }

        //public void AssertErrorPasswordRequirenments()
        //{
        //    Assert.That(UsernameOnlyDigitsRegisterError.Text.Trim(),
        //        Is.EqualTo("Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z'). " +
        //        "Passwords must have at least one uppercase ('A'-'Z')."));
        //}
    }
}
