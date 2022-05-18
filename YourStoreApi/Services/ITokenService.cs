namespace YourStoreApi.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
