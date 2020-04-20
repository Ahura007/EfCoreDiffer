using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfCore.Model;

namespace EfCore.Service
{
    public interface ILanguageService
    {
        IList<Language> GetAll();
        Language GetById(int id);
    }
}
