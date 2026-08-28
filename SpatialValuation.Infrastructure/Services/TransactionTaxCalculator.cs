namespace SpatialValuation.Infrastructure.Services;

using SpatialValuation.Application.Common.Interfaces;
using SpatialValuation.Application.Valuations.Queries.CalculatePropertyValuation;

public class TransactionTaxCalculator : ITransactionTaxCalculator
{
    private const double StampDutyRate = 0.02;         // 2%
    private const double MunicipalTransferRate = 0.04;  // 4%
    private const double CapitalGainsRate = 0.15;       // 15%

    public TransactionTaxResultDto CalculateTaxes(double estimatedMarketValue, double acquisitionCost = 0.0)
    {
        double stampDuty = estimatedMarketValue * StampDutyRate;
        double municipalFee = estimatedMarketValue * MunicipalTransferRate;

        // Calculate CGT on gain (if market value > original acquisition cost)
        double taxableGain = Math.Max(0.0, estimatedMarketValue - acquisitionCost);
        double capitalGainsTax = taxableGain * CapitalGainsRate;

        double totalTax = stampDuty + municipalFee + capitalGainsTax;

        return new TransactionTaxResultDto
        {
            EstimatedMarketValue = Math.Round(estimatedMarketValue, 2),
            StampDutyFee = Math.Round(stampDuty, 2),
            CapitalGainsTax = Math.Round(capitalGainsTax, 2),
            MunicipalTransferFee = Math.Round(municipalFee, 2),
            TotalTransactionTax = Math.Round(totalTax, 2)
        };
    }
}