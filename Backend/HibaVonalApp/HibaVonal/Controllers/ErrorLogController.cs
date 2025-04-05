using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HibaVonal.DataContext;
using Hibavonal.DataContext.Entities;

namespace HibaVonal.Controllers
{
    public class ErrorLogsController : Controller
    {
        private readonly SQL _context;

        public ErrorLogsController(SQL context)
        {
            _context = context;
        }

        // GET: ErrorLogs
        public async Task<IActionResult> Index()
        {
            var sQL = _context.ErrorLog.Include(e => e.MaintenanceWorker).Include(e => e.Reporter).Include(e => e.Room);
            return View(await sQL.ToListAsync());
        }

        // GET: ErrorLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorLog = await _context.ErrorLog
                .Include(e => e.MaintenanceWorker)
                .Include(e => e.Reporter)
                .Include(e => e.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (errorLog == null)
            {
                return NotFound();
            }

            return View(errorLog);
        }

        // GET: ErrorLogs/Create
        public IActionResult Create()
        {
            ViewData["MaintenanceWorkerId"] = new SelectList(_context.User, "Id", "Email");
            ViewData["ReporterId"] = new SelectList(_context.User, "Id", "Email");
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType");
            return View();
        }

        // POST: ErrorLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportTime,Description,Comment,Status,Level,RoomId,MaintenanceWorkerId,ReporterId")] ErrorLog errorLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(errorLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaintenanceWorkerId"] = new SelectList(_context.User, "Id", "Email", errorLog.MaintenanceWorkerId);
            ViewData["ReporterId"] = new SelectList(_context.User, "Id", "Email", errorLog.ReporterId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", errorLog.RoomId);
            return View(errorLog);
        }

        // GET: ErrorLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorLog = await _context.ErrorLog.FindAsync(id);
            if (errorLog == null)
            {
                return NotFound();
            }
            ViewData["MaintenanceWorkerId"] = new SelectList(_context.User, "Id", "Email", errorLog.MaintenanceWorkerId);
            ViewData["ReporterId"] = new SelectList(_context.User, "Id", "Email", errorLog.ReporterId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", errorLog.RoomId);
            return View(errorLog);
        }

        // POST: ErrorLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReportTime,Description,Comment,Status,Level,RoomId,MaintenanceWorkerId,ReporterId")] ErrorLog errorLog)
        {
            if (id != errorLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(errorLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErrorLogExists(errorLog.Id))
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
            ViewData["MaintenanceWorkerId"] = new SelectList(_context.User, "Id", "Email", errorLog.MaintenanceWorkerId);
            ViewData["ReporterId"] = new SelectList(_context.User, "Id", "Email", errorLog.ReporterId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", errorLog.RoomId);
            return View(errorLog);
        }

        // GET: ErrorLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorLog = await _context.ErrorLog
                .Include(e => e.MaintenanceWorker)
                .Include(e => e.Reporter)
                .Include(e => e.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (errorLog == null)
            {
                return NotFound();
            }

            return View(errorLog);
        }

        // POST: ErrorLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var errorLog = await _context.ErrorLog.FindAsync(id);
            if (errorLog != null)
            {
                _context.ErrorLog.Remove(errorLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ErrorLogExists(int id)
        {
            return _context.ErrorLog.Any(e => e.Id == id);
        }
    }
}
