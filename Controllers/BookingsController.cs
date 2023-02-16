namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Interfaces;
    using BookStore.Properties.Models;
    using BookStore.DTO;
    using AutoMapper;

    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        private readonly IMapper _mapper;

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // GET: api/Bookings
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetBookings()
        {
            var result = _mapper.Map<List<BookingDto>>(_bookingService.Get());

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, result);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Booking))]
        public IActionResult GetBooking(int id)
        {
            if (!_bookingService.Exists(id))
                return NotFound();

            var booking = _mapper.Map<BookingDto>(_bookingService.Get(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, booking);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult PutBooking(int id, BookingDto updatedBooking)
        {
            if (updatedBooking == null)
                return BadRequest(ModelState);

            if (id != updatedBooking.Id)
                return BadRequest(ModelState);

            if (!_bookingService.Exists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingMap = _mapper.Map<Booking>(updatedBooking);

            if (!_bookingService.Put(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating booking");
                return StatusCode(500, ModelState);
            }
            return StatusCode(204, "Booking was updated");
        }

        // POST: api/Bookings
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult PostBooking(BookingDto createBooking)
        {
            if (createBooking == null)
                return BadRequest(ModelState);

            var booking = _mapper.Map<List<BookingDto>>(_bookingService.Get())
                .Where(b => b.Delivery_Adress == createBooking.Delivery_Adress 
                && b.ProductId == createBooking.ProductId
                && b.StatusId == createBooking.StatusId
                && b.UserId == createBooking.UserId
                && b.Delivery_date == createBooking.Delivery_date).FirstOrDefault();

            if (booking != null)
            {
                ModelState.AddModelError("", "Booking already exists");
                return StatusCode(422, ModelState);
            }

            var bookingMap = _mapper.Map<Booking>(createBooking);
            if (!_bookingService.Post(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong, booking was not saved");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "Booking was created");
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBooking(int id)
        {
            if (!_bookingService.Exists(id))
                return NotFound();

            var bookingToDelete = _bookingService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookingService.Delete(bookingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting booking");
            }

            return StatusCode(204, "Booking was deleted");
        }

    }
}
