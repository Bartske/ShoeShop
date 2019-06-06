using System;
using System.Collections.Generic;
using System.Text;
using DAL.DataConnectors;
using Models;

namespace DAL
{
    public class AccountContext
    {
        private readonly IDataConnector dataConnector;
        private LoginContext loginContext;
        private BagItemContext bagItemContext;

        public AccountContext()
        {
            dataConnector = new SQLConnector();
            loginContext = new LoginContext();
            bagItemContext = new BagItemContext();
        }

        public AccountContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
            loginContext = new LoginContext(dataConnector);
            bagItemContext = new BagItemContext(dataConnector);
        }

        //Create
        public Account CreateAccount(Account Account)
        {
            Account.PhoneNumber = Convert.ToInt32(Account.PhoneNumber.ToString().Replace(" ", string.Empty));
            dataConnector.Insert("INSERT INTO `account` (`ID`, `FirstName`, `MiddleName`, `LastName`, `PhoneNumber`, `Email`, `Country`, `Address`, `City`, `ZIPcode`, `LoginID`, `Admin`) VALUES (NULL, '"+Account.FirstName+ "', '" + Account.MiddleName + "', '" + Account.LastName + "', '" + Account.PhoneNumber + "', '" + Account.Email + "', '" + Account.Country + "', '" + Account.Address + "', '" + Account.City + "', '" + Account.ZIPcode + "', '" + Account.Login.ID + "', '0');");
            return GetAccount(Account);
        }

        public void CreateAccounts(List<Account> Account)
        {
            for (int i = 0; i < Account.Count; i++)
            {
                CreateAccount(Account[i]);
            }
        }

        //Read
        public Account GetAccount(int ID)
        {
            return new Account()
            {
                ID = ID,
                FirstName = dataConnector.Select("SELECT `FirstName` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                MiddleName = dataConnector.Select("SELECT `MiddleName` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                LastName = dataConnector.Select("SELECT `LastName` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                Email = dataConnector.Select("SELECT `Email` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                PhoneNumber = Convert.ToInt32(dataConnector.Select("SELECT `PhoneNumber` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0]),
                Address = dataConnector.Select("SELECT `Address` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                City = dataConnector.Select("SELECT `City` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                Country = dataConnector.Select("SELECT `Country` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                ZIPcode = dataConnector.Select("SELECT `ZIPcode` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0],
                Login = loginContext.GetLogin(Convert.ToInt32(dataConnector.Select("SELECT `LoginID` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0])),
                Admin = dataConnector.Select("SELECT `Admin` FROM `account` WHERE `ID` = '" + ID.ToString() + "'")[0] == "1" ? true : false,
                Bag = bagItemContext.GetItemsFromUser(ID)
            };
        }

        public Account GetAccount(Account account)
        {
            account.ID = Convert.ToInt32(dataConnector.Select("SELECT `ID` FROM `account` WHERE `FirstName` = '"+account.FirstName+"' AND `MiddleName` = '"+account.MiddleName+"' AND `LastName` = '"+account.LastName+"' AND `Email` = '"+account.Email+"'")[0]);
            return account;
        }

        public Account GetAccount(string Username, string HashedPassword)
        {
            int LoginID = loginContext.GetLoginID(Username);
            return GetAccount(Convert.ToInt32(dataConnector.Select("SELECT `ID` FROM `account` WHERE `LoginID` = '" + LoginID.ToString() + "'")[0]));
        }
        
        public List<Account> GetAll()
        {
            List<Account> list = new List<Account>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `account`"))
            {
                list.Add(GetAccount(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateAccount(Account Account)
        {
            string admin;
            if (Account.Admin) { admin = "1"; } else { admin = "0"; }
            dataConnector.Update("UPDATE `account` SET `FirstName` = '"+Account.FirstName+ "', `MiddleName` = '" + Account.MiddleName + "', `LastName` = '" + Account.LastName + "', `PhoneNumber` = '" + Account.PhoneNumber + "', `Email` = '" + Account.Email + "', `Country` = '" + Account.Country + "', `Address` = '" + Account.Address + "', `City` = '" + Account.City + "', `ZIPcode` = '" + Account.ZIPcode + "', `Admin` = '"+admin+"' WHERE `account`.`ID` = " + Account.ID + ";");
        }

        public void UpdateAccounts(List<Account> Accounts)
        {
            foreach (Account account in Accounts)
            {
                UpdateAccount(account);
            }
        }
        

        //Delete

        public void DeleteAccount(int ID)
        {
            dataConnector.Delete("DELETE FROM `account` WHERE `ID` = '"+ID.ToString()+"'");
        }

        public void DeleteAccount(Account Account)
        {
            DeleteAccount(Account.ID);
        }

        public void DeleteAccounts(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteAccount(ID);
            }
        }

        public void DeleteAccounts(List<Account> Accounts)
        {
            foreach (Account account in Accounts)
            {
                DeleteAccount(account.ID);
            }
        }
    }
}
