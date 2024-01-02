namespace Learning.API.Models.DTOs.Walk
{
    public record CreateWalkDto(string Name, string Descripton, double LengthInKm, string? WalkImgUrl, Guid DifficultyId, Guid RegionId);
}
