using AlifTask.DataAccess;
using Microsoft.Extensions.Configuration;
namespace AlifTask.BusinessLogic;

public sealed class InstallmentService : IInstallmentService
{
    private readonly ISmsService _smsService;
    private readonly IConfiguration _configuration;
    private readonly IProductService _productService;

    public InstallmentService(ISmsService smsService, IConfiguration configuration, IProductService productService)
    {
        _smsService = smsService;
        _configuration = configuration;
        _productService = productService;
    }

    public string InstallmentRequest(InstallmentDataDto installmentDataDto)
    {
        ArgumentNullException.ThrowIfNull(installmentDataDto);

        decimal installmentPercentage = _productService
            .GetMarkupPercentage(installmentDataDto.ProductType, installmentDataDto.ClientInstallmentRange);
            
        // Represents a markup definition.
        decimal margin = installmentDataDto.Price * installmentPercentage / 100;

        // Represents a message about the purchase details.
        string message = string.Format(
                _configuration["DetaleInstallment"], 
                installmentDataDto.ProductType,
                installmentDataDto.Price,
                margin,
                installmentDataDto.Price + margin);
        
        return _smsService.SendSms(message, installmentDataDto.PhoneNumberClient);
    }
}