namespace Alpha.Reports.Models;

public record Product
{
    public Product()
    {
    }

    public Product(int productId, bool isHidden, DateTime? endOfSupportDate)
    {
        ProductId = productId;
        IsHidden = isHidden;
        EndOfSupportDate = endOfSupportDate;
    }

    public int ProductId { get; }

    public bool IsHidden { get; }

    public DateTime? EndOfSupportDate { get; }
}
