namespace SpatialValuation.Application.Common.Interfaces;

using SpatialValuation.Application.Valuations.Queries.CalculatePropertyValuation;

public interface ITransactionTaxCalculator
{
    TransactionTaxResultDto CalculateTaxes(double estimatedMarketValue, double acquisitionCost = 0.0);
}