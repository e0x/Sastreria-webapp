using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SastreriaWebApp.Models;
using SastreriaWebApp.GenericRepository;

namespace SastreriaWebApp.Controllers
{
    public class OrdersController : Controller
    {
        
        private IGenericRepository<Order> _repositoryorder;
        private IGenericRepository<Client> _repositoryclient;
        
        

        public OrdersController(IGenericRepository<Order> orderrepository,IGenericRepository<Client> clientrepository)
        {
            _repositoryclient = clientrepository;
            _repositoryorder = orderrepository;

        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //return View(await _repositoryorder.Orders.ToListAsync());
            
            return View(await _repositoryorder.GetAll().ToListAsync());

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _db.Orders
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var order = await _repositoryorder.GetAll().Include(order => order.Client).Include(order => order.Payments).FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            OrderDetailsViewModel otv = new OrderDetailsViewModel
            {
                Id = order.Id,
                ClientId = order.Client.Id,
                Name = order.Client.Name,
                Address = order.Client.Address,
                Tel = order.Client.Tel,
                DeliveryDate = order.DeliveryDate,
                Cost = order.Cost,
                Details = order.Details,
                Complete = order.Complete,
                Amount = order.Payments.Sum(P => P.Amount),
                Payments = order.Payments,
            };

            
            return PartialView(otv);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] OrderCreateViewModel orderCreateViewModel)
        {
            if (ModelState.IsValid)
            {

                var client = new Client
                {

                    Id = orderCreateViewModel.ClientId ?? Guid.Empty,
                    Name = orderCreateViewModel.Name,
                    Address = orderCreateViewModel.Address,
                    Tel = orderCreateViewModel.Tel
                };

                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    Amount = orderCreateViewModel.Amount,
                    Date = DateTime.Now
                    
                };

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    DeliveryDate = orderCreateViewModel.DeliveryDate,
                    Cost = orderCreateViewModel.Cost,
                    Details = orderCreateViewModel.Details,
                    Complete = orderCreateViewModel.Complete
                };

                order.Payments.Add(payment);
                //_repositoryclient.
                _repositoryorder.Insert(order);
                

                if (orderCreateViewModel.ClientId != null)
                {
                    _repositoryclient.Update(client);
                    //_repositoryorder.Entry(client).State = EntityState.Modified;
                } else
                {
                    _repositoryclient.Insert(client);
                }

                //await _repositoryorder.SaveChangesAsync();
                _repositoryorder.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(orderCreateViewModel);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _db.Orders.FindAsync(id);
            var order = await _repositoryorder.GetAll().Include(order => order.Client).Include(order => order.Payments).FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            //return View(order);
            OrderEditViewModel ote = new OrderEditViewModel
            {
                Id = order.Id,
                ClientId = order.Client.Id,
                Name = order.Client.Name,
                Address = order.Client.Address,
                Tel = order.Client.Tel,
                DeliveryDate = order.DeliveryDate,
                Cost = order.Cost,
                Details = order.Details,
                Complete = order.Complete,
                Amount = order.Payments.Sum(P => P.Amount)
            };


            return PartialView(ote);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DeliveryDate,Cost,Details,Complete")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repositoryorder.Update(order);
                    _repositoryorder.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // return View(order);
            return PartialView(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _repositoryorder.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return PartialView(order);
        }

        // POST: Orders/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order =  _repositoryorder.GetById(id);
            _repositoryorder.Delete(order);
             _repositoryorder.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _repositoryorder.GetAll().Any(e => e.Id == id);
        }
    }
}
