namespace SMS.Services.Interfaces
{
    using SMS.Dtos.User;
    using SMS.Models;

    public interface IUserService
    {
        public ApplicationUser CreateUser(RegisterDTO registerDTO);

        public ApplicationUser GetUserByEmail(string email);

        
    }
}
