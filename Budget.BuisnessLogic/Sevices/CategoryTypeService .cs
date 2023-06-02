using Budget.DAL.Repositories.Interfaces;
using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class CategoryTypeService : ICategoryTypeService
    {
        private IUnitOfWOrk _unitOfWork;

        public CategoryTypeService(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryType> GetCategoryTypes()
        {
            return _unitOfWork.CategoryTypeRepository.GetAll();
        }

        public CategoryType CreateCategoryType(CategoryType categoryType)
        {
            return _unitOfWork.CategoryTypeRepository.Create(categoryType);
        }

        public CategoryType GetCategoryType(int id)
        {
            var categoryType = _unitOfWork.CategoryTypeRepository.Get(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            return categoryType;
        }

        public void UpdateCategoryType(int id, CategoryType updatedCategoryType)
        {
            var categoryType = _unitOfWork.CategoryTypeRepository.Get(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            categoryType.Name = updatedCategoryType.Name;
            _unitOfWork.CategoryTypeRepository.Update(categoryType);
        }

        public void DeleteCategoryType(int id)
        {
            var categoryType = _unitOfWork.CategoryTypeRepository.Get(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            _unitOfWork.CategoryTypeRepository.Delete(categoryType);
        }
    }
}