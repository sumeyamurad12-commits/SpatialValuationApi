namespace SpatialValuation.Application.UnitTests.Services;

using FluentAssertions;
using SpatialValuation.Infrastructure.Services;
using Xunit;

public class TransactionTaxCalculatorTests
{
    private readonly TransactionTaxCalculator _sut; // SUT = System Under Test

    public TransactionTaxCalculatorTests()
    {
        _sut = new TransactionTaxCalculator();
    }

    [Fact]
    public void CalculateTaxes_ShouldComputeCorrectTaxBreakdown_WithoutAcquisitionCost()
    {
        // Arrange
        double marketValue = 10_000_000.0; // 10 Million ETB

        // Act
        var result = _sut.CalculateTaxes(marketValue);

        // Assert
        result.StampDutyFee.Should().Be(200_000.0);        // 2% = 200,000 ETB
        result.MunicipalTransferFee.Should().Be(400_000.0);  // 4% = 400,000 ETB
        result.CapitalGainsTax.Should().Be(1_500_000.0);    // 15% = 1,500,000 ETB
        result.TotalTransactionTax.Should().Be(2_100_000.0); // Total = 2.1M ETB
    }

    [Fact]
    public void CalculateTaxes_ShouldComputeCapitalGains_BasedOnNetGain()
    {
        // Arrange
        double marketValue = 10_000_000.0;
        double acquisitionCost = 6_000_000.0; // Taxable Gain = 4,000,000 ETB

        // Act
        var result = _sut.CalculateTaxes(marketValue, acquisitionCost);

        // Assert
        result.CapitalGainsTax.Should().Be(600_000.0); // 15% of 4M gain = 600,000 ETB
        result.TotalTransactionTax.Should().Be(1_200_000.0); // 200k + 400k + 600k
    }
}