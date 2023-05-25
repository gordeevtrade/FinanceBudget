using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class CategoryTypeService : ICategoryTypeService
    {
        private readonly FinanceContext _context;

        public CategoryTypeService(FinanceContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryType> GetCategoryTypes()
        {
            return _context.CategoryTypes.ToList();
        }

        public CategoryType CreateCategoryType(CategoryType categoryType)
        {
            _context.CategoryTypes.Add(categoryType);
            _context.SaveChanges();
            return categoryType;
        }

        public CategoryType GetCategoryType(int id)
        {
            var categoryType = _context.CategoryTypes.Find(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            return categoryType;
        }

        public void UpdateCategoryType(int id, CategoryType updatedCategoryType)
        {
            var categoryType = _context.CategoryTypes.Find(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            categoryType.Name = updatedCategoryType.Name;
            _context.Update(categoryType);
            _context.SaveChanges();
        }

        public void DeleteCategoryType(int id)
        {
            var categoryType = _context.CategoryTypes.Find(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            _context.CategoryTypes.Remove(categoryType);
            _context.SaveChanges();
        }
    }
}