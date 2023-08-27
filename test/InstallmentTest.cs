using AlifTask.BusinessLogic;
using Moq;
using AlifTask.DataAccess;
using Microsoft.Extensions.Configuration;

namespace AlifTask.Test;

public class InstallmentTest
{
    public class InstallmentServiceTests
    {
        // A mock object for the ISmsService interface
        private readonly Mock<ISmsService> _smsServiceMock;

        // A mock object for the IConfiguration interface
        private readonly Mock<IConfiguration> _configurationMock;

        // A mock object for the IProductService interface
        private readonly Mock<IProductService> _productServiceMock;

        // A test object for the InstallmentService class
        private readonly InstallmentService _installmentService;

        public InstallmentServiceTests()
        {
            // Initialize the mock objects
            _smsServiceMock = new Mock<ISmsService>();
            _configurationMock = new Mock<IConfiguration>();
            _productServiceMock = new Mock<IProductService>();

            // Initialize the test object with the mock objects as dependencies
            _installmentService = new InstallmentService(
                _smsServiceMock.Object,
                _configurationMock.Object,
                _productServiceMock.Object);
        }

        // A test method that checks if the InstallmentRequest method throws an exception when given a null parameter
        [Fact]
        public void InstallmentRequest_WithNullParameter_ThrowsArgumentNullException()
        {
            // Arrange
            InstallmentDataDto? installmentDataDto = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _installmentService.InstallmentRequest(installmentDataDto));
        }

        // A test method that checks if the InstallmentRequest method returns the expected message for different input values
        [Theory]
        // Use InlineData to provide values for the parameters of the test method
        [InlineData(ProductType.Phone, 1000, "123456789", 18, "Уважаемый клиент!\nВы купили Phone за 1000 сомони с рассрочкой на 18 месяца.\nНаценка составляет 60 сомони.\nОбщая сумма платежа - 1060 сомони.")]
        [InlineData(ProductType.Desktop, 2000, "987654321", 18, "Уважаемый клиент!\nВы купили Desktop за 2000 сомони с рассрочкой на 18 месяца.\nНаценка составляет 80 сомони.\nОбщая сумма платежа - 2080 сомони.")]
        [InlineData(ProductType.TV, 1500, "555555555", 18, "Уважаемый клиент!\nВы купили TV за 1500 сомони с рассрочкой на 18 месяца.\nНаценка составляет 0 сомони.\nОбщая сумма платежа - 1500 сомони.")]
        public void InstallmentRequest_WithValidParameter_ReturnsExpectedMessage(
            ProductType productType,
            decimal price,
            string phoneNumber,
            int installmentRange,
            string expectedMessage)
        {
            IProductService productService = new ProductService();
            // Arrange
            // Create an object of InstallmentDataDto with the given values
            InstallmentDataDto installmentDataDto = new InstallmentDataDto
            {
                ProductType = productType,
                Price = price,
                PhoneNumberClient = phoneNumber,
                ClientInstallmentRange = installmentRange
            };

            // Set up the mock objects to return the expected values

            _productServiceMock.Setup(p => p
                .GetMarkupPercentage(productType, installmentRange))
                    .Returns(productService.GetMarkupPercentage(
                        installmentDataDto.ProductType, installmentDataDto.ClientInstallmentRange));
            _configurationMock.Setup(c => c["DetaleInstallment"])
                .Returns("Уважаемый клиент!\nВы купили {0} за {1} сомони с рассрочкой на {2} месяца.\nНаценка составляет {3} сомони.\nОбщая сумма платежа - {4} сомони.");
            _smsServiceMock.Setup(s => s. 
                SendSms(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns((string message, string phone) => message);

            // Act
            // Call the InstallmentRequest method with the input object
            string actualMessage = _installmentService.InstallmentRequest(installmentDataDto);

            // Assert
            // Check if the actual message is equal to the expected message
            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}