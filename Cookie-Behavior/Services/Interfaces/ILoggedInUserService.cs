namespace Cookie_Behavior.Services.Interfaces
{
    public interface ILoggedInUserService
    {
        public long UserId { get; }
        public string UserEmail { get; }
        public string MobileNumber { get; }
        public string UserName { get; }
        public string RequestOrigin { get; }
        public bool IsAuthenticated { get; }
        public List<string> Roles { get; }
    }
}
