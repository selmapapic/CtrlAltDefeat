using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalNomads.Data;
using DigitalNomads.Models.Meditation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalNomads.Controllers
{
    public class MeditationController : Controller
    {
        // GET: /<controller>/
        public IActionResult MeditationTime()
        {
            return View();
        }

        private CtrlAltDefeatDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MeditationController(CtrlAltDefeatDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<MeditationRes>> GetAllMeditationDetailsAsync()
        {
            IList<MeditationRes> res = await _dbContext.Meditations
                .Select(m => new MeditationRes
                {
                    Sound = m.Sound,
                    BackgroundURL = m.BackgroundURL
                }).ToListAsync();

            return res;
        }



        public async Task<IActionResult> OneMinuteMeditation()
        {
            IList<MeditationRes> res = await GetAllMeditationDetailsAsync();
            Random rnd = new Random();
            int index = rnd.Next(res.Count);
            MeditationRes meditationRes = res[index];
            MeditationAndDurationModel model = new MeditationAndDurationModel
            {
                MeditationRes = meditationRes,
                Seconds = 60
            };
            return View("MeditationRoom", model);
        }

        public async Task<IActionResult> FiveMinutesMeditation()
        {
            IList<MeditationRes> res = await GetAllMeditationDetailsAsync();
            Random rnd = new Random();
            int index = rnd.Next(res.Count);
            MeditationRes meditationRes = res[index];
            MeditationAndDurationModel model = new MeditationAndDurationModel
            {
                MeditationRes = meditationRes,
                Seconds = 300
            };
            return View("MeditationRoom", model);
        }

        public async Task<IActionResult> TenMinutesMeditation()
        {
            IList<MeditationRes> res = await GetAllMeditationDetailsAsync();
            Random rnd = new Random();
            int index = rnd.Next(res.Count);
            MeditationRes meditationRes = res[index];
            MeditationAndDurationModel model = new MeditationAndDurationModel
            {
                MeditationRes = meditationRes,
                Seconds = 600
            };
            return View("MeditationRoom",model);
        }






    }
}
