using StockIT.Models;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    // Add methods for CRUD operations (Create, Read, Update, Delete) if needed
}

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public InMemoryProductRepository()
    {
        _products = new List<Product>(); // Initialize with some sample data (optional)
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _products;
    }

    // I will implement other methods for CRUD operations if needed (AddProduct, UpdateProduct, DeleteProduct)
}