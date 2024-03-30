using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services.Interfaces;

namespace TFG.Services
{
    public class InstagramLogService : IInstagramLogService
    {
        private readonly TFGContext _ctx;
        private readonly IMapper _mapper;

        public InstagramLogService(TFGContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<int> SaveLogMedia(InstagramMedia media)
        {
            var log = new InstagramLog()
            {
                Date = media.Date,
                Description = "Uploaded an Image"
            };

            _ctx.InstagramLogs.Add(log);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<List<InstagramLog>> Find()
        {
            return await _ctx.InstagramLogs.AsNoTracking().ToListAsync();
        }
    }
}
