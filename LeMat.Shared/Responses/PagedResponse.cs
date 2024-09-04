using System.Text.Json.Serialization;

namespace LeMat.Shared.Responses;

public class PagedResponse : Response {
	[JsonConstructor]
	public PagedResponse(
		object data,
		int totalCount,
		int currentPage = 1,
		int pageSize = Configuration.DefaultPageSize)
		: base(data) {
		TotalCount = totalCount;
		CurrentPage = currentPage;
		PageSize = pageSize;
	}

	public PagedResponse(
		object data,
		int code = Configuration.DefaultStatusCode,
		string message = null)
		: base(data, code, message) {
	}

	public int CurrentPage { get; set; }
	public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
	public int PageSize { get; set; } = Configuration.DefaultPageSize;
	public int TotalCount { get; set; }
}