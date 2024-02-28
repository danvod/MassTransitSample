namespace Service;

public interface IIdentityService
{
    public string Token { get; set; }

    Task<string> GetToken();
    void SetToken(string token);
}