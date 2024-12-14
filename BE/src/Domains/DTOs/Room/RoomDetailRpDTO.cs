using BE.src.Domains.Enum;
using BE.src.Domains.Models;

namespace BE.src.Domains.DTOs.Room
{
    public class RoomDetailRpDTO
    {
        public required Models.Room Info { get; set; }
        public required int FavouriteCount { get; set; } = 0;
    }
}