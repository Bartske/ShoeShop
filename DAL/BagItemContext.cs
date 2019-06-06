using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BagItemContext
    {
        private IDataConnector dataConnector;

        public BagItemContext()
        {
            dataConnector = new SQLConnector();
        }

        public BagItemContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
        }


        //Create
        public BagItem CreateBagItem(BagItem bagitem)
        {
            dataConnector.Insert("INSERT INTO `bagitem` (`ID`, `AccountID`, `ProductID`) VALUES (NULL, '" + bagitem.AccountID + "', '"+ bagitem.ProductID+"');");
            return GetBagItem(bagitem);
        }

        public void CreateBagItems(List<BagItem> bagitems)
        {
            foreach (BagItem bagitem in bagitems)
            {
                CreateBagItem(bagitem);
            }
        }

        //Read
        public BagItem GetBagItem(int ID)
        {
            return new BagItem()
            {
                ID = ID,
                AccountID = Convert.ToInt32(dataConnector.Select("SELECT `AccountID` FROM `bagitem` WHERE `ID` = '" + ID + "'")[0]),
                ProductID = Convert.ToInt32(dataConnector.Select("SELECT `ProductID` FROM `bagitem` WHERE `ID` = '" + ID + "'")[0])
            };
        }

        public BagItem GetBagItem(BagItem bagitem)
        {
            return GetBagItem(Convert.ToInt32(dataConnector.Select("SELECT `ID` FROM `bagitem` WHERE `AccountID` = '" + bagitem.AccountID + "' AND `ProductID` = '"+ bagitem.ProductID+"'")[0]));
        }

        public List<BagItem> GetItemsFromUser(int AccountID)
        {
            List<BagItem> list = new List<BagItem>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `bagitem` WHERE `AccountID` = '" + AccountID.ToString()+"'"))
            {
                list.Add(GetBagItem(Convert.ToInt32(ID)));
            }
            return list;
        }

        public List<BagItem> GetAll()
        {
            List<BagItem> list = new List<BagItem>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `bagitem`"))
            {
                list.Add(GetBagItem(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateBagItem(BagItem bagitem)
        {
            dataConnector.Update("UPDATE `bagitem` SET `AccountID` = '" + bagitem.AccountID + "', `ProductID` = '"+ bagitem.ProductID+ "' WHERE `bagitem`.`ID` = " + bagitem.ID + ";");
        }

        public void UpdateBagItems(List<BagItem> bagitems)
        {
            foreach (BagItem bagitem in bagitems)
            {
                UpdateBagItem(bagitem);
            }
        }


        //Delete

        public void DeleteBagItem(int ID)
        {
            dataConnector.Delete("DELETE FROM `bagitem` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteBagItem(BagItem bagitem)
        {
            DeleteBagItem(bagitem.ID);
        }

        public void DeleteBagItems(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteBagItem(ID);
            }
        }

        public void DeleteBagItems(List<BagItem> bagitems)
        {
            foreach (BagItem bagitem in bagitems)
            {
                DeleteBagItem(bagitem.ID);
            }
        }
    }
}
