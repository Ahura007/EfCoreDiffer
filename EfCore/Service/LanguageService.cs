using System;
using System.Collections.Generic;
using System.Linq;
using EfCore.Context;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Service
{
    public class LanguageService : ILanguageService
    {
        private static readonly Func<ApplicationDbContext, List<Language>> GetAllQuery =
            EF.CompileQuery((ApplicationDbContext context) =>
                context.Languages.ToList());

        private static readonly Func<ApplicationDbContext, int, Language> GetByIdQuery =
            EF.CompileQuery((ApplicationDbContext context, int id) =>
                context.Languages.FirstOrDefault(x => x.Id == id));


        public LanguageService(ApplicationDbContext context)
        {
            Context = context;
        }

        private ApplicationDbContext Context { get; }

        public IList<Language> GetAll()
        {
            return GetAllQuery(Context);
        }


        public Language GetById(int id)
        {
            return GetByIdQuery(Context, id);
        }
    }
}