namespace SMS.Factory
{
    using SMS.Dtos;
    using SMS.Models;

    public interface IFactoryService
    {
        public ApplicationUser CreateUser(RegisterDTO registerDTO);
    }
}
