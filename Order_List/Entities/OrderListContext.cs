using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Order_List.Entities
{
    class OrderListContextInitializer : CreateDatabaseIfNotExists<OrderListContext>
    {
        protected override void Seed(OrderListContext db)
        {
            string productsJson = "[{'id':1,'name':'iPhone 11 Pro','price':1200,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone11_pro__ejujupnldouq_large.jpg'},{'id':2,'name':'iPhone 11 Pro Max','price':1500,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone11_pro_max__br42x4fh0s2u_large.jpg'},{'id':3,'name':'iPhone 11','price':920,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone11__flwmadt3fvyq_large.jpg'}," +
                "{'id':4,'name':'iPhone XS','price':1000,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphoneXS__m9rwtpu277ue_large.jpg'},{'id':5,'name':'iPhone XS Max','price':1200,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphoneXSmax__btozkqkudp1e_large.jpg'},{'id':6,'name':'iPhone XR','price':800,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphoneXR__gmfkv1py74uq_large.jpg'}," +
                "{'id':7,'name':'iPhone X','price':750,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphoneX__xc1a7uy6rciu_large.jpg'},{'id':8,'name':'iPhone 8 Plus','price':720,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone8plus__exff3jiko4a6_large.jpg'},{'id':9,'name':'iPhone 8','price':700,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone8__c1zeua0f3k8y_large.jpg'},{'id':10,'name':'iPhone 7 Plus','price':560,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone7plus__fheppwnoslqq_large.jpg'},{'id':11,'name':'iPhone 7','price':500,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone7__bm3v8zud8iuu_large.jpg'}," +
                "{'id':12,'name':'iPhone 6s Plus','price':400,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone6splus__c3os19tmy5si_large.jpg'},{'id':13,'name':'iPhone 6s','price':360,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone6s__b5inxu5uh1de_large.jpg'},{'id':14,'name':'iPhone 6 Plus','price':350,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone6plus__cwa231d66auu_large.jpg'},{'id':15,'name':'iPhone 6','price':340,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphone6__bdf10vcqh3zm_large.jpg'},{'id':16,'name':'iPhone SE','price':300,'photoUrl':'https://www.apple.com/v/iphone/compare/n/images/overview/all_models_iphoneSE__bdubwl00w3o2_large.jpg'}]";
            List<Product> productsList = JsonConvert.DeserializeObject<List<Product>>(productsJson);
            foreach (var p in productsList)
            {
                db.Products.Add(p);
            }

            string statusesJson = "[{'Id':1,'Name':'Новый'},{'Id':2,'Name':'Подтвержден'},{'Id':3,'Name':'Обработка'},{'Id':4,'Name':'Отгрузка'},{'Id':5,'Name':'Доставка'},{'Id':6,'Name':'Выполнен'}]";
            List<Status> statusesList = JsonConvert.DeserializeObject<List<Status>>(statusesJson);
            foreach (var s in statusesList)
            {
                db.Statuses.Add(s);
            }

            db.SaveChanges();
        }
    }

    public class OrderListContext : DbContext
    {
        static OrderListContext()
        {
            Database.SetInitializer<OrderListContext>(new OrderListContextInitializer());
        }
        public OrderListContext() : base("DBOrderList")
        { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }

    public class OrderRepository : IOrderRepository<Order>
    {
        OrderListContext db;
        public OrderRepository()
        {
            db = new OrderListContext();
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public Order Create(Order item)
        {
            item.Id = db.Orders.Count() + 1;
            item.StatusId = 1;
            db.Orders.Add(item);
            return item;
        }

        public void Update(Order item)
        {
            var order = this.Get(item.Id);
            order.ProductId = item.ProductId;
            order.StatusId = item.StatusId;
            order.Count = item.Count;
            db.Entry(order).State = EntityState.Modified;
        }

        public Order Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
            return order;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    interface IOrderRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T item);
        void Update(T item);
        T Delete(int id);
        void Save();
    }
}