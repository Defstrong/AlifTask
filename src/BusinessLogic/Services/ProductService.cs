using AlifTask.DataAccess;
namespace AlifTask.BusinessLogic;

public sealed class ProductService : IProductService
{
    protected readonly List<int> InstallmentRanges = new() { 3, 6, 9, 12, 18, 24 };
    protected readonly Dictionary<int, (int, int)> PercentAndInstallmentRange = new() 
        {
            [(int)ProductType.Phone] = (3,9), 
            [(int)ProductType.Desktop] = (4,12),
            [(int)ProductType.TV] = (5,18)
        };
    public decimal GetMarkupPercentage(ProductType productType, int clientInstallmentRange)
    {
        (int percentage, int productInstallmentRange) = PercentAndInstallmentRange[(int)productType];
        
        // Here we define the amount of the additional installment range.
        int numberOfAdditionalRange = InstallmentRanges.IndexOf(clientInstallmentRange) - InstallmentRanges.IndexOf(productInstallmentRange);

        //  We calculate the percentage markup of the product. 
        //  If the number of extra range is greater than 0.
        decimal markupPercentage = numberOfAdditionalRange < 0 
            ? 0 : numberOfAdditionalRange * percentage;

        return markupPercentage;
    }
}