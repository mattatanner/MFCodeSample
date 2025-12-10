public class UserService
{
    private string? _username;

    public string? Username
    {
        get => _username;
        private set
        {
            _username = value;
            NotifyStateChanged();
        }
    }

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Username);

    public void SetUsername(string username)
    {
        Username = username;
    }

    // Event to notify components
    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
