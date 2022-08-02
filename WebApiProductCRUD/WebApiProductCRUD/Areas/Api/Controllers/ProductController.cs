using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Repositories;
using WebApiProductCRUD.Utils;

namespace WebApiProductCRUD.Areas.Api.Controllers
{
    [Area("Api")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string from)
        {
            var dateFrom = StringDateTime.Parse(from, DateTime.Now.AddYears(-1));
            var list = await _repository.Get(dateFrom);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            var result = await _repository.Edit(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _repository.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
