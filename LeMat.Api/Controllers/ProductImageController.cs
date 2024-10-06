using LeMat.Api.Utils;
using LeMat.Domain.Handlers;
using LeMat.Domain.Requests;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Controllers;

[ApiController]
[Route("v1/products/images")]
public class ProductImageController : ControllerBase {

	public ProductImageController() { }

	[HttpGet]
	public async Task<ActionResult<Response>> GetProductImages(
		int productId,
		[FromServices] ProductImageHandler handler
	) {
		var response = (Response)await handler.Handle(productId);
		return ResponseUtils.CreateResponse(response);
	}

	[HttpPost]
	public async Task<ActionResult<Response>> UploadImage(
		[FromForm] UpdateProductImagesRequest request,
		[FromServices] ProductImageHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		return ResponseUtils.CreateResponse(response);
	}

	[HttpDelete]
	public async Task<ActionResult<Response>> DeleteImage(
		[FromBody] DeleteIdRequest request,
		[FromServices] ProductImageHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		return ResponseUtils.CreateResponse(response);
	}

}
