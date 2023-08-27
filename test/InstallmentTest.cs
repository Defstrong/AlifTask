using AlifTask.DataAccess;

namespace AlifTask.BusinessLogic.Tests
{
    public class InstallmentServiceTests
    {
        [Fact]
        public void InstallmentRequest_NullArgument_ThrowsArgumentNullException()
        {
            // Arrange
            var smsService = new SmsService();
            var productService = new ProductService();

            var installmentService = new InstallmentService(
                smsService,
                productService);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => installmentService.InstallmentRequest(null));
        }

        [Fact]
        public void InstallmentRequest_ValidArgument_ReturnsSmsMessage()
        {
            // Arrange
            var smsService = new SmsService();
            var productService = new ProductService();

            var installmentDataDto = new InstallmentDataDto
            {
                ProductType = ProductType.Phone,
                Price = 1000,
                ClientInstallmentRange = 18,
                PhoneNumberClient = "1234567890"
            };
            productService.GetMarkupPercentage(installmentDataDto.ProductType, installmentDataDto.ClientInstallmentRange);

            var installmentService = new InstallmentService(smsService,productService);

            // Act
            var result = installmentService.InstallmentRequest(installmentDataDto);

            // Assert
            Assert.Equal($"SMS отправлена на номер 1234567890:\n"
                + "Уважаемый клиент!\n"
                + "Вы купили Phone за 1000 сомони с рассрочкой на 18 месяца.\n"
                + "Наценка составляет 60 сомони.\nОбщая сумма платежа - 1060сомони.", result);
        }
    }
}
