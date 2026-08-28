namespace SpatialValuation.Application.Valuations.Commands.UploadPropertyDocument;

using MediatR;

public class UploadPropertyDocumentCommandHandler
    : IRequestHandler<UploadPropertyDocumentCommand, UploadDocumentResultDto>
{
    // Allowed file extensions
    private static readonly string[] AllowedLegalExtensions = { ".pdf" };
    private static readonly string[] AllowedSpatialExtensions = { ".zip", ".geojson", ".kml", ".tif", ".tiff" };
    public async Task<UploadDocumentResultDto> Handle(
        UploadPropertyDocumentCommand request,
        CancellationToken cancellationToken)

    {
        // 1. Validate Legal Document (.pdf)
        if (request.LegalDocument == null || request.LegalDocument.Length == 0)
        {
            throw new ArgumentException("Legal document is required.");
        }

        var legalExtension = Path.GetExtension(request.LegalDocument.FileName).ToLowerInvariant();
        if (!AllowedLegalExtensions.Contains(legalExtension))
        {
            throw new ArgumentException($"Invalid legal document format '{legalExtension}'. Only PDF files are accepted.");
        }

        // 2. Validate Spatial Vector File (if provided)
        if (request.SpatialVectorFile != null && request.SpatialVectorFile.Length > 0)
        {
            var spatialExtension = Path.GetExtension(request.SpatialVectorFile.FileName).ToLowerInvariant();
            if (!AllowedSpatialExtensions.Contains(spatialExtension))
            {
                throw new ArgumentException(
                    $"Invalid spatial file format '{spatialExtension}'. Allowed formats: {string.Join(", ", AllowedSpatialExtensions)}"
                );
            }
        }
        // 1. Validate file extension (.pdf, .zip, .geojson)
        // 2. Save file to disk / cloud storage
        // 3. Extract spatial coordinates or title deed data if applicable

        return new UploadDocumentResultDto(
            DocumentId: Guid.NewGuid(),
            FilePath: $"uploads/{request.LegalDocument.FileName}",
            DocumentType: request.LegalDocument.ContentType,
            SpatialDataExtracted: request.SpatialVectorFile != null,
            Message: "Document uploaded and queued for spatial processing."
        );
    }

    
}