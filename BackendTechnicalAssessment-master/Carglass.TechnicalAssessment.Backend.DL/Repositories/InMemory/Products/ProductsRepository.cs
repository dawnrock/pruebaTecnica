using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public class ProductRepository : ICrudRepository<Product>
{
    private ICollection<Product> _products;

    public ProductRepository()
    {
        _products = new HashSet<Product>()
        {
            new Product()
            {
                Id = 1111111,
                ProductName = "Cristal ventanilla delantera",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = "2019-01-09 14:26:17"
            }
        };
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.ToList();
    }

    public Product? GetById(params object[] keyValues)
    {
        var id = Convert.ToInt64(keyValues[0]);
        return _products.SingleOrDefault(x => x.Id == id);
    }

    public void Create(Product item)
    {
        _products.Add(item);
    }

    public void Update(Product item)
    {
        var toUpdateItem = _products.Single(x => x.Id == item.Id);

        _products.Remove(toUpdateItem);
        _products.Add(item);
    }

    public void Delete(Product item)
    {
        var toDeleteItem = _products.Single(x => x.Id == item.Id);

        _products.Remove(toDeleteItem);
    }
}