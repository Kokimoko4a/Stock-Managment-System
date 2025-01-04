namespace SMS.Services.Interfaces
{
    public interface IManagerService
    {
        public Task BecomeManager(string email);
    }
}
