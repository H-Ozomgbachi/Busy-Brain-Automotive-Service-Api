using Common.Contracts.v1.Organisation;
using Common.Core.CQRS.Queries;
using Common.Core.Models;
using Common.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Api.Controllers.v1
{
    [Authorize]
    [Route("api/v1/organisations")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrganisationService _organisationService;
        private readonly IUserService _userService;
        public OrganisationController(IMediator mediator, IOrganisationService  organisationService, IUserService userService)
        {
            _mediator = mediator;
            _organisationService = organisationService;
            _userService = userService;
        }

        [HttpGet]        
        public async Task<List<ListItemViewModel>> GetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                return await _mediator.Send(new GetOrganisationsQuery());             
            }
            throw new UnauthorizedAccessException("User does not have access to view this organisation");
        }


        [HttpGet("{id}")]
        public async Task<OrganisationViewModel> GetAsync(int id)
        {
            string strUserId = User.FindFirstValue(ClaimTypes.Name);
            var activeUserId = Guid.Parse(strUserId);

            if (await _userService.CanUserAccessOrganizationAsync(activeUserId, id) || User.IsInRole("Admin"))
            {
                return await _mediator.Send(new GetOrganisationQuery() { Id = id });
            }
            throw new UnauthorizedAccessException("User does not have access to view this organisation");
        }

        [HttpPost]
        
        public async Task<OrganisationViewModel> Create([FromBody] OrganisationPayload payload)
        {
            string strUserId = User.FindFirstValue(ClaimTypes.Name);
            var activeUserId = Guid.Parse(strUserId);

            if (User.IsInRole("Admin"))
            {
                return await _organisationService.Create(payload, activeUserId);
            }

            throw new UnauthorizedAccessException("User does not have access to view this organisation");
            
        }
    }
}
