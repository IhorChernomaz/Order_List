using Order_List.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static Order_List.Models.OrderListVM;

namespace Order_List.Controllers
{
    public class StatusController : ApiController
    {
        StatusModel _statusModel = new StatusModel();

        // GET: api/Statuses
        [HttpGet]
        [Route("api/statuses")]
        public List<StatusModel> GetStatuses()
        {
            return _statusModel.GetStatuses();
        }
    }
}
