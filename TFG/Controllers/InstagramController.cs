using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TFG.Models;
using TFG.ViewModels.Instagram;

namespace TFG.Controllers
{
    public class InstagramController : Controller
    {
        private readonly TFGContext _ctx;

        public InstagramController(TFGContext ctx)
        {
            _ctx=ctx;
        }   

        public async Task<IActionResult> Index()
        {
            var logs = await _ctx.InstagramLogs
                                .AsNoTracking()
                                .ToListAsync();

            InstagramIndexVM vm = new InstagramIndexVM();
            vm.Logs = logs;

            return View(vm);
        }
    }
}
