using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ShoeLogic
    {
        ShoeContext shoeContext = new ShoeContext();
        BrandContext brandContext = new BrandContext();
        GroupContext groupContext = new GroupContext();

        public ShoeLogic()
        {

        }

        public List<Shoe> GetProductResults(string brand, string group, string Color, string sort, int NumOfResults, int Page)
        {
            List<Shoe> list = GetProductFromFilter(brand, group, Color, sort);

            int start = (Page * NumOfResults) - NumOfResults;
            int end = (Page * NumOfResults);

            return list.Skip(start).Take(end).ToList();
        }

        public List<Shoe> GetProductFromFilter(string brand, string group, string Color, string sort)
        {
            List<Shoe> list = shoeContext.GetAll();

            if (group != "")
            {
                list = list.Where(shoe => shoe.Group.Name == group).ToList();
            }
            if (brand != "")
            {
                list = list.Where(shoe => shoe.Brand.Name == brand).ToList();
            }
            if (Color != "")
            {
                list = list.Where(shoe => shoe.Color == Color).ToList();
            }
            switch (sort)
            {
                case "Alpabetical":
                    list = list.OrderBy(Shoe => Shoe.Name).ToList();
                    break;
                case "Price":
                    list = list.OrderBy(Shoe => Shoe.Price).ToList();
                    break;
                default:
                    list = list.OrderBy(Shoe => Shoe.Name).ToList();
                    break;
            }

            return list.ToList();
        }

        public List<Shoe> GetBestSellingShoes()
        {
            //Change later
            return shoeContext.GetAll().OrderBy(s => s.DateAdded).Take(6).ToList();
        }

        public List<Shoe> GetShoes()
        {
            return shoeContext.GetAll();
        }

        public Shoe GetShoe(int id)
        {
            return shoeContext.GetShoe(id);
        }

        public List<Shoe> GetLatestShoes()
        {
            return shoeContext.GetAll().OrderBy(s => s.DateAdded).Take(6).ToList();
        }

        public List<Brand> GetBrands()
        {
            return brandContext.GetAll();
        }

        public List<Group> GetGroups()
        {
            return groupContext.GetAll();
        }

        public List<string> GetColors()
        {
            return shoeContext.GetFields("SELECT DISTINCT(`Color`) FROM `shoe`");
        }

        public void CreateShoe(Shoe shoe)
        {
            shoeContext.CreateShoe(shoe);
        }

        public void EditShoe(Shoe shoe)
        {
            shoeContext.UpdateShoe(shoe);
        }

        public void DeleteShoe(int id)
        {
            shoeContext.DeleteShoe(id);
        }


    }
}
