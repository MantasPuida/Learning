using Learning.API.Models.DTOs.Difficulty;
using Learning.API.Models.DTOs.Region;

namespace Learning.API.Models.DTOs.Walk
{
    public record WalkDto(string Name, string Descripton, double LengthInKm, string? WalkImgUrl, DifficultyDto Difficulty, RegionDto Region);
}
