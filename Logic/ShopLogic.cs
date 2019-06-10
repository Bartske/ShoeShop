using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DAL;

namespace Logic
{
    public class ShopLogic
    {
        BagItemContext bagItemContext;
        AccountContext accountContext;
        OrderContext orderContext;
        OrderItemContext orderItemContext;

        public ShopLogic()
        {
            bagItemContext = new BagItemContext();
            accountContext = new AccountContext();
            orderContext = new OrderContext();
            orderItemContext = new OrderItemContext();
        }

        public void AddToBag(int ProductID, int AccountID)
        {
            BagItem bagItem = new BagItem()
            {
                AccountID = AccountID,
                ProductID = ProductID
            };

            bagItemContext.CreateBagItem(bagItem);
        }

        public List<BagItem> GetBagItems(int AccountID)
        {
            return bagItemContext.GetItemsFromUser(AccountID);
        }

        public void CreateOrder(int accountID)
        {
            int orderID = orderContext.CreateOrder(new Order()
            {
                AccountID = accountID
            }).ID;

            List<OrderItem> items = new List<OrderItem>();

            foreach (BagItem Bagitem in GetBagItems(accountID))
            {
                items.Add(
                    new OrderItem()
                    {
                        OrderID = orderID,
                        ProductID = Bagitem.ProductID
                    }
                 );
            }
            orderItemContext.CreateOrderItems(items);

        }

        public List<Order> GetAllOrder()
        {
            return orderContext.GetAll();
        }

        public List<Order> GetOrderFromUser(int AccountID)
        {
            return orderContext.GetOrderFromUser(AccountID);
        }

        public void DeleteBag(int accountID)
        {
            bagItemContext.DeleteBagItems(bagItemContext.GetItemsFromUser(accountID));
        }

        public void CompleteOrder(int OrderID)
        {
            orderContext.DeleteOrder(OrderID);
        }
    }
}
