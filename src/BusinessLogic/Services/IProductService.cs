using AlifTask.DataAccess;
namespace AlifTask.BusinessLogic;

public interface IProductService
{
    decimal GetMarkupPercentage(ProductType productType, int installmentRange);
}