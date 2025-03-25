using Microsoft.AspNetCore.Mvc;
using BlazorApp;

namespace BlazorApp;

[ApiController]
[Route("api/[controller]")]
public class AppealsController : ControllerBase
{
    private readonly IAppealFormService _appealService;

    public AppealsController(IAppealFormService appealService)
    {
        _appealService = appealService;
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] StatusUpdateModel model)
    {
        var success = await _appealService.UpdateStatusAsync(id, model.Status);
        if (!success)
            return NotFound();

        return Ok(new { message = "Status updated", newStatus = model.Status });
    }

    public class StatusUpdateModel
    {
        public string Status { get; set; }
    }
}