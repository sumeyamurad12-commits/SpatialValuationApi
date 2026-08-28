using NetTopologySuite.Geometries;

namespace SpatialValuation.Domain.Entities;

public class Road
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; } // Nullable string allows unnamed roads in database
    public string RoadType { get; set; } = "Paved"; // Paved, Unpaved, Highway
    public LineString Geometry { get; set; } = null!;
}