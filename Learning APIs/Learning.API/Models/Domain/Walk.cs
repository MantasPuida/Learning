namespace Learning.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
