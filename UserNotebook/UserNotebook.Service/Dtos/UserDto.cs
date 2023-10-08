namespace UserNotebook.Service.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string? ShoeSize { get; set; }
    }
}
