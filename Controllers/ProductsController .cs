using System.ComponentModel.DataAnnotations;
using Cohorts_1.FakeDependencyInjectionServises;
using Cohorts_1.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    //Fake servisler geliştirilerek Dependency injection kullanılmalıdır.
    private readonly IFakeDependencyInjection _fakeDependencyInjection; // DI tanımlama
    private readonly IFakeDbService  _fakeDbService; // DI tanımlama

    public ProductsController(IFakeDependencyInjection fakeDependencyInjection,IFakeDbService  fakeDbService)
    {
        _fakeDependencyInjection = fakeDependencyInjection;
        _fakeDbService= fakeDbService;
    }
    private static List<ProductModel> _products = new List<ProductModel>();

    [HttpGet]
    public IActionResult GetProducts() //listeme 
    {
        _fakeDependencyInjection.Yazdir("GetProduct ile listelendi");

        return Ok(_products);//200 
    }

    [HttpGet("GetProductById/{id}")] // Crud - Read
    public IActionResult GetProductById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound($"Product with Id {id} not found.");
        }

        return Ok(product);
    }

    [HttpPost("create")] // Crud - Create
    public IActionResult CreateProduct([FromBody] ProductModel product) //Body üzerinden 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);//400
        }

        product.Id = _products.Count + 1;
        _products.Add(product);

        _fakeDbService.SaveChange(); // DI kullanımı

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product); //201
    }

    [HttpPut("UpdateProductById/{id}")] //Crud - Update
    public IActionResult UpdateProduct(int id, [FromBody] ProductModel updatedProduct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);//400
        }

        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound($"Product with Id {id} not found.");//404
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

      _fakeDbService.SaveChange(); // DI kullanımı

        return Ok(existingProduct);//200
    }

    [HttpDelete("{id}")] //Crud - Delete
    public IActionResult DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound($"Product with Id {id} not found.");//404
        }

        _products.Remove(product);

        _fakeDbService.SaveChange(); // DI kullanımı

        return NoContent();//204
    }

    [HttpPatch("{id}")]
    public IActionResult PatchProduct(int id, [FromBody] ProductModel updatedProduct)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);

        if (existingProduct == null)
        {
            return NotFound(); // 404 
        }

        // Apply partial updates
        if (!string.IsNullOrEmpty(updatedProduct.Name))
        {
            existingProduct.Name = updatedProduct.Name;
        }

        if (updatedProduct.Price != 0)
        {
            existingProduct.Price = updatedProduct.Price;
        }

        _fakeDbService.SaveChange(); // DI kullanımı

        return Ok(existingProduct); // 200 
    }
    // Standart crud işlemlerine ek olarak, listeleme ve sıralama işlevleride eklenmelidir.
    // Örn: /api/products/list?name=abc 
    [HttpGet("list")] //Crud a ek Listeleme işlevi
    public IActionResult ListProducts([FromQuery] string name)
    {
        var filteredProducts = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        if (filteredProducts == null)
        {
            return NotFound();//404
        }

        return Ok(filteredProducts);//200
    }
}
