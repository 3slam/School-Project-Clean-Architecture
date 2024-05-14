using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Emails.Command.Model;
using School.Data.AppMetaData;
using SchoolWepApi.Bases;

namespace SchoolWepApi.Controllers
{
    [ApiController]
    public class EmailController(IMediator mediator) : AppController
    {
        private readonly IMediator mediator = mediator;


        [HttpPost(Router.EmailRouting.SendEmail)]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand command)
        {
            return Result(await mediator.Send(command));
        }

    }

}
