using Order_List.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static Order_List.Models.OrderListVM;

namespace Order_List.Controllers
{
    public class ProductController : ApiController
    {
        ProductModel _model = new ProductModel();

        // GET: api/products
        [HttpGet]
        [Route("api/products")]
        public List<ProductModel> GetProducts()
        {
            return _model.GetProducts();
        }
    }
}
