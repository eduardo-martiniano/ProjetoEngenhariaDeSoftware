using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngSoftware.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EngSoftware.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuCoordenador()
        {
            return View();
        }

        public IActionResult MenuAluno()
        {
            List<Projeto> projetos = new List<Projeto>();
            ViewBag.Projetos = projetos;
            return View();
        }
    }
}
