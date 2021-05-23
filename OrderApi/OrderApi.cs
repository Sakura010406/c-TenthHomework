using System;
using Microsoft.EntityFrameworkCore;


namespace OrderApi
{
    public class OrderItem{

        public long Id{get;set;}

        public string Name{get;set;}

        public string Customer{get;set;}

        public double Pay{get;set;}

        public string Time{get;set;}

        public bool IsComplete{get;set;}
    }

    public class OrderContext:DbContext{

        public OrderContext(DbContextOptions<OrderContext> options)
             : base(options){
             this.Database.EnsureCreated();
              }

              public DbSet<OrderItem> OrderTtems{get;set;}
    }
}
