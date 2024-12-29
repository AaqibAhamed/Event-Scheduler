using EventScheduler.Controllers.Base;
using EventScheduler.Data;
using EventScheduler.Model.Auth;
using EventScheduler.Services.Model.Event;
using EventScheduler.Services.Model.External.Google;
using EventScheduler.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventScheduler.Data.Model;

namespace EventScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WebHooksController : BaseController
    {
        private readonly IEventDataService _eventDataService;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public WebHooksController(IEventDataService eventDataService, UserManager<ApplicationUser> userManager)
        {
            _eventDataService = eventDataService;
            _userManager = userManager;

        }
        
        [HttpPost]
        [Route("googlecalendar")]
        public async Task<IActionResult> AddEvent([FromBody] GoogleEvent gEvent)
        {

            //get user manually
            //real world scenario user would be retrieved through an
            //integration you might set in your database so you know based on the account that came from google

            var user = await _userManager.FindByNameAsync("john");
            if (user == null) return NotFound();
            _eventDataService.AddEvent(user.Id, new NewEventModel
            {
                IsOnline = true,
                Name=gEvent.Name,
                Description=gEvent.Description,
                End=gEvent.End,
                Start=gEvent.Start
            });
            return Ok();
        }
       
    }
}
