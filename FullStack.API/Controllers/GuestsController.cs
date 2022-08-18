using FullStack.API.Data;
using FullStack.API.Models;
using FullStack.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : Controller

    {
        private readonly FullStackDbContext _fullStackDbContext;

        public GuestsController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllGuests()
        {
            var guests = await _fullStackDbContext.Guests.OrderBy(o => o.FullName).Where(x => x.CheckOut > DateTime.Now).Select(x => new GuestViewModel {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Room = x.Room,
                price = x.price,
                CheckIn = x.CheckIn.ToShortDateString(),
                CheckOut = x.CheckOut.ToShortDateString(),

            }).ToListAsync();

   
            return Ok(guests);
        }


        //public async Task<IActionResult> GetAllGuests()
        //{
        //    var guests = await _fullStackDbContext.Guests.OrderBy(o => o.FullName).Where(x => x.CheckOut > DateTime.Now).Select(x => new GuestViewModel
        //    {
        //        Id = x.Id,
        //        FullName = x.FullName,
        //        Email = x.Email,
        //        Room = x.Room,
        //        price = x.price,
        //        CheckIn = x.CheckIn.ToShortDateString(),
        //        CheckOut = x.CheckOut.ToShortDateString(),

        //    }).ToListAsync();

        //    int dueCheckouts = await _fullStackDbContext.Guests.Where(x => x.CheckOut.Day - DateTime.Now.Day == 1 || x.CheckOut.Day == DateTime.Now.Day).CountAsync();

        //    ResponseBody responseBody = new ResponseBody();
        //    responseBody.Guests = guests;
        //    responseBody.DueCheckouts = dueCheckouts;
        //    return Ok(responseBody);
        //}
        ////Get due date method

        //[HttpGet]
        //public async Task<JsonResult> GetDueCheckouts()
        //{

        //    int dueCheckouts = await _fullStackDbContext.Guests.Where(x => x.CheckOut.Day - DateTime.Now.Day == 1 || x.CheckOut.Day == DateTime.Now.Day).CountAsync();

        //    return Json(dueCheckouts);
        //}

        //end of due date method

        [HttpPost]

        public async Task<IActionResult> AddGuest([FromBody] Guest guestRequest)
        {
            guestRequest.Id = Guid.NewGuid();

            var guest = new Guest
            {
                Id = guestRequest.Id,
                FullName = guestRequest.FullName,
                Email = guestRequest.Email,
                CheckIn = Convert.ToDateTime(guestRequest.CheckIn),
                CheckOut = Convert.ToDateTime(guestRequest.CheckOut),
                price = guestRequest.price,
                Room = guestRequest.Room,
            };

            await _fullStackDbContext.Guests.AddAsync(guest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(guestRequest);

        }

        [HttpGet]
        [Route("id:Guid")]

        public async Task<IActionResult> GetGuest( Guid id)
        {
            var guest =
                await _fullStackDbContext.Guests.FirstOrDefaultAsync(x => x.Id == id);

            if (guest == null)
            {
                return NotFound();

            }

            return Ok(guest);
        }

        [HttpPut]
        [Route("id:Guid")]
        public async Task<IActionResult> UpdateGuest(  Guid id, Guest updateGuestRequest)
        {
            var guest = await _fullStackDbContext.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            guest.FullName = updateGuestRequest.FullName;
            guest.CheckIn = updateGuestRequest.CheckIn;
            guest.CheckOut = updateGuestRequest.CheckOut;
            guest.Email = updateGuestRequest.Email;
            guest.price = updateGuestRequest.price;
            guest.Room = updateGuestRequest.Room;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(guest);
        }


        [HttpDelete]
        [Route("id:Guid")]

        public async Task<IActionResult> DeleteGuest( Guid id)
        {
            var guest = await _fullStackDbContext.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            _fullStackDbContext.Guests.Remove(guest);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok();

        }

    }

    }

