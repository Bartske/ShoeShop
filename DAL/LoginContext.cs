using DAL.DataConnectors;
using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DAL
{
    public class LoginContext
    {
        private IDataConnector dataConnector;

        public LoginContext()
        {
            dataConnector = new SQLConnector();
        }

        public LoginContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
        }

        //Check
        public int GetLoginID(string Username)
        {
            return Convert.ToInt32(dataConnector.Select("SELECT `ID` FROM `login` WHERE `Username` = '" + Username + "'")[0]);
        }

        public bool CheckLogin(string Username, string HashedPassword)
        {
            return dataConnector.Select("SELECT `ID` FROM `login` WHERE `Username` = '"+Username+"' AND `Password` = '"+HashedPassword+"'").Count > 0;
        }

        //Create
        public Login CreateLogin(Login Login)
        {
            dataConnector.Insert("INSERT INTO `login` (`ID`, `Username`, `Password`, `Salt`) VALUES (NULL, '"+Login.UserName+"', '"+Login.HashedPassword+"', '"+Login.Salt+"');");
            return GetLogin(Login);
        }

        public void CreateLogins(List<Login> Login)
        {
            foreach (Login login in Login)
            {
                CreateLogin(login);
            }
        }

        //Read
        public Login GetLogin(int ID)
        {
            return new Login()
            {
                ID = ID,
                UserName = dataConnector.Select("SELECT `Username` FROM `login` WHERE `ID` = '" + ID + "'")[0],
                Password = "",
                HashedPassword = dataConnector.Select("SELECT `Password` FROM `login` WHERE `ID` = '" + ID + "'")[0],
                Salt = dataConnector.Select("SELECT `Salt` FROM `login` WHERE `ID` = '" + ID + "'")[0]
            };
        }

        public Login GetLogin(Login Login)
        {
            string ID = dataConnector.Select("SELECT `ID` FROM `login` WHERE `Username` = '"+Login.UserName+"' AND `Password` = '"+Login.HashedPassword+"' AND `Salt` = '"+Login.Salt+"'")[0];
            Login.ID = Convert.ToInt32(ID);
            return Login;
        }

        public Login GetLogin(string Username, string HashedPassword)
        {
            int ID = Convert.ToInt32(dataConnector.Select("SELECT `ID` FROM `login` WHERE `Username` = '" + Username + "' and `Password` = '" + HashedPassword + "'")[0]);
            return GetLogin(ID);
        }
        
        public List<Login> GetAll()
        {
            List<Login> list = new List<Login>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `login`"))
            {
                list.Add(GetLogin(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateLogin(Login Login)
        {
            dataConnector.Update("UPDATE `login` SET `Username` = '" + Login.UserName + "', `Password` = '" + Login.HashedPassword + "', `Salt` = '" + Login.Salt + "' WHERE `Login`.`ID` = " + Login.ID + ";");
        }

        public void UpdateLogins(List<Login> Logins)
        {
            foreach (Login Login in Logins)
            {
                UpdateLogin(Login);
            }
        }


        //Delete

        public void DeleteLogin(int ID)
        {
            dataConnector.Delete("DELETE FROM `login` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteLogin(Login Login)
        {
            DeleteLogin(Login.ID);
        }

        public void DeleteLogins(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteLogin(ID);
            }
        }

        public void DeleteLogins(List<Login> Logins)
        {
            foreach (Login Login in Logins)
            {
                DeleteLogin(Login.ID);
            }
        }
    }
}
