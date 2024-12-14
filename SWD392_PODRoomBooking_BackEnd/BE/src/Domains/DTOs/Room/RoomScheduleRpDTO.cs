using Microsoft.CodeAnalysis.CSharp.Syntax;
using Org.BouncyCastle.Asn1.Cms;

namespace BE.src.Domains.DTOs.Room
{
    public class RoomScheduleRpDTO
    {
        public required DateOnly DateOnly;
        public required List<BookingPeriod> BookingPeriods;
    }

    public class BookingPeriod
    {
        public required TimeOnly StartTime;
        public required TimeOnly EndTime;
    }
}