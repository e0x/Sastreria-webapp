using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using SastreriaWebApp.Data;
using SastreriaWebApp.Models;
using SastreriaWebApp.GenericRepository;

namespace SastreriaWebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private IGenericRepository<Payment> _repositorypayment;
        private IGenericRepository<Order> _repositoryorder;
        //private readonly SastreriaWebAppContext _repository;

        public PaymentsController(IGenericRepository<Payment> paymentRepository, IGenericRepository<Order> orderrepository)
        {
            _repositorypayment = paymentRepository;
            _repositoryorder = orderrepository;

        }

     


        // GET: Payments/Create
        [Route("/Orders/{orderId}/Payment/Create")]
        public IActionResult Create(Guid orderid)
        {
            //var order =  _repository.Orders.FirstOrDefaultAsync(o => o.Id  == orderid);
            var order = _repositoryorder.GetById(orderid);
            if (order == null)
            {
                return RedirectToAction("Index", "Orders");
            }


            return PartialView();

        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Orders/{orderId}/Payment/Create")]
        public async Task<IActionResult> Create([FromForm] Payment payment, Guid orderId)
        {
            if (ModelState.IsValid)
            {

                //var order = await _repository.Orders.Include(order => order.Client).Include(order => order.Payments).FirstOrDefaultAsync(m => m.Id == orderId);
                var order = await _repositoryorder.GetAll().Include(order => order.Client).Include(order => order.Payments).FirstOrDefaultAsync(m => m.Id == orderId);
                 _repositorypayment.Insert(payment);

                payment.Date = DateTime.Now;
                
                order.Payments.Add(payment);
                _repositoryorder.Update(order);
                _repositoryorder.Save();
                //_repository.Entry(payment).State = EntityState.Added;

                //await _repository.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                
                return RedirectToAction("Index", "Orders");
            }



            return PartialView(payment);
        }



    }
}
