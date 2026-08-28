namespace SpatialValuation.Application.Valuations.Commands.UploadPropertyDocument;

using MediatR;
using Microsoft.AspNetCore.Http;

public record UploadPropertyDocumentCommand(
    Guid PropertyId,
    IFormFile LegalDocument,      // Title Deed PDF / Lease Agreement
    IFormFile? SpatialVectorFile  // Shapefile (.zip), GeoJSON, or KML
) : IRequest<UploadDocumentResultDto>;

public record UploadDocumentResultDto(
    Guid DocumentId,
    string FilePath,
    string DocumentType,
    bool SpatialDataExtracted,
    string Message
);