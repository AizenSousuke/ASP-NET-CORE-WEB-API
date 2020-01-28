using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_WEB_API.Models
{
    // A copy of Author.cs entity without Courses collection
    // Dtos are objects that are sent over the wire to the client based on the rows in the sql table. It is the frontend facing model per se.
    // We use Automapper in the controller to map what the backend Model should be when acting as a frondend Model, when sent over the wire. 
    public class AuthorDto
    {
        // No need validation attributes because this class doesn't post data
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }          

        public string MainCategory { get; set; }
    }
}
