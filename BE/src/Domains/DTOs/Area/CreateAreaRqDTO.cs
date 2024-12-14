namespace BE.src.Domains.DTOs.Area
{
    public class CreateAreaRqDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Address { get; set; }
        public required float Longitude { get; set; }
        public required float Latitude { get; set; }
        public required List<IFormFile> Images { get; set; }
    }
}
