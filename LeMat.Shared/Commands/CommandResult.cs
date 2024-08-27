using System.Text.Json.Serialization;

namespace LeMat.Shared.Commands;

public class CommandResult : ICommandResult {
	private int _code = Configuration.DefaultStatusCode;
	public CommandResult() { }

	public CommandResult(object data, int code = Configuration.DefaultStatusCode, string message = null) {
		_code = code;
		Message = message;
		Data = data;
	}

	public string Message { get; set; }
	public object Data { get; set; }
	[JsonIgnore]
	public bool IsSuccess => _code is >= 200 and <= 299;
}
