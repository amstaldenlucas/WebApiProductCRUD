using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProductCRUD.Repositories;

namespace WebApiProductCRUD.Areas.Web.Controllers
{
    [Area("Web")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repository.Get();
            return View(products.ToArray());
        }

    }
}
