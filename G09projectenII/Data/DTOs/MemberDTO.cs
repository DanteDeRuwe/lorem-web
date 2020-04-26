namespace G09projectenII.Models.DTOs
{
    public class MemberDTO
    {
        public string Name { get; set; }
        public string Profilepicpath { get; set; }
        public string Username { get; set; }

        public MemberDTO(Member member)
        {
            Name = $"{member.Firstname} {member.Lastname}";
            Profilepicpath = member.Profilepicpath;
            Username = member.Username;
        }
    }
}