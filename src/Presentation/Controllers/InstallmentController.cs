using AlifTask.BusinessLogic;
using AlifTask.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Alif.Presentation;

[ApiController]
[Route("getinstallment")]
public class InstallmentController : ControllerBase
{
    private readonly IInstallmentService _installmentService;
    public InstallmentController(IInstallmentService installmentService)
        => _installmentService = installmentService;

    [HttpPost]
    public string GetInstallment(InstallmentDataDto installmentDataDto)
    {
        return _installmentService.InstallmentRequest(installmentDataDto);
    }
}