using AlifTask.DataAccess;
namespace AlifTask.BusinessLogic;

public interface IInstallmentService
{
    string InstallmentRequest(InstallmentDataDto installmentDataDto);
}