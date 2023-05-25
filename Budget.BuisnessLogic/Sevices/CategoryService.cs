using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class CategoryService : ICategoryService
    {
        private readonly FinanceContext _context;

        public CategoryService(FinanceContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            return category;
        }

        public IEnumerable<Category> GetCategoriesByCategoryType(int categoryTypeId)
        {
            var categorys = _context.Categories
                .Where(c => c.CategoryTypeId == categoryTypeId)
                .ToList();

            if (!categorys.Any())
            {
                throw new Exception("Category not found for the provided category");
            }

            return categorys;
        }

        public void UpdateCategory(int id, Category updatedCategory)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            category.Name = updatedCategory.Name;
            category.CategoryTypeId = updatedCategory.CategoryTypeId;
            _context.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}