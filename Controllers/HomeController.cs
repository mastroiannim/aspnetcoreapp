using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace aspnetcoreapp
{
    public class HomeController : Controller
    {

        private readonly Models.PostgreDbContext _context;

        public HomeController(Models.PostgreDbContext context)
        {
            _context = context;
        }
      

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Welcome
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        // GET: /Home/Hello/ 
        // Requires using System.Text.Encodings.Web;
        /*public string Hello(string name = "Guest", int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");
        }*/

        public async Task<IActionResult> EditById(Models.MessageModel message)
        {
            int id = message.Id;
            string name = message.Name;
            string text = message.Text;

            using var db = _context;

            // Update
            db.Update(new Models.MessageModel { Id = id, Name = name, Text = text });
            db.SaveChanges();

            return RedirectToAction("Hello");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var db = _context;

            //Find

            var message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }


            return View("Edit", message);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var db = _context;

            //Find

            var message = await db.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View("Details", message);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var db = _context;

            // Remove

            var message = await db.Messages.FindAsync(id);
            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Hello");
        }

        // GET: /Home/Hello/
        public async Task<IActionResult> Hello()
        {
            using var db = _context;

            //List all

            return View("Hello", await db.Messages.ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> Hello(Models.MessageModel message)
        {
            int id = message.Id;
            string name = message.Name;
            string text = message.Text;

            using var db = _context;
            // Note: This sample requires the database to be created before running.

            // Create
            db.Add(new Models.MessageModel { Name = name, Text = text });
            db.SaveChanges();

            // Read
            var allMessages = await db.Messages
                .OrderByDescending(b => b.Id)
                .ToListAsync();

            return View("Hello", allMessages);

        }
    }
}
