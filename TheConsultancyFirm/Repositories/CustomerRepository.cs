﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheConsultancyFirm.Data;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Customer> Get(int id)
        {
            return _context.Customers.FindAsync(id);
        }

        public Task<List<Customer>> GetAll()
        {
            return _context.Customers.ToListAsync();
        }

        public IQueryable<Customer> GetAllQueryable()
        {
            return _context.Customers;
        }

        public async Task<List<Customer>> Search(string term)
        {
            if (term == null || term.Trim() == "")
                return await GetAll();

            var q = term.Trim().ToLower();
            return await _context.Customers.Where(c => c.Name.ToLower().Contains(q)).ToListAsync();
        }

        public Task Create(Customer customer)
        {
            _context.Add(customer);
            return _context.SaveChangesAsync();
        }

        public Task Update(Customer customer)
        {
            _context.Customers.Update(customer);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var customer = Get(id).Result;
            _context.Customers.Remove(customer);
            return _context.SaveChangesAsync();
        }
    }
}
