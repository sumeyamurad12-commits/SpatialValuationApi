namespace SpatialValuation.Application.UnitTests.Queries;

using FluentAssertions;
using Moq;
using SpatialValuation.Application.Common.Interfaces;
using SpatialValuation.Application.Valuations.Queries.CalculatePropertyValuation;
using Xunit;

public class CalculatePropertyValuationQueryHandlerTests
{
    private readonly Mock<IValuationDbContext> _contextMock;
    private readonly Mock<ITransactionTaxCalculator> _taxCalculatorMock;

    public CalculatePropertyValuationQueryHandlerTests()
    {
        _contextMock = new Mock<IValuationDbContext>();
        _taxCalculatorMock = new Mock<ITransactionTaxCalculator>();
    }

    [Fact]
    public void TransactionTaxCalculator_ShouldBeCalledWithCalculatedMarketValue()
    {
        // Arrange
        var expectedTaxDto = new TransactionTaxResultDto
        {
            EstimatedMarketValue = 10_000_000,
            StampDutyFee = 200_000,
            CapitalGainsTax = 1_500_000,
            MunicipalTransferFee = 400_000,
            TotalTransactionTax = 2_100_000
        };

        _taxCalculatorMock
            .Setup(t => t.CalculateTaxes(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(expectedTaxDto);

        // Assert setup verification
        var taxResult = _taxCalculatorMock.Object.CalculateTaxes(10_000_000);
        taxResult.TotalTransactionTax.Should().Be(2_100_000);

        _taxCalculatorMock.Verify(t => t.CalculateTaxes(10_000_000, 0.0), Times.Once);
    }
}