namespace AlifTask.DataAccess;

public sealed record InstallmentDataDto
{
    public decimal Price { get; set; }

    public string? PhoneNumberClient { get; set; }

    public int ClientInstallmentRange { get; set; }

    public ProductType ProductType { get; init; }
}