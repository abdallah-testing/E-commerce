using E_CommerceSystem.DTO.Product;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepo;

        public ProductController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var products = productRepo.GetAll();
            List<GetProduct> getProducts = new List<GetProduct>();
            foreach (var product in products)
            {
                GetProduct getProduct = new GetProduct();
                getProduct.Id = product.Id;
                getProduct.Name = product.Name;
                getProduct.CategoryId = product.CategoryId;
                getProduct.SKU = product.SKU;
                getProduct.StockQuantity = product.StockQuantity;
                getProduct.Price = product.Price;
                getProduct.Description = product.Description;
                getProduct.IsActive = product.IsActive;
                getProduct.CreatedAt = product.CreatedAt;

                getProducts.Add(getProduct);
            }
            return Ok(ApiResponse<List<GetProduct>>.SuccessResponse(getProducts));
        }

        [HttpGet("with")]
        [Authorize]
        public IActionResult GetAllWithCategory()
        {
            var products = productRepo.GetAllWithCategory();
            List<GetProductWithCategory> getProductWithCategories = new List<GetProductWithCategory>();
            foreach (var product in products)
            {
                GetProductWithCategory getProductWithCategory = new GetProductWithCategory();
                getProductWithCategory.Id = product.Id;
                getProductWithCategory.Name = product.Name;
                getProductWithCategory.CategoryId = product.CategoryId;
                getProductWithCategory.SKU = product.SKU;
                getProductWithCategory.StockQuantity = product.StockQuantity;
                getProductWithCategory.Price = product.Price;
                getProductWithCategory.Description = product.Description;
                getProductWithCategory.IsActive = product.IsActive;
                getProductWithCategory.CreatedAt = product.CreatedAt;

                HelperCategory helperCategory = new HelperCategory();
                helperCategory.Id = product.Category.Id;
                helperCategory.Name = product.Category.Name;
                helperCategory.Descritpion = product.Category.Descritpion;
                helperCategory.CreatedAt = product.Category.CreatedAt;
                helperCategory.IsActive = product.Category.IsActive;

                getProductWithCategory.HelperCategory = helperCategory;

                getProductWithCategories.Add(getProductWithCategory);
            }
            return Ok(ApiResponse<List<GetProductWithCategory>>.SuccessResponse(getProductWithCategories));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            Product product = productRepo.GetById(id);
            if (product != null)
            {
                GetProduct getProduct = new GetProduct();
                getProduct.Id = product.Id;
                getProduct.Name = product.Name;
                getProduct.SKU = product.SKU;
                getProduct.Price = product.Price;
                getProduct.CategoryId = product.CategoryId;
                getProduct.Description = product.Description;
                getProduct.IsActive = product.IsActive;
                getProduct.StockQuantity = product.StockQuantity;
                getProduct.CreatedAt = product.CreatedAt;
                getProduct.UpdatedAt = product.UpdatedAt;

                return Ok(ApiResponse<GetProduct>.SuccessResponse(getProduct));
            }
            return BadRequest(ApiResponse<GetProduct>.ErrorResponse());
        }

        [HttpGet("with/{id:int}")]
        [Authorize]
        public IActionResult GetByIdWithCategory(int id)
        {
            var product = productRepo.GetByIdWithCategory(id);
            if (product != null)
            {
                GetProductWithCategory getProductWithCategory = new GetProductWithCategory();
                getProductWithCategory.Id = product.Id;
                getProductWithCategory.Name = product.Name;
                getProductWithCategory.CategoryId = product.CategoryId;
                getProductWithCategory.SKU = product.SKU;
                getProductWithCategory.StockQuantity = product.StockQuantity;
                getProductWithCategory.Price = product.Price;
                getProductWithCategory.Description = product.Description;
                getProductWithCategory.IsActive = product.IsActive;
                getProductWithCategory.CreatedAt = product.CreatedAt;

                HelperCategory helperCategory = new HelperCategory();
                helperCategory.Id = product.Category.Id;
                helperCategory.Name = product.Category.Name;
                helperCategory.Descritpion = product.Category.Descritpion;
                helperCategory.CreatedAt = product.Category.CreatedAt;
                helperCategory.IsActive = product.Category.IsActive;

                getProductWithCategory.HelperCategory = helperCategory;

                return Ok(ApiResponse<GetProductWithCategory>.SuccessResponse(getProductWithCategory));
            }
            return BadRequest(ApiResponse<GetProductWithCategory>.ErrorResponse());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateProduct product)
        {
            if (ModelState.IsValid)
            {
                if (!productRepo.GetAll().Any(p => p.SKU == product.SKU))
                {
                    var newProduct = new Product();
                    newProduct.Name = product.Name;
                    newProduct.Price = product.Price;
                    newProduct.SKU = product.SKU;
                    newProduct.StockQuantity = product.StockQuantity;
                    newProduct.CategoryId = product.CategoryId;
                    newProduct.Description = product.Description;
                    newProduct.IsActive = true;

                    productRepo.Create(newProduct);
                    productRepo.Save();
                    return Ok(ApiResponse<CreateProduct>.SuccessResponse(product));
                } 
                return BadRequest(ApiResponse<CreateProduct>.ErrorResponse(message: "The SKU should be unique!"));
            }
            return BadRequest(ApiResponse<CreateProduct>.ErrorResponse());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(UpdateProduct updatedProduct, int id)
        {
            if (ModelState.IsValid)
            {
                var product = productRepo.GetById(id);
                if (product != null)
                {
                    // mapping
                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;
                    product.SKU = updatedProduct.SKU;
                    product.StockQuantity = updatedProduct.StockQuantity;
                    product.CategoryId = updatedProduct.CategoryId;
                    product.Description = updatedProduct.Description;
                    product.IsActive = updatedProduct.IsActive;
                    product.UpdatedAt = DateTime.Now;
                    productRepo.Update(product);
                    productRepo.Save();

                    return Ok(ApiResponse<Product>.SuccessResponse(product));
                }
            }
            return BadRequest(ApiResponse<Product>.ErrorResponse());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = productRepo.GetById(id);
            if (product != null)
            {
                productRepo.Delete(product);
                productRepo.Save();

                return Ok(ApiResponse<Product>.SuccessResponse(product, "Deleted Successfully"));
            }
            return BadRequest(ApiResponse<Product>.ErrorResponse());
        }

        [HttpGet("category/{CategoryId:int}")]
        [Authorize]
        public IActionResult GetProductsByCategory(int CategoryId)
        {
            var products = productRepo.GetProductsByCategory(CategoryId);
            if (products.Count > 0)
            {
                List<GetProduct> getProducts = new List<GetProduct>();
                foreach (var product in products)
                {
                    GetProduct getProduct = new GetProduct();
                    getProduct.Id = product.Id;
                    getProduct.Name = product.Name;
                    getProduct.SKU = product.SKU;
                    getProduct.Description = product.Description;
                    getProduct.IsActive = product.IsActive;
                    getProduct.Price = product.Price;
                    getProduct.StockQuantity = product.StockQuantity;
                    getProduct.CategoryId = product.CategoryId;
                    getProduct.CreatedAt = product.CreatedAt;

                    getProducts.Add(getProduct);
                }
                return Ok(ApiResponse<List<GetProduct>>.SuccessResponse(getProducts));
            }
            return BadRequest(ApiResponse<GetProduct>.ErrorResponse());
        }

        [HttpGet("search")]
        [Authorize]
        public IActionResult SearchProductByName(string name)
        {
            var product = productRepo.GetProductByName(name);
            if (product != null)
            {
                GetProduct getProduct = new GetProduct();
                getProduct.Id = product.Id;
                getProduct.Name = product.Name;
                getProduct.SKU = product.SKU;
                getProduct.Description = product.Description;
                getProduct.IsActive = product.IsActive;
                getProduct.Price = product.Price;
                getProduct.StockQuantity = product.StockQuantity;
                getProduct.CategoryId = product.CategoryId;
                getProduct.CreatedAt = product.CreatedAt;

                return Ok(ApiResponse<GetProduct>.SuccessResponse(getProduct));
            }
            return BadRequest(ApiResponse<GetProduct>.ErrorResponse());
        }

        [HttpGet("pagination")]
        [Authorize]
        public IActionResult Pagination(int pageNum, int pageSize)
        {
            int totalCount = productRepo.GetAll().Count();
            if (totalCount > 0)
            {
                var products = productRepo.GetAll().Skip((pageNum - 1) * pageSize)
                                                    .Take(pageSize);

                List<GetProduct> getProducts = new List<GetProduct>();
                foreach (var product in products)
                {
                    GetProduct getProduct = new GetProduct();
                    getProduct.Id = product.Id;
                    getProduct.Name = product.Name;
                    getProduct.CategoryId = product.CategoryId;
                    getProduct.SKU = product.SKU;
                    getProduct.StockQuantity = product.StockQuantity;
                    getProduct.Price = product.Price;
                    getProduct.Description = product.Description;
                    getProduct.IsActive = product.IsActive;
                    getProduct.CreatedAt = product.CreatedAt;

                    getProducts.Add(getProduct);
                }

                int totalPages = (int)(Math.Ceiling(totalCount / (double)pageSize));

                return Ok(new
                {
                    items = getProducts,
                    total_Count = totalCount,
                    currentPage = pageNum,
                    page_Size = pageSize,
                    total_Pages = totalPages
                });
            }
            return BadRequest();
        }
    }
}
