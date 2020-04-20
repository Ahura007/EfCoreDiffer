using System.Collections.Generic;
using EfCore.Model;
using EfCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EfCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;


        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        [HttpGet]
        public IList<Country> GetAll()
        {
            return _countryService.GetAll();
        }


        [HttpPost]
        public Country Post(Country country)
        {
            return _countryService.Post(country);
        }
    }
}