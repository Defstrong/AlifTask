using AlifTask.DataAccess;
using Microsoft.Extensions.Configuration;
namespace AlifTask.BusinessLogic;

public sealed class InstallmentService : IInstallmentService
{
    private readonly ISmsService _smsService;
    private readonly IProductService _productService;

    public InstallmentService(ISmsService smsService, IProductService productService)
    {
        _smsService = smsService;
        _productService = productService;
    }

    public string InstallmentRequest(InstallmentDataDto installmentDataDto)
    {
        ArgumentNullException.ThrowIfNull(installmentDataDto);

        decimal installmentPercentage = _productService
            .GetMarkupPercentage(installmentDataDto.ProductType, installmentDataDto.ClientInstallmentRange);
            
        // Represents a markup definition.
        decimal margin = installmentDataDto.Price * installmentPercentage / 100;

        string baseMessageText = "Уважаемый клиент!\nВы купили {0} за {1} сомони с рассрочкой на {2} месяца.\nНаценка составляет {3} сомони.\nОбщая сумма платежа - {4}сомони.";
        // Represents a message about the purchase details.
        string message = string.Format(
                baseMessageText,
                installmentDataDto.ProductType,
                installmentDataDto.Price,
                installmentDataDto.ClientInstallmentRange,
                margin,
                installmentDataDto.Price + margin);
        
        return _smsService.SendSms(message, installmentDataDto.PhoneNumberClient);
    }
}