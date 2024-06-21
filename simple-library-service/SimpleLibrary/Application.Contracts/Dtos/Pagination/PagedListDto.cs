namespace SimpleLibrary.Application.Contracts.Dtos.Pagination;

public record PagedListDto<T>(ICollection<T> Items, uint TotalPages, uint CurrentPage);