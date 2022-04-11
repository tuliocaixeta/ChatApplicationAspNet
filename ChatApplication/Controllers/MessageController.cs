#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Services.Services.interfaces;
using Repositories.Models;

namespace ChatApplication.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
           
            this._messageService = messageService;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            return View(await _messageService.GetChatMessages());
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _messageService.GetMessageById(id);
            if (message == null || ValidateMessageFromUser(message))
            {
                return NotFound();
            }


            return View(message);
        }

        

        // GET: Message/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MessageContent")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.User = User.Identity.Name;
                await _messageService.SendMessage(message);
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var message = await _messageService.GetMessageById(id);
            if (message == null || ValidateMessageFromUser(message))
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MessageContent")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    message.User = User.Identity.Name;
                    _messageService.EditMessage(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
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
            return View(message);
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message =  await _messageService.GetMessageById(id);

            if (message == null || ValidateMessageFromUser(message))
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _messageService.DeleteMessage(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return false;//_context.Message.Any(e => e.Id == id);
        }

        private bool ValidateMessageFromUser(Message message)
        {
            return message.User != User.Identity.Name;
        }
    }
}
