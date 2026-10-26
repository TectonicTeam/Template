namespace BC.TempateFull.Shared.Domain.Dtos;

public class TokenClaims
{
    public string UserName { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? MemberId { get; set; }
    public string? ClubId { get; set; }
}