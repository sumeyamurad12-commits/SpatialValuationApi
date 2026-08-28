namespace SpatialValuation.Application.Valuations.Queries.CalculatePropertyValuation;

public class TransactionTaxResultDto
{
    public double EstimatedMarketValue { get; set; }
    public double StampDutyFee { get; set; }
    public double CapitalGainsTax { get; set; }
    public double MunicipalTransferFee { get; set; }
    public double TotalTransactionTax { get; set; }
}