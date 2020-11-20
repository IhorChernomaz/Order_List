using Order_List.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static Order_List.Models.OrderListVM;

namespace Order_List.Controllers
{
    public class StatusController : ApiController
    {
        // GET: api/Statuses
        [HttpGet]
        [Route("api/statuses")]
        public List<StatusModel> GetStatuses()
        {
            using(OrderListContext db = new OrderListContext())
            {
                return db.Statuses.Select(s=> new StatusModel { Id = s.Id, Name = s.Name }).ToList();
            }
        }
    }
}
