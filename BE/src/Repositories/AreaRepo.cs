using BE.src.Domains.Database;
using BE.src.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Mozilla;

namespace BE.src.Repositories
{
    public interface IAreaRepo
    {
        Task<bool> CreateLocation(Location location);
        Task<bool> CreateArea(Area area);
        Task<bool> AddImageArea(Image image);
        Task<Area?> GetAreaById(Guid areaId);
        Task<List<Area>> GetAreas();
        Task<bool> UpdateArea(Area area);
        Task<bool> UpdateImageArea(Image image);
        Task<Image?> GetImageByAreaId(Guid areaId);
        Task<bool> UpdateLocation(Location location);
        Task<Location?> GetLocationById(Guid locationId);
        Task<List<Image>?> GetImagesByAreaId(Guid areaId);
        Task<bool> UpdateSecondImageArea(List<Image> image);
    }
    public class AreaRepo : IAreaRepo
    {
        private readonly PodDbContext _context;

        public AreaRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLocation(Location location)
        {
            _context.Locations.Add(location);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateArea(Area area)
        {
            _context.Areas.Add(area);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddImageArea(Image image)
        {
            _context.Images.Add(image);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Area?> GetAreaById(Guid areaId)
        {
            return await _context.Areas.FirstOrDefaultAsync(a => a.Id == areaId);
        }

        public async Task<List<Area>> GetAreas()
        {
            return await _context.Areas.Include(a => a.Location).ToListAsync();
        }

        public async Task<bool> UpdateArea(Area area)
        {
            _context.Areas.Update(area);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateImageArea(Image image)
        {
            var imageToUpdate = await _context.Images.FirstOrDefaultAsync(i => i.Id == image.Id);
            if (imageToUpdate == null)
            {
                return false;
            }

            imageToUpdate.Url = image.Url;
            imageToUpdate.UpdateAt = image.UpdateAt;

            _context.Images.Update(imageToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSecondImageArea(List<Image> image)
        {
            _context.Images.UpdateRange(image);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Image?> GetImageByAreaId(Guid areaId)
        {
            return await _context.Images.OrderByDescending(i => i.UpdateAt)
                                    .Take(1).FirstOrDefaultAsync(i => i.AreaId == areaId);
        }

        public async Task<List<Image>?> GetImagesByAreaId(Guid areaId)
        {
            return await _context.Images.Where(i => i.AreaId == areaId).ToListAsync();
        }

        public async Task<Location?> GetLocationById(Guid locationId)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Id == locationId);
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            _context.Locations.Update(location);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}