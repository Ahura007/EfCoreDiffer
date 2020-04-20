using System.Collections.Generic;
using EfCore.Model;

namespace EfCore.Service
{
    public interface ICountryService
    {
        IList<Country> GetAll();
        Country Post(Country country);
    }
}