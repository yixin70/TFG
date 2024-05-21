using AutoMapper;
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
    }
}
