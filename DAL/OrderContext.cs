using DAL.DataConnectors;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class OrderContext
    {
        private IDataConnector dataConnector;
        AccountContext accountContext;
        OrderItemContext itemContext;

        public OrderContext()
        {
            dataConnector = new SQLConnector();
            accountContext = new AccountContext();
            itemContext = new OrderItemContext();
        }

        public OrderContext(IDataConnector dataConnector)
        {
            dataConnector = this.dataConnector;
            accountContext = new AccountContext(dataConnector);
            itemContext = new OrderItemContext(dataConnector);
        }


        //Create
        public Order CreateOrder(Order Order)
        {
            dataConnector.Insert("INSERT INTO `order` (`ID`, `AccountID`) VALUES (NULL, '" + Order.AccountID + "');");
            return GetOrder(Order);
        }

        public void CreateOrders(List<Order> Order)
        {
            foreach (Order order in Order)
            {
                CreateOrder(order);
            }
        }

        //Read
        public Order GetOrder(int ID)
        {
            if (dataConnector == null) { dataConnector = new SQLConnector(); }
            return new Order()
            {
                ID = ID,
                AccountID = Convert.ToInt32(dataConnector.Select("SELECT `AccountID` FROM `order` WHERE `ID` = '" + ID + "'")[0]),
                Account = accountContext.GetAccount(Convert.ToInt32(dataConnector.Select("SELECT `AccountID` FROM `order` WHERE `ID` = '" + ID + "'")[0])),
                items = itemContext.GetItemsFromOrder(ID)

            };
        }

        public Order GetOrder(Order Order)
        {
            string ID = dataConnector.Select("SELECT `ID` FROM `order` WHERE `AccountID` = '" + Order.AccountID + "'")[0];
            Order.ID = Convert.ToInt32(ID);
            return Order;
        }

        public List<Order> GetAll()
        {
            List<Order> list = new List<Order>();
            foreach (string ID in dataConnector.Select("SELECT `ID` FROM `order`"))
            {
                list.Add(GetOrder(Convert.ToInt32(ID)));
            }
            return list;
        }


        //Update

        public void UpdateOrder(Order Order)
        {
            dataConnector.Update("UPDATE `order` SET `AccountID` = '" + Order.AccountID + "' WHERE `order`.`ID` = " + Order.ID + ";");
        }

        public void UpdateOrders(List<Order> Orders)
        {
            foreach (Order Order in Orders)
            {
                UpdateOrder(Order);
            }
        }


        //Delete

        public void DeleteOrder(int ID)
        {
            dataConnector.Delete("DELETE FROM `order` WHERE `ID` = '" + ID.ToString() + "'");
        }

        public void DeleteOrder(Order Order)
        {
            DeleteOrder(Order.ID);
        }

        public void DeleteOrders(List<int> IDs)
        {
            foreach (int ID in IDs)
            {
                DeleteOrder(ID);
            }
        }

        public void DeleteOrders(List<Order> Orders)
        {
            foreach (Order Order in Orders)
            {
                DeleteOrder(Order.ID);
            }
        }
    }
}
