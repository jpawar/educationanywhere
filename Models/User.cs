namespace Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public string Grade { get; set; }

        public Enums.Role Role { get; set; }

    }
}