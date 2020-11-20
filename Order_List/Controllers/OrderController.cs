using Order_List.Entities;
using System.Collections.Generic;
using System.Web.Http;
using static Order_List.Models.OrderListVM;

namespace Order_List.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        [Route("api/orders")]
        public List<OrderModel> GetOrders()
        {
            OrderRepository repo = new OrderRepository();
            OrderModel model = new OrderModel();
            var order = repo.GetAll();
            return model.GetOrderModelList(order);
        }

        [HttpPost]
        [Route("api/orders/add")]
        public OrderModel Add(OrderModel model)
        {
            OrderRepository repo = new OrderRepository();
            Order order = repo.Create(model.GetOrder(model));
            repo.Save();
            return model.GetOrderModel(order);
        }

        [HttpPost]
        [Route("api/orders/update")]
        public OrderModel Update(OrderModel model)
        {
            OrderRepository repo = new OrderRepository();
            repo.Update(model.GetOrder(model));
            repo.Save();
            return model;
        }

        [HttpPost]
        [Route("api/orders/delete/{id}")]
        public OrderModel Delete(int id)
        {
            OrderRepository repo = new OrderRepository();
            OrderModel model = new OrderModel();
            Order order = repo.Delete(id);
            return model.GetOrderModel(order);
        }
    }
}
