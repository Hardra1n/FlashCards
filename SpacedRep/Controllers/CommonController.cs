using Microsoft.AspNetCore.Mvc;
using SpacedRep.RpcClients;

namespace SpacedRep;

[Route("api/")]
public class CommonController : ControllerBase
{
    private FlashCardsRpcPublisher _rpcPublisher;
    public CommonController(FlashCardsRpcPublisher rpcPublisher)
    {
        _rpcPublisher = rpcPublisher;
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        bool flashCardsPing = await _rpcPublisher.SendPing();
        string pingResult = $"This service: {true}" + '\n' + $"Flash Cards service: {flashCardsPing}";
        return Ok(pingResult);
    }
}