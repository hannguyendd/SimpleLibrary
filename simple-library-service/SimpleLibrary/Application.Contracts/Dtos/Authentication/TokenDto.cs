namespace SimpleLibrary.Application.Contracts.Dtos.Authentication;

public record TokenDto(string Token, DateTime ExpiredAt);