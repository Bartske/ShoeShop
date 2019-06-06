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

        public ShopLogic()
        {
            bagItemContext = new BagItemContext();
            accountContext = new AccountContext();
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
    }
}
