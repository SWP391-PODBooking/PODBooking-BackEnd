using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class ServiceDetail : BaseEntity
    {
        public string Name { get; set; } = null!;
        public bool IsNormal { get; set; }
        //public bool IsInUse { get; set; }
//Dư field => nó lỗi thì database ko có đủ field để trả kết quả về => database liên tục tìm kiếm field <=> error 
        public Guid AmenitySerivceId { get; set; }
        public AmenityService AmenityService { get; set; } = null!;

        public ICollection<BookingItem> BookingItems { get; set; } = null!;
    }
}