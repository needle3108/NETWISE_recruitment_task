namespace ConsoleApplication{
    public interface IConnectService{
        Task<string> GetAsync();
    }
}