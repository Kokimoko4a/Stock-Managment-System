namespace SMS.Factory
{
    using SMS.Dtos.User;
    using SMS.Models;

    public interface IFactoryService
    {
        public ApplicationUser CreateUser(RegisterDTO registerDTO);

        public Manager CreateManager(ApplicationUser applicationUser);
    }
}
