using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TFG.Models;
using TFG.Services.Interfaces;

namespace TFG.Services
{
    public class TwitterLogService : ITwitterLogService
    {
        private readonly TFGContext _ctx;
        private readonly IMapper _mapper;

        public TwitterLogService(TFGContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<TwitterLog> FindOne(string tweetId)
        {
            return await _ctx.TwitterLogs.Where(it => it.TweetId == tweetId).FirstOrDefaultAsync();
        }

        public async Task<int> Save(TwitterLog log)
        {
            _ctx.TwitterLogs.Add(log);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<List<TwitterLog>> Find()
        {
            return await _ctx.TwitterLogs.ToListAsync();
        }
    }
}
