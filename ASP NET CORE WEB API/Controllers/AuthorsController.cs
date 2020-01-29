using ASP_NET_CORE_WEB_API.Entities;
using ASP_NET_CORE_WEB_API.Helpers;
using ASP_NET_CORE_WEB_API.Models;
using ASP_NET_CORE_WEB_API.ResourceParameters;
using ASP_NET_CORE_WEB_API.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            // Check if Null
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Attribute routing
        [HttpGet()]
        // Like Get but doesn't return a response payload to the client
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            // From query
            [FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            if (authorsFromRepo == null)
            {
                NotFound();
            }
            // Using mapper to map from authorsFromRepo object properties to destination type IEnumerable<AuthorDto>
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
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
            // Using mapper to map authorFromRepo to AuthorDto
            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
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
