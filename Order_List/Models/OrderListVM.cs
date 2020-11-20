using Order_List.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Order_List.Models
{
    public class OrderListVM
    {
        public class StatusModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public List<StatusModel> GetStatusModelList(IEnumerable<Status> list)
            {
                var modelList = new List<StatusModel>();
                foreach(var item in list)
                {
                    modelList.Add(new StatusModel { Id = item.Id, Name = item.Name });
                }
                return modelList;
            }
        }

        public class ProductModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string PhotoUrl { get; set; }

            public List<ProductModel> GetProductModelList(IEnumerable<Product> list)
            {
                var modelList = new List<ProductModel>();
                foreach (var item in list)
                {
                    modelList.Add(new ProductModel 
                    {
                        Id = item.Id, Name = item.Name, Price = item.Price, PhotoUrl = item.PhotoUrl 
                    });
                }
                return modelList;
            }
        }

        public class OrderModel
        {
            public int Id { get; set; }
            public int Count { get; set; }
            public int ProductId { get; set; }
            public int StatusId { get; set; }

            public Order GetOrder(OrderModel model) => 
                new Order { Id = model.Id, Count = model.Count, ProductId = model.ProductId, StatusId = model.StatusId };
            public OrderModel GetOrderModel(Order model) =>
                new OrderModel { Id = model.Id, Count = model.Count, ProductId = model.ProductId, StatusId = model.StatusId };
            public List<OrderModel> GetOrderModelList(IEnumerable<Order> list)
            {
                return list.Select(l => new OrderModel { Id = l.Id, Count = l.Count, StatusId = l.StatusId, ProductId = l.ProductId }).ToList();
            }
        }
    }
}