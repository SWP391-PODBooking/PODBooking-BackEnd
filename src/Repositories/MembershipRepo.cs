using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Database;
using BE.src.Domains.DTOs;
using BE.src.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.src.Repositories
{
    public interface IMembershipRepo
    {
        Task<Membership?> GetMembershipDetails(Guid userId);
        Task<bool> CreateMembership(Membership membership);
        Task<List<Membership>> GetAllMembership();
        Task<bool> UpdateMembership(Membership membership);
        Task<bool> DeleteMembership(Guid id);
    }

    public class MembershipRepo : IMembershipRepo
    {
        private readonly PodDbContext _context;
        public MembershipRepo(PodDbContext context)
        {
            _context = context;
        }
        public async Task<Membership?> GetMembershipDetails(Guid userId)
        {
            return (await _context.MembershipUsers.Where(m => m.UserId == userId && m.Status)
                    .Include(x => x.Membership).FirstOrDefaultAsync())?.Membership;
        }

        public async Task<bool> CreateMembership(Membership membership){
            _context.Memberships.Add(membership);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<List<Membership>> GetAllMembership()
        {
            return await _context.Memberships.ToListAsync();
        }

        public async Task<bool> UpdateMembership(Membership membership)
        {
            _context.Update(membership);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMembership(Guid id)
        {
            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null) return false;
            _context.Memberships.Remove(membership);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}