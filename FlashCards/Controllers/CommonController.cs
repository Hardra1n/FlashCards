using FlashCards.RpcClients;
using Microsoft.AspNetCore.Mvc;

namespace SpacedRep;

[Route("api/")]
public class CommonController : ControllerBase
{
    private SpacedRepRpcPublisher _rpcPublisher;
    public CommonController(SpacedRepRpcPublisher rpcPublisher)
    {
        _rpcPublisher = rpcPublisher;
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        bool spacedRepPing = await _rpcPublisher.SendPing();
        string pingResult = $"This service: {true}" + '\n' + $"Spaced Repetitions service: {spacedRepPing}";
        return Ok(pingResult);
    }
}