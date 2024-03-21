

using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers


{
    [ApiController]//1 Gives this attribute
    [Route("api/[controller]")]//2 Specifies the route 
    public class ProductsController //3 Derive from Controller base using Microsoft.AspNetCore.Mvc;

    {
        /* 4 We're gonna use dependency injection to get our store context inside here 
        so that we've got access to the products table in our db. Add a ctor constructor */
        private readonly StoreContext _context;

        public ProductsController(StoreContext context) //5 add Parameter and initialize fields from parameter to go to step 6
        {
            /* 6 creates read only private field and assign this private field to the context added*/
            _context = context;
        }

        /* 7 use the context inside the controller by creating and endpoint, an HttpGet method specifying the type of returning result(ActionResult*/
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")] // ex. api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id); //8 test it on swagger
        }

    }
}