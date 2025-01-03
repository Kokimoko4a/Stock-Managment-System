namespace SMS.Services.Interfaces
{
    using SMS.Dtos;
    using SMS.Models;

    public interface IUserService
    {
        public ApplicationUser CreateUser(RegisterDTO registerDTO);
    }
}
