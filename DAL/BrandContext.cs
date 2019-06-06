using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BrandContext
    {
        private IDataConnector dataConnector;

        public BrandContext()
        {
            dataConnector = new SQLConnector();
        }

        public BrandContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
        }


        //Create
        public Brand CreateBrand(Brand Brand)
        {
            dataConnector.Insert("INSERT INTO `brand` (`ID`, `Name`) VALUES (NULL, '" + Brand.Name + "');");
            return GetBrand(Brand);
        }

        public void CreateBrands(List<Brand> Brand)
        {
            foreach (Brand brand in Brand)
            {
                CreateBrand(brand);
            }
        }

        //Read
        public Brand GetBrand(int ID)
        {
            if (dataConnector == null) { dataConnector = new SQLConnector(); }
            return new Brand()
            {
                ID = ID,
                Name = dataConnector.Select("SELECT `Name` FROM `brand` WHERE `ID` = '" + ID + "'")[0]
            };
        }

        public Brand GetBrand(Brand Brand)
        {
            string ID = dataConnector.Select("SELECT `ID` FROM `brand` WHERE `Name` = '" + Brand.Name + "'")[0];
            Brand.ID = Convert.ToInt32(ID);
            return Brand;
        }

        public List<Brand> GetAll()
        {
            List<Brand> list = new List<Brand>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `brand`"))
            {
                list.Add(GetBrand(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateBrand(Brand Brand)
        {
            dataConnector.Update("UPDATE `brand` SET `Name` = '" + Brand.Name + "' WHERE `brand`.`ID` = " + Brand.ID + ";");
        }

        public void UpdateBrands(List<Brand> Brands)
        {
            foreach (Brand Brand in Brands)
            {
                UpdateBrand(Brand);
            }
        }


        //Delete

        public void DeleteBrand(int ID)
        {
            dataConnector.Delete("DELETE FROM `brand` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteBrand(Brand Brand)
        {
            DeleteBrand(Brand.ID);
        }

        public void DeleteBrands(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteBrand(ID);
            }
        }

        public void DeleteBrands(List<Brand> Brands)
        {
            foreach (Brand Brand in Brands)
            {
                DeleteBrand(Brand.ID);
            }
        }
    }
}
