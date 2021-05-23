using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {       
        private readonly OrderContext orderDb;

        public OrderController(OrderContext context)
        {

            this.orderDb=context;
        }

        //Get:api/Order
        [HttpGet]

        public ActionResult<List<OrderItem>> GetOrderItems(string Name,string Customer){
        IQueryable<OrderItem> query=orderDb.OrderTtems;

        if(Name!=null){

        query=query.Where(t=>t.Name.Contains(Name));
        }
        if(Customer!=null){

        query=query.Where(t=>t.Time.Contains(Customer));
        }

           return query.ToList();      
        }

        //Get:api/Order/{id}
        [HttpGet("{id}")]

        public ActionResult<OrderItem> GetOrderItem(long id){

            var OrderItem=orderDb.OrderTtems.FirstOrDefault(t=>t.Id==id);

            if(OrderItem==null){

                return NotFound();
            }

            return OrderItem;
        }

        //POST:api/Order
        [HttpPost]

        public ActionResult<OrderItem> PostOrderItem(OrderItem ot){

            try{
           orderDb.OrderTtems.Add(ot);

           orderDb.SaveChanges();

            }catch(Exception e){

                return BadRequest(e.InnerException.Message);
            }
            return ot;
        }

        //put:api/Order/{id}
        [HttpPut("{id}")]

        public ActionResult<OrderItem> PutOrderItem(long id,OrderItem ot){

            if(id!=ot.Id){
                return BadRequest("ID cannot be modified!");
            }
            try{

                orderDb.Entry(ot).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                
                orderDb.SaveChanges();
            }catch(Exception e){
                string error=e.Message;

                if(e.InnerException!=null)error=e.InnerException.Message;

                return BadRequest(error);
            }
            return NoContent();
        }

        //DELETE:api/Order/{id}
        [HttpDelete("{id}")]

        public ActionResult DeleteOrderItem(long id){

            try{
                var ot=orderDb.OrderTtems.FirstOrDefault(t=>t.Id==id);

                if(ot!=null){
                    orderDb.Remove(ot);

                    orderDb.SaveChanges();
                }
            }catch(Exception e){

                return BadRequest(e.InnerException.Message);
            }

            return NoContent();
        }
    }
}