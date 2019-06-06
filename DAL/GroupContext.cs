using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class GroupContext
    {
        private IDataConnector dataConnector;

        public GroupContext()
        {
            dataConnector = new SQLConnector();
        }

        public GroupContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
        }
        

        //Create
        public Group CreateGroup(Group Group)
        {
            dataConnector.Insert("INSERT INTO `group` (`ID`, `Name`) VALUES (NULL, '" + Group.Name + "');");
            return GetGroup(Group);
        }

        public void CreateGroups(List<Group> Group)
        {
            foreach (Group group in Group)
            {
                CreateGroup(group);
            }
        }

        //Read
        public Group GetGroup(int ID)
        {
            if (dataConnector == null) { dataConnector = new SQLConnector(); }
            return new Group()
            {
                ID = ID,
                Name = dataConnector.Select("SELECT `Name` FROM `group` WHERE `ID` = '" + ID + "'")[0]
            };
        }

        public Group GetGroup(Group Group)
        {
            string ID = dataConnector.Select("SELECT `ID` FROM `group` WHERE `Name` = '" + Group.Name + "'")[0];
            Group.ID = Convert.ToInt32(ID);
            return Group;
        }

        public List<Group> GetAll()
        {
            List<Group> list = new List<Group>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `group`"))
            {
                list.Add(GetGroup(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateGroup(Group Group)
        {
            dataConnector.Update("UPDATE `group` SET `Name` = '" + Group.Name + "' WHERE `group`.`ID` = " + Group.ID + ";");
        }

        public void UpdateGroups(List<Group> Groups)
        {
            foreach (Group Group in Groups)
            {
                UpdateGroup(Group);
            }
        }


        //Delete

        public void DeleteGroup(int ID)
        {
            dataConnector.Delete("DELETE FROM `group` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteGroup(Group Group)
        {
            DeleteGroup(Group.ID);
        }

        public void DeleteGroups(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteGroup(ID);
            }
        }

        public void DeleteGroups(List<Group> Groups)
        {
            foreach (Group Group in Groups)
            {
                DeleteGroup(Group.ID);
            }
        }
    }
}
