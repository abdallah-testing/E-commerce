namespace E_CommerceSystem.DTO.Account
{
    public class GetUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
