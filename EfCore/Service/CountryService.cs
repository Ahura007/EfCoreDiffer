using System;
using System.Collections.Generic;
using System.Linq;
using EfCore.Context;
using EfCore.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfCore.Service
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;

        public CountryService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        private static readonly Func<ApplicationDbContext, List<Country>> GetAllQuery =
            EF.CompileQuery((ApplicationDbContext context) =>
                context.Countries.ToList());


        public IList<Country> GetAll()
        {
            return GetAllQuery(_context);
        }

        public Country Post(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return country;
        }
    }
}