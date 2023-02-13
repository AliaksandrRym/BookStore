namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Interfaces;
    using BookStore.Properties.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/Bookings
        [HttpGet]
        public IActionResult GetBookings()
        {
            try
            {
                var result = _bookingService.Get();
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var product = _bookingService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return StatusCode(200, product);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public IActionResult PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = _bookingService.Put(booking);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(201)]
        [HttpPost]
        public IActionResult PostBooking(Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _bookingService.Post(booking);
                    return StatusCode(201, result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                var result = _bookingService.Delete(id);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }

    }
}
