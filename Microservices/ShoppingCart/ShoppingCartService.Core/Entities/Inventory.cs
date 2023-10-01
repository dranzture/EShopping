using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace ShoppingCartService.Core.Entities;

public class Inventory : BaseEntity
{
    public Inventory()
    {
    } //For ef

    public Inventory(string name,
        string description,
        int inStock,
        decimal height,
        decimal width,
        decimal weight,
        decimal price,
        string username,
        Guid? id = null)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Description = Guard.Against.NullOrEmpty(description);
        InStock = Guard.Against.Negative(inStock);
        Height = Guard.Against.Negative(height);
        Width = Guard.Against.Negative(width);
        Weight = Guard.Against.Negative(weight);
        Price = Guard.Against.Negative(price);
        CreatedBy = username;
        CreatedDateTime = DateTimeOffset.Now;
        
        //For Unit Test
        if (id.HasValue)
        {
            Id = id.Value;
        }
    }
    [Required]
    public string Name { get; private set; }
    
    [Required]
    public string Description { get; private set; }
    
    [Required]
    public int InStock { get; private set; }
    
    [Required]
    public decimal Height { get; private set; }
    
    [Required]
    public decimal Width { get; private set; }
    
    [Required]
    public decimal Weight { get; private set; }
    
    [Required]
    public decimal Price { get; private set; }
    
    public void IncreaseStock(int quantity, string username)
    {
        InStock += quantity;
        UpdateModifiedFields(username);
    }

    public void DecreaseStock(int quantity, string username)
    {
        InStock -= quantity;
        if (InStock < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Inventory), "In stock cannot be less than 0");
        }

        UpdateModifiedFields(username);
    }

    public void ChangeSize(decimal height, decimal width, string username)
    {
        if (height < 0 || width < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Inventory), "Dimensions cannot be less than 0");
        }

        Height = height;
        Width = width;
        UpdateModifiedFields(username);
    }

    public void UpdatePrice(decimal price, string username)
    {
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Inventory), "Price cannot be less than 0");
        }
        Price = price;
        UpdateModifiedFields(username);
    }

    public void ChangeDescription(string description, string username)
    {
        if (string.IsNullOrEmpty(description))
        {
            throw new ArgumentOutOfRangeException(nameof(Inventory), "Description cannot be empty");
        }

        Description = description;
        UpdateModifiedFields(username);
    }

    public void Delete(string username)
    {
        Delete(username);
    }
}