using E_CommerceSystem.DTO.Category;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Category> categories = categoryRepo.GetAll();

            var getCategories = new List<GetCategory>();
            foreach (var category in categories)
            {
                var getCategory = new GetCategory();
                getCategory.Id = category.Id;
                getCategory.Name = category.Name;
                getCategory.Descritpion = category.Descritpion;
                getCategory.CreatedAt = category.CreatedAt;
                getCategory.UpdatedAt = category.UpdatedAt;

                getCategories.Add(getCategory);
            }

            return Ok(ApiResponse<List<GetCategory>>.SuccessResponse(getCategories));
        }

        [HttpGet("WithProducts")]
        [Authorize]
        public IActionResult GetAllWithProducts()
        {
            var categories = categoryRepo.GetAllWithProducts();

            var GetCategoriesWithProducts = new List<GetCategoryWithProduct>();
            foreach (var category in categories)
            {
                var GetCategoryWith = new GetCategoryWithProduct();
                GetCategoryWith.Id = category.Id;
                GetCategoryWith.Descritpion = category.Descritpion;
                GetCategoryWith.CreatedAt = category.CreatedAt;
                GetCategoryWith.IsActive = category.IsActive;
                GetCategoryWith.UpdatedAt = category.UpdatedAt;

                List<HelperProduct> helperProducts = new List<HelperProduct>();

                foreach (var product in category.Products)
                {
                    HelperProduct helperProduct = new HelperProduct();
                    helperProduct.Id = product.Id;
                    helperProduct.Name = product.Name;
                    helperProduct.SKU = product.SKU;
                    helperProduct.StockQuantity = product.StockQuantity;
                    helperProduct.Price = product.Price;
                    helperProduct.CreatedAt = product.CreatedAt;
                    helperProduct.Description = product.Description;
                    helperProduct.IsActive = product.IsActive;


                    helperProducts.Add(helperProduct);
                }
                GetCategoryWith.HelperProducts = helperProducts;

                GetCategoriesWithProducts.Add(GetCategoryWith);

            }
            return Ok(ApiResponse<List<GetCategoryWithProduct>>.SuccessResponse(GetCategoriesWithProducts));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            Category category = categoryRepo.GetById(id);
            if (category != null)
            {
                GetCategory getCategory = new GetCategory();
                getCategory.Id = category.Id;
                getCategory.Name = category.Name;
                getCategory.Descritpion = category.Descritpion;
                getCategory.UpdatedAt = category.UpdatedAt;
                getCategory.CreatedAt = category.CreatedAt;
                getCategory.IsActive = category.IsActive;

                return Ok(ApiResponse<GetCategory>.SuccessResponse(getCategory));
            }
            return BadRequest(ApiResponse<GetCategory>.ErrorResponse());
        }

        [HttpGet("WithProducts/{id:int}")]
        [Authorize]
        public IActionResult GetByIdWith(int id)
        {
            var category = categoryRepo.GetByIdWithProducts(id);
            if (category != null)
            {
                GetCategoryWithProduct getCategoryWith = new GetCategoryWithProduct();
                getCategoryWith.Name = category.Name;
                getCategoryWith.Id = category.Id;
                getCategoryWith.Descritpion = category.Descritpion;
                getCategoryWith.CreatedAt = category.CreatedAt;
                getCategoryWith.IsActive = category.IsActive;
                getCategoryWith.UpdatedAt = category.UpdatedAt;

                List<HelperProduct> helperProducts = new List<HelperProduct>();
                foreach (var product in category.Products)
                {
                    HelperProduct helperProduct = new HelperProduct();
                    helperProduct.Id = product.Id;
                    helperProduct.Name = product.Name;
                    helperProduct.SKU = product.SKU;
                    helperProduct.StockQuantity = product.StockQuantity;
                    helperProduct.Price = product.Price;
                    helperProduct.CreatedAt = product.CreatedAt;
                    helperProduct.Description = product.Description;
                    helperProduct.IsActive = product.IsActive;

                    helperProducts.Add(helperProduct);
                }
                getCategoryWith.HelperProducts = helperProducts;

                return Ok(ApiResponse<GetCategoryWithProduct>.SuccessResponse(getCategoryWith));
            }
            return BadRequest(ApiResponse<GetCategory>.ErrorResponse()); ;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateCategory category)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = category.Name;
                newCategory.Descritpion = category.Descritpion;
                newCategory.IsActive = category.IsActive;

                categoryRepo.Create(newCategory);
                categoryRepo.Save();
                return Ok(ApiResponse<CreateCategory>.SuccessResponse(category));
            }
            return BadRequest(ApiResponse<CreateCategory>.ErrorResponse());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public IActionResult Update(UpdateCategory updatedCategory, int id)
        {
            if (ModelState.IsValid)
            {
                var category = categoryRepo.GetById(id);
                if (category != null)
                {
                    // mapping
                    category.Name = updatedCategory.Name;
                    category.Descritpion = updatedCategory.Descritpion;
                    category.IsActive = updatedCategory.IsActive;
                    category.UpdatedAt = DateTime.Now;

                    categoryRepo.Update(category);
                    categoryRepo.Save();
                    return Ok(updatedCategory);
                }
                return Ok(ApiResponse<UpdateCategory>.SuccessResponse(updatedCategory));
            }
            return BadRequest(ApiResponse<UpdateCategory>.ErrorResponse());
        }

        //[HttpDelete]
        //[Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var category = categoryRepo.GetById(id);
            if (category != null)
            {
                categoryRepo.Delete(category);
                categoryRepo.Save();
                return Ok(ApiResponse<Category>.SuccessResponse(category, "Deleted Successfully"));
            }
            return BadRequest(ApiResponse<Category>.ErrorResponse());
        }
    }
}
