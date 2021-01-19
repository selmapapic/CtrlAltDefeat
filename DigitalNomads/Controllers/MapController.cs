using DigitalNomads.Data;
using DigitalNomads.Entities;
using DigitalNomads.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigitalNomads.Controllers
{
    public class MapController : Controller
    {
        private readonly CtrlAltDefeatDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public MapController(CtrlAltDefeatDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> GetAccountIdByUserIdAsync(string UserId)
        {
            AccountGetDetailsRes res = await _context.Accounts
                .Where(a => a.UserId == UserId)
                .Select(a => new AccountGetDetailsRes
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Lat = a.Lat,
                    Long = a.Long,
                    TeamId = a.TeamId

                }).FirstOrDefaultAsync();

            return res.Id;
        }

        public async Task<IActionResult> MapAsync()
        {
            List<Place> places = _context.Places.ToList();
            List<Account> accounts = _context.Accounts.ToList();

            string markeriLokacija = "[";
            int vel = 0;
            foreach (Place place in places)
            {
                markeriLokacija += "{";
                double lat = place.Lat;
                double lng = place.Long;
                markeriLokacija += String.Format("'lat': '{0}',", lat.ToString(System.Globalization.CultureInfo.InvariantCulture));
                markeriLokacija += String.Format("'long': '{0}',", lng.ToString(System.Globalization.CultureInfo.InvariantCulture));
                markeriLokacija += string.Format("'name': '{0}',", place.Name.Substring(0, place.Name.Length-3));
                markeriLokacija += string.Format("'address': '{0}',", place.Address);
                markeriLokacija += string.Format("'begin': '{0}',", place.Start.Hour);
                markeriLokacija += string.Format("'end': '{0}',", place.End.Hour);
                markeriLokacija += string.Format("'speed': '{0}'", place.Name.Substring(place.Name.Length-3, 3));
                if (vel < places.Count - 1)
                {
                    markeriLokacija += "},";
                }
                else
                {
                    markeriLokacija += "}";
                }
                vel++;
            }
            markeriLokacija += "];";
            ViewBag.MarkeriLokacija = markeriLokacija;

            ViewBag.Latitude = places.ElementAt(0).Lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.Longitude = places.ElementAt(0).Long.ToString(System.Globalization.CultureInfo.InvariantCulture);

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int accountId = await GetAccountIdByUserIdAsync(userId);

            int teamId = 0;


            foreach (Account account in accounts)
            {
                if (account.Id.Equals(accountId))
                {
                    teamId = account.TeamId;
                    break;
                }
            }

            string markeriTima = "[";
            vel = 0;
            int brojac = 0;

            foreach (Account account in accounts)
            {
                if (account.TeamId == teamId && account.UserId != userId)
                {
                    brojac++;
                }
            }

            foreach (Account account in accounts)
            {
                if (account.TeamId == teamId && account.UserId != userId)
                {
                    markeriTima += "{";
                    double lat = account.Lat;
                    double lng = account.Long;
                    markeriTima += String.Format("'lat': '{0}',", lat.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    markeriTima += String.Format("'long': '{0}',", lng.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    markeriTima += string.Format("'firstname': '{0}',", account.FirstName);
                    markeriTima += string.Format("'lastname': '{0}'", account.LastName);

                    if (vel < brojac - 1)
                    {
                        markeriTima += "},";
                    }
                    else
                    {
                        markeriTima += "}";
                    }
                    vel++;
                }
            }

            markeriTima += "];";
            ViewBag.MarkeriTima = markeriTima;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(string name, string address, DateTime start, DateTime end, string speed, String addressLat, String addressLong)
        {
            Place newPlace = new Place();
            newPlace.Name = name + speed;
            newPlace.Address = address;
            newPlace.Start = start;
            newPlace.End = end;
            newPlace.Lat = Double.Parse(addressLat, CultureInfo.InvariantCulture);
            newPlace.Long = Double.Parse(addressLong, CultureInfo.InvariantCulture);

            _context.Add(newPlace);
            _context.SaveChanges();

            return RedirectToAction("Map");

        }
    }
}