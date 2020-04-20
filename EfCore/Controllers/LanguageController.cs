using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCore.Model;
using EfCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly ILogger<LanguageController> _logger;
        private readonly ILanguageService _languageService;
 

        public LanguageController(ILogger<LanguageController> logger, ILanguageService languageService)
        {
            _logger = logger;
            _languageService = languageService;
        }

        [HttpGet]
        public IList<Language> GetAll()
        {
            return _languageService.GetAll();
        }


        [HttpGet]
        public Language GetById(int id = 1)
        {
            return _languageService.GetById(id);
        }
    }
}
