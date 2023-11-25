using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Management_System.Models;
using Travel_Agency_Management_System.Models.ViewModels;

namespace Travel_Agency_Management_System.Controllers
{
    public class ClientsController : Controller
    {
        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _he;
        public ClientsController(TravelDbContext context, IWebHostEnvironment he)
        {
            this._context = context;
            this._he = he;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var clients = await _context.Clients.Include(x => x.BookingEntries).ThenInclude(y => y.Spot).ToListAsync();
            ViewBag.totalPages = (int)Math.Ceiling((double)_context.Clients.Count() / 5);
            ViewBag.currentPage = page;
            return View(await _context.Clients.Include(x => x.BookingEntries).ThenInclude(y => y.Spot).Skip((page-1)*5).Take(5).ToListAsync());
        }

        public ActionResult Details(int? id)
        {
            Client client = _context.Clients.Include(x => x.BookingEntries).ThenInclude(y => y.Spot).Single(x=>x.ClientId==id);
            return View(client);
        }

        public IActionResult AddNewSpots(int? id)
        {
            ViewBag.spot = new SelectList(_context.Spots, "SpotId", "SpotName", id.ToString() ?? "");
            return PartialView("_addNewSpots");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientVM clientVM, int[] spotId)
        {
            if (ModelState.IsValid)
            {
                Client client = new Client()
                {
                    ClientName = clientVM.ClientName,
                    DateOfBirth = clientVM.DateOfBirth,
                    Phone = clientVM.Phone,
                    IsMarried = clientVM.IsMarried
                };
                //Image
                var file = clientVM.ImagePath;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(clientVM.ImagePath.FileName);
                string fileToSave = Path.Combine(webroot, folder, imgFileName);

                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        clientVM.ImagePath.CopyTo(stream);
                        client.Image = "/" + folder + "/" + imgFileName;
                    }
                }

                foreach (var item in spotId)
                {
                    BookingEntry bookingEntry = new BookingEntry()
                    {
                        Client = client,
                        ClientId = client.ClientId,
                        SpotId = item
                    };
                    _context.BookingEntries.Add(bookingEntry);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == id);

            ClientVM clientVM = new ClientVM()
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                DateOfBirth = client.DateOfBirth,
                Phone = client.Phone,
                Image = client.Image,
                IsMarried = client.IsMarried
            };
            var existSpot = _context.BookingEntries.Where(x => x.ClientId == id).ToList();
            foreach (var item in existSpot)
            {
                clientVM.SpotList.Add(item.SpotId);
            }
            return View(clientVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ClientVM clientVM, int[] spotId)
        {
            if (ModelState.IsValid)
            {
                Client client = new Client()
                {
                    ClientId = clientVM.ClientId,
                    ClientName = clientVM.ClientName,
                    DateOfBirth = clientVM.DateOfBirth,
                    Phone = clientVM.Phone,
                    IsMarried = clientVM.IsMarried,
                    Image = clientVM.Image
                };
                var file = clientVM.ImagePath;
                if (file != null)
                {
                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Path.GetFileName(clientVM.ImagePath.FileName);
                    string fileToSave = Path.Combine(webroot, folder, imgFileName);
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        clientVM.ImagePath.CopyTo(stream);
                        client.Image = "/" + folder + "/" + imgFileName;
                    }
                }

                var existSpot = _context.BookingEntries.Where(x => x.ClientId == client.ClientId).ToList();
                foreach (var item in existSpot)
                {
                    _context.BookingEntries.Remove(item);
                }
                foreach (var item in spotId)
                {
                    BookingEntry bookingEntry = new BookingEntry()
                    {

                        ClientId = client.ClientId,
                        SpotId = item
                    };
                    _context.BookingEntries.Add(bookingEntry);
                }
                _context.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(x=>x.BookingEntries).ThenInclude(y=>y.Spot)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'TravelDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
