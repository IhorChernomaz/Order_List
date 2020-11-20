using Order_List.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static Order_List.Models.OrderListVM;

namespace Order_List.Controllers
{
    public class ProductController : ApiController
    {
        // GET: api/products
        [HttpGet]
        [Route("api/products")]
        public List<ProductModel> GetProducts()
        {
            using (OrderListContext db = new OrderListContext())
            {
                return db.Products.Select(p => 
                    new ProductModel { Id = p.Id, Name = p.Name, Price = p.Price, PhotoUrl = p.PhotoUrl }).ToList();
            }
        }
    }
}
