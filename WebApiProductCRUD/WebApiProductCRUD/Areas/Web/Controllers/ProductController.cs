using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProductCRUD.Areas.Web.ViewModels;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Repositories;
using WebApiProductCRUD.Utils;

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

        [HttpGet]
        public async Task<IActionResult> Edit(string? id = null)
        {
            var item = await _repository.Get(id);

            var vm = _mapper.Map<ProductVm>(item);
            vm ??= new ProductVm();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVm vm)
        {
            var model = _mapper.Map<Product>(vm);
            var result = await _repository.Edit(model);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            foreach (var item in result.KeyAndErrorMessages)
                ModelState.AddModelError(item.Key, item.Message);

            return View(vm);
        }

    }
}
