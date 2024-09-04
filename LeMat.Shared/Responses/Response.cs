using System.Text.Json.Serialization;

namespace LeMat.Shared.Responses;

public class Response : IResponse {
	private int _code = Configuration.DefaultStatusCode;
	public Response() { }

	public Response(object data, int code = Configuration.DefaultStatusCode, string message = null) {
		_code = code;
		Message = message;
		Data = data;
	}

	public string Message { get; private set; }
	public object Data { get; private set; }
	[JsonIgnore]
	public bool IsSuccess => _code is >= 200 and <= 299;
}
