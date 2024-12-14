using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.AmenityService{
    public class CreateServiceDTO{
        public required string Name {get; set;}
        public required AmenityServiceTypeEnum Type {get; set;}
        public required float Price {get; set;}
        public required IFormFile Image {get; set;}
    }
}