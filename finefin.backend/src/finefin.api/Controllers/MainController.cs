using finefin.api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace finefin.api.Controllers
{
    [ApiController]
    [Route("/")]
    public class MainController
    {
        [HttpGet]
        public bool Get(Transaction transaction)
        {
            return true;
        }
    }
}
