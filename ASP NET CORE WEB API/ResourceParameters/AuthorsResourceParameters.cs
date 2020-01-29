using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_WEB_API.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }
        // Just need to add more categories here if there is more filters to be added. 
        // Those strings are from the Query field sent by the client when interacting with the API
    }
}
