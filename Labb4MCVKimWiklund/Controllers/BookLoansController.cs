using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb4MCVKimWiklund.Data;
using Labb4MCVKimWiklund.Models;

namespace Labb4MCVKimWiklund.Controllers
{
    public class BookLoansController : Controller
    {
        private readonly ApplicationDBContext context;

        public BookLoansController(ApplicationDBContext context)
        {
            this.context = context;
        }

        // GET: BookLoans
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = context.BookLoans.Include(b => b.Books).Include(b => b.Customers);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: BookLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await context.BookLoans
                .Include(b => b.Books)
                .Include(b => b.Customers)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // GET: BookLoans/Create
        public IActionResult Create()
        {
            ViewData["fkBookId"] = new SelectList(context.Books, "BookId", "Description");
            ViewData["fkCustomerId"] = new SelectList(context.Customers, "CustomerId", "CustomerName");
            return View();
        }

        // POST: BookLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanId,fkCustomerId,fkBookId,LoanDate,ReturnDate")] BookLoan bookLoan)
        {
            if (ModelState.IsValid)
            {
                context.Add(bookLoan);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["fkBookId"] = new SelectList(context.Books, "BookId", "Description", bookLoan.fkBookId);
            ViewData["fkCustomerId"] = new SelectList(context.Customers, "CustomerId", "CustomerName", bookLoan.fkCustomerId);
            return View(bookLoan);
        }

        // GET: BookLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await context.BookLoans.FindAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }
            ViewData["fkBookId"] = new SelectList(context.Books, "BookId", "Description", bookLoan.fkBookId);
            ViewData["fkCustomerId"] = new SelectList(context.Customers, "CustomerId", "CustomerName", bookLoan.fkCustomerId);
            return View(bookLoan);
        }

        // POST: BookLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,fkCustomerId,fkBookId,LoanDate,ReturnDate")] BookLoan bookLoan)
        {
            if (id != bookLoan.LoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(bookLoan);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookLoanExists(bookLoan.LoanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["fkBookId"] = new SelectList(context.Books, "BookId", "Description", bookLoan.fkBookId);
            ViewData["fkCustomerId"] = new SelectList(context.Customers, "CustomerId", "CustomerName", bookLoan.fkCustomerId);
            return View(bookLoan);
        }

        // GET: BookLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await context.BookLoans
                .Include(b => b.Books)
                .Include(b => b.Customers)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // POST: BookLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookLoan = await context.BookLoans.FindAsync(id);
            context.BookLoans.Remove(bookLoan);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookLoanExists(int id)
        {
            return context.BookLoans.Any(e => e.LoanId == id);
        }
    }
}
