﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheConsultancyFirm.Data;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Contact contact)
        {
            _context.Add(contact);
            return _context.SaveChangesAsync();
        }

        public IQueryable<Contact> GetAll()
        {
            return _context.Contacts;
        }
    }
}
