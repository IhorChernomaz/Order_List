using Order_List.Entities;
using System.Collections.Generic;
using System.Web.Http;
using static Order_List.Models.OrderListVM;

namespace Order_List.Controllers
{
    public class OrderController : ApiController
    {
        OrderModel _model = new OrderModel();

        [HttpGet]
        [Route("api/orders")]
        public List<OrderModel> GetOrders()
        {
            return _model.GetOrders();
        }

        [HttpPost]
        [Route("api/orders/add")]
        public OrderModel Add(OrderModel model)
        {
            return _model.AddOrder(model);
        }

        [HttpPost]
        [Route("api/orders/update")]
        public OrderModel Update(OrderModel model)
        {
            return _model.UpdateOrder(model);
        }

        [HttpPost]
        [Route("api/orders/delete/{id}")]
        public OrderModel Delete(int id)
        {
            return _model.DeleteOrder(id);
        }
    }
}
