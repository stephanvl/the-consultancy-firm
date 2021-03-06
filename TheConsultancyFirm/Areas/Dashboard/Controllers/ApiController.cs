﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheConsultancyFirm.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheConsultancyFirm.Areas.Dashboard.Controllers
{
    [Route("api/dashboard")]
    [Authorize]
    public class ApiController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ApiController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // GET: api/values
        [HttpGet("contacts/unread")]
        public int Get()
        {
            return _contactRepository.CountUnread();
        }
    }
}
