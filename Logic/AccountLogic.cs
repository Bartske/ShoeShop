using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DAL;

namespace Logic
{
    public class AccountLogic
    {
        LoginContext loginContext;
        AccountContext accountContext;
        EmailLogic emailLogic;

        public AccountLogic()
        {
            loginContext = new LoginContext();
            accountContext = new AccountContext();
            emailLogic = new EmailLogic();
        }

        public void ResetPassword(int AccountID)
        {
            Account account = accountContext.GetAccount(AccountID);
            
            string newPassword = Encrypter.Encrypt("ShoeShop01!", account.Login.Salt);

            string subject = "Recover password for Shoe Shop";
            string message = "Dear " + account.Login.UserName + "," + Environment.NewLine +
                Environment.NewLine +
                "You have requested a new password." + Environment.NewLine +
                "Your new password is : ShoeShop01!" + Environment.NewLine +
                Environment.NewLine +
                "With kind regards," + Environment.NewLine +
                "Shoe Shop";
            account.Login.HashedPassword = newPassword;

            accountContext.UpdateAccount(account);
            emailLogic.SendEmail(account.Email, subject, message);
        }

        public Login CreateLogin(Login login)
        {
            login.Salt = Encrypter.RandomString(251);
            login.HashedPassword = Encrypter.Encrypt(login.Password, login.Salt);

            return loginContext.CreateLogin(login);
        }

        public Account CreateAccount(Account account)
        {
            return accountContext.CreateAccount(account);
        }

        public Account GetAccount(int LoginID)
        {
            Login login = loginContext.GetLogin(LoginID);
            return accountContext.GetAccount(login.UserName, login.HashedPassword);
        }

        public Account GetAccountByID(int ID)
        {
            return accountContext.GetAccount(ID);
        }

        public List<Account> GetAllAccounts()
        {
            return accountContext.GetAll();
        }

        public Login CheckLogin(Login login)
        {
            if (login.UserName == "")
            {
                throw new Exception("Username cannot be empty");
            }
            else if (login.Password == "")
            {
                throw new Exception("Password cannot be empty");
            }

            int LoginID = loginContext.GetLoginID(login.UserName);
            login.ID = LoginID;
            if (LoginID == 0)
            {
                throw new Exception("This user is not in our system!");
            }

            string Salt = loginContext.GetLogin(LoginID).Salt;

            //Het Wachtwoord Encrypten
            string HashedPassword = Encrypter.Encrypt(login.Password, Salt);
            login.HashedPassword = HashedPassword;

            //Kijken of de gebruikersnaam en het wachtwoord overeenkomen
            if (!loginContext.CheckLogin(login.UserName, HashedPassword))
            {
                throw new Exception("The Password is incorrect!");
            }

            return login;
        }

        public void UpdateAccount(Account account)
        {
            if (account.Login.Password == "" || account.Login.Password == null)
            {
                account.Login.Password = accountContext.GetAccount(account.ID).Login.Password;
                account.Login.HashedPassword = accountContext.GetAccount(account.ID).Login.HashedPassword;
            }


            accountContext.UpdateAccount(account);
        }

        public void DeleteAccount(int id)
        {
            accountContext.DeleteAccount(id);
        }
    }
}
