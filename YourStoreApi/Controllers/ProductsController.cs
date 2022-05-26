#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourStoreApi.Context;
using YourStoreApi.Errors;
using YourStoreApi.Models;
using YourStoreApi.Models.Dto;
using YourStoreApi.Services;
using YourStoreApi.Services.Helpers;
using System.Text.Json;

namespace YourStoreApi.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Colour> _ColoesRepo;
        private readonly IGenericRepository<Size> _SizesRepo;
        private readonly IGenericRepository<Category> _CategoriesRepo;
        private readonly IGenericRepository<Review> _ReviewsRepo;
        private readonly IGenericRepository<SubCategory> _SubCategoriesRepo;

        private readonly IMapper _mapper;
        // private readonly IProductRepository _productRepository;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandsRepo,
        IGenericRepository<Colour> ColoesRepo,
        IGenericRepository<Size> SizesRepo,
        IGenericRepository<SubCategory> SubCategoriesRepo,
        IGenericRepository<Category> CategoriesRepo,
         IGenericRepository<Review> ReviewsRepo,
         IUnitOfWork unitOfWork,
        //  IProductRepository ProductRepo,
        IMapper mapper)
        {
            _productBrandsRepo = productBrandsRepo;
            _productsRepo = productsRepo;
            _mapper = mapper;
            _ColoesRepo = ColoesRepo;
            _SizesRepo = SizesRepo;
            _SubCategoriesRepo = SubCategoriesRepo;
            _CategoriesRepo = CategoriesRepo;
            _ReviewsRepo = ReviewsRepo;
            _unitOfWork = unitOfWork;
            // _productRepository = ProductRepo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFilterForCountSpecification(productParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            await _CategoriesRepo.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
            productParams.PageSize, totalItems, data));


        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            await _CategoriesRepo.ListAllAsync();
            await _ReviewsRepo.ListAllAsync();
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            //return product;
            return _mapper.Map<Product, ProductToReturnDto>(product);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandsRepo.ListAllAsync());
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyCollection<Category>>> GetCategories()
        {
            var subCategories = await _SubCategoriesRepo.ListAllAsync();
            var caategories = await _CategoriesRepo.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<CategoryToReturnDto>>(caategories));
        }
        [HttpGet("Colors")]
        public async Task<ActionResult<IReadOnlyCollection<Category>>> GetColors()
        {

            return Ok(await _ColoesRepo.ListAllAsync());
        }
        [HttpGet("Sizes")]
        public async Task<ActionResult<IReadOnlyCollection<Category>>> GetSizes()
        {

            return Ok(await _SizesRepo.ListAllAsync());
        }
        [HttpGet("SubCategories")]
        public async Task<ActionResult<IReadOnlyCollection<SubCategory>>> GetSubCategories()
        {
            var caategories = await _CategoriesRepo.ListAllAsync();
            var subCategories = await _SubCategoriesRepo.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<SubCategoryToReturnDto>>(subCategories));

        }
    
        [HttpPut]
        public async Task<Product> CreateProduct(object obj)

        {

            var serialized_product = obj.ToString();
            Product p = new Product()
            {
                Description = serialized_product.Split(("description\": \""))[1].Split("\"")[0],
                Color_Id = int.Parse(serialized_product.Split(("color_Id\": \""))[1].Split("\"")[0]),
                AverageRating = 0,
                Size_Id = int.Parse(serialized_product.Split(("size_Id\": \""))[1].Split("\"")[0]),
                Name = serialized_product.Split(("name\": \""))[1].Split("\"")[0],
                Sale = 0,
                Price = int.Parse(serialized_product.Split(("color_Id\": \""))[1].Split("\"")[0]),
                SubCategory_Id = int.Parse(serialized_product.Split(("subCategory_Id\": \""))[1].Split("\"")[0]),
                ProductBrandId = int.Parse(serialized_product.Split(("productBrandId\": \""))[1].Split("\"")[0]),
                CustomerBuy = 0,
                RateCount = 0,
                Quantity = int.Parse(serialized_product.Split(("quantity\": \""))[1].Split("\"")[0]),
                Date = DateTime.UtcNow
            };
            var product = (JsonSerializer.Deserialize<Product>(serialized_product));
            _unitOfWork.Repository<Product>().Add(product);
            var res=await _unitOfWork.Complete();
            if(res<=0)return null;
            return product;
        }
        [HttpDelete]
        public async Task<Product> DeleteProduct(int id)

        {
          var product= await _unitOfWork.Repository<Product>().GetByIdAsync(id);
             _unitOfWork.Repository<Product>().Delete(product);
            var res=await _unitOfWork.Complete();
            if(res<=0)return null;
            return product;
        }
        // [HttpPost("PostProduct")]
        // public async void PostProduct(object obj)

        // {
        //     var posted_product = obj.ToString();
        //     var Recieved_product =(JsonSerializer.Deserialize<Product>(posted_product));   
        //     Product product=new Product() { Description= Recieved_product.Description,
        //         Name= Recieved_product.Name
        //     , PictureUrl=Recieved_product.PictureUrl, Price=Recieved_product.Price, 
        //         ProductBrandId= Recieved_product.ProductBrandId, 
        //         ProductTypeId=Recieved_product.ProductTypeId};
        //     _productRepository.AddProduct(product);
        // }
        /*
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        */
    }
}
