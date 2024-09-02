using Betting.Application.Commands.CasinoWagers.Publish;
using Betting.Application.Queries.CasinoWagers.GetByPlayer;
using Betting.Application.Queries.CasinoWagers.TopSpenders;
using Betting.Common.DTOs;
using Betting.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Betting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("casinowager")]
        public async Task<ActionResult> Publish(PublishCasinoWagerCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("topSpenders")]
        [Produces(typeof(IList<SpendingDetailDTO>))]
        public async Task<ActionResult<IList<SpendingDetailDTO>>> GetTopSpenders(int count)
        {
            var topSpenders = await _mediator.Send(new GetTopSpendersQuery { Count = count });

            return Ok(topSpenders);
        }

        [HttpGet("{playerId}/casino")]
        [Produces(typeof(PaginatedResponse<IList<CasinoWagerDTO>>))]
        public async Task<ActionResult<PaginatedResponse<IList<CasinoWagerDTO>>>> GetPlayerCasinoWagers(Guid playerId, int page, int pageSize)
        {
            var playerCasinoWagers = await _mediator.Send(new GetCasinoWagersByPlayerIdQuery { PlayerId = playerId, Page = page, PageSize = pageSize });

            return Ok(playerCasinoWagers);
        }
    }
}
