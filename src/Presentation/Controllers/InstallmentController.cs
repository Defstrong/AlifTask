using AlifTask.BusinessLogic;
using AlifTask.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Alif.Presentation;

[ApiController]
[Route("getinstallment")]
public class InstallmentController : ControllerBase
{
    private readonly IValidator<InstallmentDataDto> _validator;
    private readonly IInstallmentService _installmentService;
    public InstallmentController(IInstallmentService installmentService, IValidator<InstallmentDataDto> validator)
    {
        _validator = validator;
        _installmentService = installmentService;
    }

    [HttpPost]
    public string GetInstallment(InstallmentDataDto installmentDataDto)
    {
        var result = _validator.Validate(installmentDataDto);

        string textResult = string.Empty;

        foreach(var ii in result.Errors)
            textResult += ii + "\n";

        if(!result.IsValid)
            return $"Incorrect data\n{textResult}";
        return _installmentService.InstallmentRequest(installmentDataDto);
    }
}