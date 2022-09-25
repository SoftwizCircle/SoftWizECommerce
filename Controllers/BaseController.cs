using Microsoft.AspNetCore.Mvc;
using SoftWizDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftWizECommerce.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SoftWizDatabaseContext _context;
        protected BaseController(SoftWizDatabaseContext context)
        {
            _context = context;
        }
    }
}