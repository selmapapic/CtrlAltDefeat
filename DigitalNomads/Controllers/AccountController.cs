using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DigitalNomads.Data;
using DigitalNomads.Entities;
using DigitalNomads.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DigitalNomads.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> AccountDetailsAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int accId = await GetAccountIdByUserIdAsync(userId);

            Account account = await _dbContext.Accounts
                .Where(a => a.Id == accId)
               .FirstOrDefaultAsync();

            Team team = await _dbContext.Teams
            .Where(t => t.Id == account.TeamId)
            .FirstOrDefaultAsync();

            AccountRes res = new AccountRes
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                TeamName = team.Name
            };



            return View(res);
        }

        private CtrlAltDefeatDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(CtrlAltDefeatDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> SaveAccountChanges(AccountGetDetailsRes res)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int accId = await GetAccountIdByUserIdAsync(userId);

            Account account = await _dbContext.Accounts
                .Where(a => a.Id == accId)
               .FirstOrDefaultAsync();



            Team team = await _dbContext.Teams
                .Where(t => t.Name == res.TeamName)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                Account acc = new Account
                {
                    UserId = userId,
                    FirstName = res.FirstName,
                    LastName = res.LastName,
                    TeamId = team.Id
                };

                _dbContext.Accounts.Add(acc);
            }
            else
            {
                account.TeamId = team.Id;
                account.FirstName = res.FirstName;
                account.LastName = res.LastName;
                _dbContext.Accounts.Update(account);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("ShowCards", "Show");
        }

        public async Task<int> GetAccountIdByUserIdAsync(string UserId)
        {
            AccountGetDetailsRes res = await _dbContext.Accounts
                .Where(a => a.UserId == UserId)
                .Select(a => new AccountGetDetailsRes
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Lat = a.Lat,
                    Long = a.Long

                }).FirstOrDefaultAsync();

            return res.Id;
        }

    }
}