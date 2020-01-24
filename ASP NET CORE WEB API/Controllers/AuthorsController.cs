using ASP_NET_CORE_WEB_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_WEB_API.Controllers
{
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        public AuthorsController(ICourseLibraryRepository courseLibraryReopository)
        {
            _courseLibraryRepository = courseLibraryReopository;
        }

        [HttpGet("api/authors")]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            return new JsonResult(authorsFromRepo);
        }

        [HttpGet("api/authors/{id}")]
        public IActionResult GetAuthorById(Guid id) 
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(id);
            return new JsonResult(authorFromRepo);
        }

        // [HttpPost("api/authors")]
        // public IActionResult AddAuthor()
        // {
            
        //     return new JsonResult();
        // }
    }
}
