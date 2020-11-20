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

            public List<StatusModel> GetStatuses()
            {
                using (OrderListContext db = new OrderListContext())
                {
                   return db.Statuses.Select(s => new StatusModel { Id = s.Id, Name = s.Name }).ToList();
                }
            }
        }

        public class ProductModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string PhotoUrl { get; set; }

            public List<ProductModel> GetProducts()
            {
                using (OrderListContext db = new OrderListContext())
                {
                    return db.Products.Select(p =>
                        new ProductModel { Id = p.Id, Name = p.Name, Price = p.Price, PhotoUrl = p.PhotoUrl }).ToList();
                }
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

            public List<OrderModel> GetOrders()
            {
                var repositoryOrder = new OrderRepository();
                return repositoryOrder.GetAll().Select(l => new OrderModel { Id = l.Id, Count = l.Count, StatusId = l.StatusId, ProductId = l.ProductId }).ToList();
            }

            public OrderModel AddOrder(OrderModel model)
            {
                var repositoryOrder = new OrderRepository();
                Order order = repositoryOrder.Create(GetOrder(model));
                repositoryOrder.Save();
                return GetOrderModel(order);
            }

            public OrderModel UpdateOrder(OrderModel model)
            {
                var repositoryOrder = new OrderRepository();
                repositoryOrder.Update(GetOrder(model));
                repositoryOrder.Save();
                return model;
            }

            public OrderModel DeleteOrder(int id)
            {
                var repositoryOrder = new OrderRepository();
                Order order = repositoryOrder.Delete(id);
                repositoryOrder.Save();
                return GetOrderModel(order);
            }
        }
    }
}