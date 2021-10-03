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
    public class ClientsController : Controller
    {
        //private readonly SastreriaWebAppContext _db;

        private IGenericRepository<Client> _repositoryclient;
        public ClientsController(IGenericRepository<Client> clientrepository)
        {
            _repositoryclient = clientrepository;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _repositoryclient.GetAll().ToListAsync());
        }
        public IActionResult Search(string term)
        {
            var tels = _repositoryclient.GetAll().Where(c => c.Tel.Contains(term)).Select( c => c.Tel).ToList();
            
            return new JsonResult(tels);
        }

        public IActionResult SearchByTel(string tel)
        {
            var clients = _repositoryclient.GetAll().Where(c => c.Tel.Contains(tel));

            return new JsonResult(clients);
        }

    }
}
