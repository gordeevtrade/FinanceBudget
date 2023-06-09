using FamilyBudjetAPI;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();

    Category Get(int id);

    Category Create(Category category);

    IEnumerable<Category> GetByCategoryType(int categoryTypeId);

    void Update(Category category);

    public void Delete(Category category);
}