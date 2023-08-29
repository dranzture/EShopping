using Ardalis.GuardClauses;
using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Models;

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
        string username)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Description = Guard.Against.NullOrEmpty(description);
        InStock = Guard.Against.Negative(inStock);
        Height = Guard.Against.Negative(height);
        Width = Guard.Against.Negative(width);
        Weight = Guard.Against.Negative(weight);
        CreatedBy = username;
        CreatedDateTime = DateTimeOffset.Now;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public int InStock { get; private set; }
    public decimal Height { get; private set; }
    public decimal Width { get; private set; }
    public decimal Weight { get; private set; }

    public void IncreaseStock(int amount, string username)
    {
        InStock += amount;
        UpdateModifiedFields(username);
    }

    public void DecreaseStock(int amount, string username)
    {
        InStock -= amount;
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

    public void ChangeWidth(decimal weight, string username)
    {
        if (weight < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Inventory), "Weight cannot be less than 0");
        }

        Weight = weight;
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
}