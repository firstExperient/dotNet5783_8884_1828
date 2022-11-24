using BlApi;
using BO;
using Dal;
using DO;
using System.Collections.Generic;

namespace BlImplementation
{
    internal class Order
    {
        private DalApi.IDal Dal = new DalList();

        #region GET

        public IEnumerable<OrderForList> GetAll()
        {
            List<DO.Order> dalOrders = (List<DO.Order>)Dal.Order.GetAll();
            List<BO.OrderForList> blOrders = new List<BO.OrderForList>();
            double totalPrice = 0; // fix this

            foreach (DO.Order item in dalOrders)
            {
                blOrders.Add(new BO.OrderForList()
                {
                    ID = item.ID,
                    CustomerName = item.CustomerName,
                    // Status=BO.Enums.OrderStatus, //fix this
                    AmountOfItems = dalOrders.Count,
                    TotalPrice = totalPrice
                });
            }
            return blOrders;
        }

        public BO.Order Get(int id)
        {
            if (id > 0) throw new Exception();//fix this
            {
                BO.Order order;
                try
                {
                    DO.Order dalOrder = Dal.Order.Get(id);

                    order = new BO.Order()
                    {
                        ID = dalOrder.ID,
                        CustomerName = dalOrder.CustomerName,
                        CustomerEmail = dalOrder.CustomerEmail,
                        CustomerAdress = dalOrder.CustomerAdress,
                        OrderDate = dalOrder.OrderDate,
                        // Status = BO.Enums.OrderStatus, //fix this
                        ShipDate = dalOrder.ShipDate,
                        DeliveryDate = dalOrder.DeliveryDate,
                        // Items = 
                        // TotalPrice =  הם רוצים שנבחר לבד?
                    };
                    return order;
                }
                catch (DO.NotFoundException e)
                {
                    throw new BO.NotFoundException("Order not found", e);
                }
            }
        }
        #endregion


        #region UPDATE

        public BO.Order ShipOrder(int id)
        {
            BO.Order order;

            try
            {
                DO.Order dalOrder = Dal.Order.Get(id);

                IEnumerable<DO.Order> orderList = Dal.Order.GetAll();

                int numOfOrders = orderList.Count();

            } catch (Exception)
            {
                //fix this
                throw;
            }
            return order;
        }
    
        public Order DeliverOrder(int id)
        {
            DO.Order DalOrder = Dal.Order.Get(id);
            BO.Order order = DalOrder.Items cart.Items.Find((x) => x.ID == id);


            for (int i = 0; i <BO.Order cart.Items.Count; i++)
            {
                if (cart.Items[i].ProductId == id)//the product is already in cart
                {
                    //fix this
                    //האם הכמות היא לפי הכמות הכללית פחות מה שכבר יש או רק פחות האחד שהוא מוסיף?
                }
            }
        }

        public void UpdateOrder(Order order)
        {

        }

        #endregion

        #region TRACK

        public OrderTracking TrackOrder(int id)
        {
            BO.OrderTracking orderTracking;
            try
            {
                DO.Order dalOrder = Dal.Order.Get(id);

                orderTracking = new BO.OrderTracking()
                {
                    ID = dalOrder.ID,
                 //   Status =  (BO.OrderStatus)// I don't know how to continue,

                };
                return orderTracking;
            }
            catch (DO.NotFoundException e)
            {
                throw new BO.NotFoundException("order not found", e);
            }
        }
        #endregion
    }
}
