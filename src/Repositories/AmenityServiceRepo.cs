using BE.src.Domains.Database;
using BE.src.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.src.Repositories
{
    public interface IAmenityServiceRepo
    {
        Task<List<AmenityService>> GetAllAmenityService();
        Task<AmenityService?> GetAmenityServiceById(Guid amenityServiceId);
        Task<bool> CreateService(AmenityService service);
        Task<bool> CreateServiceImage(Image image);
        Task<int> CountServiceRemain(Guid serviceId);
        Task<bool> CreateServiceDetail(ServiceDetail serviceDetail);
        Task<bool> UpdateService(AmenityService service);
        Task<bool> DeleteService(AmenityService service);
        Task<bool> UpdateServiceImage(Image image);
        Task<bool> DeleteServiceImage(Image image);
        Task<Image?> GetImageByServiceId(Guid amenityServiceId);
        Task<DeviceChecking?> GetDeviceChecking(Guid BookingItemsId);
        Task<bool> AddDeviceChecking(DeviceChecking deviceChecking);
        Task<bool> UpdateDeviceChecking(DeviceChecking deviceChecking);
    }

    public class AmenityServiceRepo : IAmenityServiceRepo
    {
        private readonly PodDbContext _context;

        public AmenityServiceRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<List<AmenityService>> GetAllAmenityService()
        {
            return await _context.AmenityServices
                                .Include(a => a.Image)
                                .OrderBy(a => a.CreateAt)
                                .ToListAsync();
        }

        public async Task<AmenityService?> GetAmenityServiceById(Guid amenityServiceId)
        {
            return await _context.AmenityServices.Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == amenityServiceId);
        }

        public async Task<bool> CreateService(AmenityService service)
        {
            _context.AmenityServices.Add(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateServiceImage(Image image)
        {
            _context.Images.Add(image);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountServiceRemain(Guid serviceId)
        {

            // mat field thi ko goi duoc => && dang muon goi IsInUse thi ko dc .Where(s => s.AmenitySerivceId == serviceId && s.IsInUse == false)
            return await _context.ServiceDetails
                        .Where(s => s.AmenitySerivceId == serviceId)
                        .CountAsync();
        }

        public async Task<bool> CreateServiceDetail(ServiceDetail serviceDetail)
        {
            _context.ServiceDetails.Add(serviceDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateService(AmenityService service)
        {
            _context.AmenityServices.Update(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteService(AmenityService service)
        {
            _context.AmenityServices.Remove(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateServiceImage(Image image)
        {
            _context.Images.Update(image);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteServiceImage(Image image)
        {
            _context.Images.Remove(image);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Image?> GetImageByServiceId(Guid amenityServiceId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.AmenityServiceId == amenityServiceId);
        }

        public async Task<DeviceChecking?> GetDeviceChecking(Guid bookingItemsId)
        {
            return await _context.DeviceCheckings.FirstOrDefaultAsync(d => d.BookingItemsId == bookingItemsId);
        }

        public async Task<bool> AddDeviceChecking(DeviceChecking deviceChecking)
        {
            _context.DeviceCheckings.Add(deviceChecking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDeviceChecking(DeviceChecking deviceChecking)
        {
            _context.DeviceCheckings.Update(deviceChecking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}