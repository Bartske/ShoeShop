using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ShoeSizeContext
    {
        private IDataConnector dataConnector;

        public ShoeSizeContext()
        {
            dataConnector = new SQLConnector();
        }

        public ShoeSizeContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
        }


        //Create
        public void CreateShoeSize(ShoeSize ShoeSize)
        {
            dataConnector.Insert("INSERT INTO `shoesize` (`ID`, `Size`, `ProductID`, `Stock`) VALUES (NULL, '" + ShoeSize.Size + "', '" + ShoeSize.ProductID + "', '" + ShoeSize.Stock + "');");
        }

        public void CreateShoeSizes(List<ShoeSize> ShoeSize)
        {
            foreach (ShoeSize shoeSize in ShoeSize)
            {
                CreateShoeSize(shoeSize);
            }
        }

        //Read
        public ShoeSize GetShoeSize(int ID)
        {
            return new ShoeSize()
            {
                ID = ID,
                Size = Convert.ToInt32(dataConnector.Select("SELECT `Size` FROM `shoesize` WHERE `ID` = '" + ID + "'")[0]),
                ProductID = Convert.ToInt32(dataConnector.Select("SELECT `ProductID` FROM `shoesize` WHERE `ID` = '" + ID + "'")[0]),
                Stock = Convert.ToInt32(dataConnector.Select("SELECT `Stock` FROM `shoesize` WHERE `ID` = '" + ID + "'")[0])
            };
        }

        public List<ShoeSize> GetShoeSizes(int ProductID)
        {
            if (dataConnector == null) { dataConnector = new SQLConnector(); }
            List<ShoeSize> list = new List<ShoeSize>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `shoesize` WHERE `ProductID` = '"+ProductID.ToString()+"'"))
            {
                list.Add(GetShoeSize(Convert.ToInt32(ID)));
            }
            return list;
        }

        public List<ShoeSize> GetAll()
        {
            List<ShoeSize> list = new List<ShoeSize>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `shoesize`"))
            {
                list.Add(GetShoeSize(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateShoeSize(ShoeSize ShoeSize)
        {
            dataConnector.Update("UPDATE `shoesize` SET `Size` = '" + ShoeSize.Size + "',`ProductID`= '" + ShoeSize.ProductID + "', `Stock` = '" + ShoeSize.Stock + "' WHERE `shoesize`.`ID` = " + ShoeSize.ID + ";");
        }

        public void UpdateShoeSizes(List<ShoeSize> ShoeSizes)
        {
            foreach (ShoeSize ShoeSize in ShoeSizes)
            {
                UpdateShoeSize(ShoeSize);
            }
        }


        //Delete

        public void DeleteShoeSize(int ID)
        {
            dataConnector.Delete("DELETE FROM `shoesize` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteShoeSize(ShoeSize ShoeSize)
        {
            DeleteShoeSize(ShoeSize.ID);
        }

        public void DeleteShoeSizes(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteShoeSize(ID);
            }
        }

        public void DeleteShoeSizes(List<ShoeSize> ShoeSizes)
        {
            foreach (ShoeSize ShoeSize in ShoeSizes)
            {
                DeleteShoeSize(ShoeSize.ID);
            }
        }
    }
}
