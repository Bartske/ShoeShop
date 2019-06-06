using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ShoeContext
    {
        private IDataConnector dataConnector;
        private GroupContext groupContext;
        private BrandContext brandContext;
        private ShoeSizeContext shoeSizeContext;

        public ShoeContext()
        {
            dataConnector = new SQLConnector();
            groupContext = new GroupContext(dataConnector);
            brandContext = new BrandContext(dataConnector);
            shoeSizeContext = new ShoeSizeContext(dataConnector);
        }

        public ShoeContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
            groupContext = new GroupContext(this.dataConnector);
            brandContext = new BrandContext(this.dataConnector);
            shoeSizeContext = new ShoeSizeContext(this.dataConnector);
        }

        //Create
        public Shoe CreateShoe(Shoe Shoe)
        {
            string sale = (Shoe.Sale) ? "0" : "1";
            string date = DateTime.Now.ToString();
            dataConnector.Insert("INSERT INTO `shoe`(`ID`, `img`, `Name`, `Price`, `OldPrice`, `GroupID`, `BrandID`, `Color`, `Sale`,`DateAdded`, `Description`) VALUES (NULL,'"+Shoe.img+ "','" + Shoe.Name + "','" + Shoe.Price + "','" + Shoe.OldPrice + "','" + Shoe.Group.ID + "','" + Shoe.Brand.ID + "','" + Shoe.Color + "','" + sale + "','" + date + "','" + Shoe.Description + "')");
            return GetShoe(Shoe);
        }

        public void CreateShoes(List<Shoe> Shoes)
        {
            foreach (Shoe Shoe in Shoes)
            {
                CreateShoe(Shoe);
            }
        }

        //Read
        public Shoe GetShoe(int ID)
        {
            return new Shoe()
            {
                ID = ID,
                Name = dataConnector.Select("SELECT `Name` FROM `shoe` WHERE `ID` = '" + ID + "'")[0],
                img = dataConnector.Select("SELECT `img` FROM `shoe` WHERE `ID` = '" + ID + "'")[0],
                Price = Convert.ToInt32(dataConnector.Select("SELECT `Price` FROM `shoe` WHERE `ID` = '" + ID + "'")[0]),
                OldPrice = Convert.ToInt32(dataConnector.Select("SELECT `OldPrice` FROM `shoe` WHERE `ID` = '" + ID + "'")[0]),
                Group = groupContext.GetGroup(Convert.ToInt32(dataConnector.Select("SELECT `GroupID` FROM `shoe` WHERE `ID` = '" + ID + "'")[0])),
                Brand = brandContext.GetBrand(Convert.ToInt32(dataConnector.Select("SELECT `BrandID` FROM `shoe` WHERE `ID` = '" + ID + "'")[0])),
                Color = dataConnector.Select("SELECT `Color` FROM `shoe` WHERE `ID` = '" + ID + "'")[0],
                Sale = Convert.ToBoolean(dataConnector.Select("SELECT `Sale` FROM `shoe` WHERE `ID` = '" + ID + "'")[0]),
                Description = dataConnector.Select("SELECT `Description` FROM `shoe` WHERE `ID` = '" + ID + "'")[0],
                DateAdded = Convert.ToDateTime(dataConnector.Select("SELECT `DateAdded` FROM `shoe` WHERE `ID` = '" + ID + "'")[0]),
                Sizes = shoeSizeContext.GetShoeSizes(ID)
            };
        }

        public Shoe GetShoe(Shoe Shoe)
        {
            string ID = dataConnector.Select("SELECT `ID` FROM `shoe` WHERE `Name` = '" + Shoe.Name + "' AND `Description` = '" + Shoe.Description + "'")[0];
            Shoe = GetShoe(Convert.ToInt32(ID));
            return Shoe;
        }

        public Shoe GetShoe(string Name, string Description)
        {
            Shoe shoe;
            string ID = dataConnector.Select("SELECT `ID` FROM `shoe` WHERE `Name` = '" + Name + "' AND `Description` = '" + Description + "'")[0];
            shoe = GetShoe(Convert.ToInt32(ID));
            return shoe;
        }

        public List<Shoe> GetAll()
        {
            List<Shoe> list = new List<Shoe>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `shoe`"))
            {
                list.Add(GetShoe(Convert.ToInt32(ID)));
            }
            return list;
        }

        public List<string> GetFields(string query)
        {
            return dataConnector.Select(query);
        }


        //Update

        public void UpdateShoe(Shoe Shoe)
        {
            string sale = (Shoe.Sale) ? "0" : "1";
            dataConnector.Update("UPDATE `shoe` SET `img`='" + Shoe.img + "',`Name`='" + Shoe.Name + "',`Price`='" + Shoe.Price + "',`OldPrice`='" + Shoe.OldPrice + "',`GroupID`='" + Shoe.Group.ID + "',`BrandID`='" + Shoe.Brand.ID + "',`Color`='" + Shoe.Color + "',`Sale`='" + sale + "',`Description`='" + Shoe.Description + "' WHERE `ID` = '"+Shoe.ID+"'");
        }

        public void UpdateShoes(List<Shoe> Shoes)
        {
            foreach (Shoe Shoe in Shoes)
            {
                UpdateShoe(Shoe);
            }
        }


        //Delete

        public void DeleteShoe(int ID)
        {
            dataConnector.Delete("DELETE FROM `shoe` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteShoe(Shoe Shoe)
        {
            DeleteShoe(Shoe.ID);
        }

        public void DeleteShoes(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteShoe(ID);
            }
        }

        public void DeleteShoes(List<Shoe> Shoes)
        {
            foreach (Shoe Shoe in Shoes)
            {
                DeleteShoe(Shoe.ID);
            }
        }
    }
}
