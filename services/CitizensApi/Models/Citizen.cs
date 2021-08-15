namespace CitizensApi.Models
{
    public class Citizen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CitizenTypeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}