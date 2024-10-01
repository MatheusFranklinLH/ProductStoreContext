using LeMat.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Utils;

public static class ResponseUtils {
	public static ActionResult<Response> CreateResponse(Response response) {
		return response.Code switch {
			200 => new OkObjectResult(response),
			404 => new NotFoundObjectResult(response),
			400 => new BadRequestObjectResult(response),
			500 => new ObjectResult(response) { StatusCode = 500 },
			_ => new StatusCodeResult(response.Code) // Padrão para outros códigos
		};
	}
}
