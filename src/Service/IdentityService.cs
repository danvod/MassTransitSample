namespace Service;

public class IdentityService : IIdentityService
{
    public string Token { get; set; }

    public Task<string> GetToken()
    {
        var token = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 20)
                                         .Select(s => s[new Random().Next(s.Length)]).ToArray());

        Console.WriteLine("Set Token value: {0}", token);
        
        return Task.FromResult(token);
    }
    

    public void SetToken(string token)
    {
        Token = token;
    }
}