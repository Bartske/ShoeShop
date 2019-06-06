using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class OrderItemContext
    {
        private IDataConnector dataConnector;
        ShoeContext shoeContext;

        public OrderItemContext()
        {
            dataConnector = new SQLConnector();
            shoeContext = new ShoeContext();
        }

        public OrderItemContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
            shoeContext = new ShoeContext(dataConnector);

        }


        //Create
        public OrderItem CreateOrderItem(OrderItem Orderitem)
        {
            dataConnector.Insert("INSERT INTO `orderitem` (`ID`, `OrderID`, `ProductID`) VALUES (NULL, '" + Orderitem.OrderID + "', '" + Orderitem.ProductID + "');");
            return GetOrderItem(Orderitem);
        }

        public void CreateOrderItems(List<OrderItem> Orderitems)
        {
            foreach (OrderItem Orderitem in Orderitems)
            {
                CreateOrderItem(Orderitem);
            }
        }

        //Read
        public OrderItem GetOrderItem(int ID)
        {
            return new OrderItem()
            {
                ID = ID,
                OrderID = Convert.ToInt32(dataConnector.Select("SELECT `OrderID` FROM `orderitem` WHERE `ID` = '" + ID + "'")[0]),
                ProductID = Convert.ToInt32(dataConnector.Select("SELECT `ProductID` FROM `orderitem` WHERE `ID` = '" + ID + "'")[0]),
                shoe = shoeContext.GetShoe(Convert.ToInt32(dataConnector.Select("SELECT `ProductID` FROM `orderitem` WHERE `ID` = '" + ID + "'")[0]))
            };
        }

        public OrderItem GetOrderItem(OrderItem Orderitem)
        {
            return GetOrderItem(Convert.ToInt32(dataConnector.Select("SELECT `ID` FROM `orderitem` WHERE `OrderID` = '" + Orderitem.OrderID + "' AND `ProductID` = '" + Orderitem.ProductID + "'")[0]));
        }

        public List<OrderItem> GetItemsFromOrder(int OrderID)
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `orderitem` WHERE `OrderID` = '" + OrderID.ToString() + "'"))
            {
                list.Add(GetOrderItem(Convert.ToInt32(ID)));
            }
            return list;
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `orderitem`"))
            {
                list.Add(GetOrderItem(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateOrderItem(OrderItem Orderitem)
        {
            dataConnector.Update("UPDATE `orderitem` SET `OrderID` = '" + Orderitem.OrderID + "', `ProductID` = '" + Orderitem.ProductID + "' WHERE `orderitem`.`ID` = " + Orderitem.ID + ";");
        }

        public void UpdateOrderItems(List<OrderItem> Orderitems)
        {
            foreach (OrderItem Orderitem in Orderitems)
            {
                UpdateOrderItem(Orderitem);
            }
        }


        //Delete

        public void DeleteOrderItem(int ID)
        {
            dataConnector.Delete("DELETE FROM `orderitem` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteOrderItem(OrderItem Orderitem)
        {
            DeleteOrderItem(Orderitem.ID);
        }

        public void DeleteOrderItems(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteOrderItem(ID);
            }
        }

        public void DeleteOrderItems(List<OrderItem> Orderitems)
        {
            foreach (OrderItem Orderitem in Orderitems)
            {
                DeleteOrderItem(Orderitem.ID);
            }
        }
    }
}
