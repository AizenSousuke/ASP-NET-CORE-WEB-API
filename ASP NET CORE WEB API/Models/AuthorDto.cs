using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_WEB_API.Models
{
    // A copy of Author.cs entity without Courses collection
    public class AuthorDto
    {
        // No need validation attributes because this class doesn't post data
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }          

        public string MainCategory { get; set; }
    }
}
