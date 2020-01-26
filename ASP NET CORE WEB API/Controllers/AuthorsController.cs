using ASP_NET_CORE_WEB_API.Entities;
using ASP_NET_CORE_WEB_API.Helpers;
using ASP_NET_CORE_WEB_API.Models;
using ASP_NET_CORE_WEB_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_WEB_API.Controllers
{
    [ApiController]
    // Set Default Route template | AKA [Route("api/[controller]")] where [controller] will be set to the class name without 'controller'
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        public AuthorsController(ICourseLibraryRepository courseLibraryReopository)
        {
            // Check if Null
            _courseLibraryRepository = courseLibraryReopository ?? throw new ArgumentNullException(nameof(courseLibraryReopository));
        }

        // Attribute routing
        [HttpGet()]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            var authors = new List<AuthorDto>();

            foreach (var author in authorsFromRepo)
            {
                authors.Add(new AuthorDto()
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    MainCategory = author.MainCategory,
                    Age = author.DateOfBirth.GetCurrentAge()
                });
            }
            // Serialize to JSON format
            // return new JsonResult(authorsFromRepo);
            
            // Returns 200 OK response
            // return Ok(authorsFromRepo);
            return Ok(authors);
        }

        // Route will only match if authorId can be casted as a guid
        [HttpGet("{authorId:guid}")]
        public IActionResult GetAuthor(Guid authorId) 
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            
            // Check if author exists
            if (authorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(authorFromRepo);
            // return new JsonResult(authorFromRepo);
        }

        [HttpPost()]
        public IActionResult AddAuthor([FromBody]Author author)
        {
            Console.WriteLine(author);
            _courseLibraryRepository.AddAuthor(author);
            _courseLibraryRepository.Save();
            var newAuthor = _courseLibraryRepository.GetAuthor(author.Id);
            return Ok(newAuthor);
        }

        [HttpDelete("{authorId:guid}")]
        public IActionResult DeleteAuthor(Guid authorId)
        {
            if (_courseLibraryRepository.GetAuthor(authorId) != null)
            {
                _courseLibraryRepository.DeleteAuthor(_courseLibraryRepository.GetAuthor(authorId));
                _courseLibraryRepository.Save();
                return Ok();
            }
            return NotFound();
        }
    }
}
