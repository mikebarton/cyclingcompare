using AutoMapper;
using cyclingcompare.comms.api.Email;
using cyclingcompare.comms.api.Email.ContactUs;
using cyclingcompare.comms.api.ViewModels.ContactUs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.Controllers
{
    [Route("ContactUs")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IOptions<ContactUsOptions> _options;
        private readonly EmailService _emailService;
        private readonly IRazorLightEngine _razorEngine;
        private readonly IMapper _mapper;

        public ContactUsController(IOptions<ContactUsOptions> options, EmailService emailService, IRazorLightEngine razorEngine, IMapper mapper)
        {
            _options = options;
            _emailService = emailService;
            _razorEngine = razorEngine;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("SendContactUsMessage")]
        public async Task SendContactUsMessage(SendContactUsMessageViewModel viewmodel)
        {
            var emailVm = _mapper.Map<ContactUsViewModel>(viewmodel);
            var body = await _razorEngine.CompileRenderAsync(@"ContactUs/Email.cshtml", emailVm);
            await _emailService.SendMessage(_options.Value.TargetEmail, _options.Value.FromEmail, _options.Value.FromName, viewmodel.Subject, body);
        }
    }
}
