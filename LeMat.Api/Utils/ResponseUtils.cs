using LeMat.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Utils;

public class ResponseUtils : ControllerBase {
	public IActionResult ResponseToAction(Response response) {
		if (response.IsSuccess)
			return Ok(response);
		return BadRequest(response);
	}
}