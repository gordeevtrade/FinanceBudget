namespace Budget.BuisnessLogic.Sevices.Interface
{
    public interface IGoogleAuthService
    {
        string GetAuthUrl();

        Task<string> GetToken(string code);
    }
}