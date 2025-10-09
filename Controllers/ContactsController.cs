using ContactList.Data;
using ContactList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContactList.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactsController(ApplicationDbContext db) => _db = db;

        // GET: /Contacts
        public async Task<IActionResult> Index()
        {
            var contacts = await _db.Contacts.Include(c => c.Category).ToListAsync();
            return View(contacts);
        }

        // GET: /Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _db.Contacts.Include(c => c.Category)
                                .FirstOrDefaultAsync(c => c.ContactId == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // GET: /Contacts/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories, "CategoryId", "Name");
            return View("CreateEdit", new Contact());
        }

        // POST: /Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact, string submit)
        {
            if (submit == "Cancel") return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                contact.DateAdded = System.DateTime.UtcNow;
                _db.Add(contact);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_db.Categories, "CategoryId", "Name", contact.CategoryId);
            return View("CreateEdit", contact);
        }

        // GET: /Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _db.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            ViewBag.Categories = new SelectList(_db.Categories, "CategoryId", "Name", contact.CategoryId);
            return View("CreateEdit", contact);
        }

        // POST: /Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact, string submit)
        {
            if (submit == "Cancel") return RedirectToAction(nameof(Details), new { id });

            if (id != contact.ContactId) return BadRequest();

            if (ModelState.IsValid)
            {
                // Keep DateAdded as-is (should not be edited via form)
                var existing = await _db.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.ContactId == id);
                if (existing == null) return NotFound();

                contact.DateAdded = existing.DateAdded;
                _db.Update(contact);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = contact.ContactId });
            }

            ViewBag.Categories = new SelectList(_db.Categories, "CategoryId", "Name", contact.CategoryId);
            return View("CreateEdit", contact);
        }

      // GET: /Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _db.Contacts.Include(c => c.Category)
                                            .FirstOrDefaultAsync(c => c.ContactId == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: /Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _db.Contacts.FindAsync(id);
            if (contact != null)
            {
                _db.Contacts.Remove(contact);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
