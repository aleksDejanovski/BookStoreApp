using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Book_Store_app
{
    [TestFixture]
    public class Book_Store_TestCases : BaseTestClass

    {

        [Test]
        [Category("REGISTRATION")]
        public void GoToLoginPage()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            Assert.IsTrue(driver.Url.Contains("login"));
        }
        [Test]
        [Category("REGISTRATION")]
        public void GoToRegistrationPage()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.ClickNewUser();
            Assert.IsTrue(driver.Url.Contains("register"));

        }
        [Test]
        [Category("REGISTRATION")]
        public void VerifyThatUserCanRegisterUsingValidData()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.ClickNewUser();
            pageRegister.EnterFields("aleksandar", "dejanovski", "usernamee", "12345678!!21212");
            pageRegister.frameSwitch();
            pageRegister.ClickCheckBox();
            pageRegister.ClickRegisterButton();


            //NOTE AFTER CAPCHA GETS CLICKED IMAGES APPEAR SO I CAN NOT AUTOMATE THAT !!!!

            var alert = driver.SwitchTo().Alert();
            Assert.IsTrue(alert.Text.Contains("User Register Successfully"));

        }
        [Test]
        [Category("REGISTRATION")]
        public void VerifyThatUserIsNotAbleToRegisterWithEmptyForm()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.ClickNewUser();
            pageRegister.EnterFields("", "", "", "");
            pageRegister.frameSwitch();
            pageRegister.ClickCheckBox();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2500);
            pageRegister.ClickRegisterButton();
            Assert.IsTrue(pageLogin.EmptyForm.Displayed);


            //NOTE AFTER CAPCHA GETS CLICKED IMAGES APPEAR SO I CAN NOT AUTOMATE THAT !!!!


        }
        [Test]
        [Category("REGISTRATION")]
        public void VerifyThatUserIsNotAbleToRegisterWithNotValidPassword()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.ClickNewUser();
            pageRegister.EnterFields("marko", "markovski", "aleks112", "33");
            pageRegister.frameSwitch();
            pageRegister.ClickRegisterButton();
            Assert.IsTrue(pageRegister.WrongPassMessage.Text.Contains("Passwords must have at least one non alphanumeric character, one digit "));

            // NOTE CAPCHA ELEMENT OVERLAPS THE REGISTER BUTON SO SELENIUM CAN CLICK IT AND THROWS EX

            //NOTE AFTER CAPCHA GETS CLICKED IMAGES APPEAR SO I CAN NOT AUTOMATE THAT !!!!



        }
        [Test]
        [Category("LOGIN")]
        public void VerifyThatUserIsAbleToLoginUsingValidData()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            Assert.AreEqual("user", pageProf.userNameForVerify.Text);

        }
        [Test]
        [Category("LOGIN")]
        public void VerifyThatUserIsNOTAbleToLoginUsingInvalidPass()
        { 
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, "22");
            pageLogin.ClickLoginButton();
            Assert.AreEqual("Invalid username or password!", pageLogin.ErrorLoginMessage.Text);

        }
        [Test]
        [Category("LOGIN")]
        public void VerifyThatUserIsNOTAbleToLoginUsingInvalidUserName()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass("1123!", pass);
            pageLogin.ClickLoginButton();
            Assert.AreEqual("Invalid username or password!", pageLogin.ErrorLoginMessage.Text);

        }
        [Test]
        [Category("LOGIN")]
        public void VerifyThatUserIsNOTAbleToLoginUsingEmptyForm()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass("", "");
            pageLogin.ClickLoginButton();
            Assert.IsTrue(pageLogin.EmptyForm.Displayed);

        }
        [Test]
        [Category("PROFILE")]
        public void VerifyThatUserIsAbleToLogOut()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.ClickLogOut();
            Assert.AreEqual("Welcome,", pageLogin.WelcomeText.Text);

        }
        [Test]
        [Category("PROFILE")]
        public void VerifyThatUserIsAbleToVisitProfilePage()
        { 
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageLogin.ProfileEle.Click();
            Assert.IsTrue(driver.Url.Contains("profile"));


        }
        [Test]
        [Category("BOOKSTORE")]
        public void VerifyThatUserIsAbleToClickBook()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.ClickBook();
            Assert.AreEqual("ISBN :", pageProf.Isbn.Text);

        }

        [Test]
        [Category("BOOKSTORE")]
        public void VerifyThatUserIsAbleAddBookToCollection()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.ClickBook();
            actions.SendKeys(Keys.PageDown).Build().Perform();
            Thread.Sleep(2000);
            pageProf.ClickAddColection();  
            Thread.Sleep(2000);
            var alert = driver.SwitchTo().Alert();
            Assert.IsTrue(alert.Text.Contains("collection"));


        }
        [Test]
        [Category("BOOKSTORE")]
        public void VerifyThatUserIsAbleToSearchaBook()
        {
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.SearchItem("Speaking JavaScript");
            Assert.IsTrue(pageProf.SeachJavaBook.Text.Contains("Speaking JavaScript"));


        }

        [Test]
        [Category("BOOKSTORE")]
        public void VerifyThatUserIsAbleToClickSearchedBook()
        {
            
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.SearchItem("Speaking JavaScript");
            pageProf.ClickSeachedBook();
            Assert.AreEqual("ISBN :", pageProf.Isbn.Text);

        }
        [Test]
        [Category("BOOKSTORE")]
        public void VerifyThatUserIsAbleToSortCollectionByClickingAuthor()
        {
            
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.ClickAuthor();
            Assert.IsTrue(pageProf.AuthorName.Text.Contains("Axel"));

        }
        [Test]
        [Category("BOOKSTORE")]
        public void VerifyThatCorectBookIsShownAtBookDetailsPage()
        {
            
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass(username, pass);
            pageLogin.ClickLoginButton();
            pageProf.SearchItem("git pocket");
            pageProf.ClickGitBook();
            Assert.IsTrue(pageProf.NameOfBookGit.Text.Contains("e"));

        }
        [Test]
        [Category("PROFILE")]
        public void VerifyThatUserIsAbleToDeleteAllBooks()
        {
            
            pageMain.GoTo();
            pageMain.ClickLogin();
            pageLogin.EnterUserAndPass("user1", pass);
            pageLogin.ClickLoginButton();
            Thread.Sleep(2000);
            pageLogin.ProfileEle.Click();
            actions.SendKeys(Keys.PageDown).Build().Perform();
            Thread.Sleep(2000);
            pageProf.ClickDelete();
            Assert.IsTrue(pageProf.DeleteElementVerify.Text.Contains("Delete"));


        }
    }
}