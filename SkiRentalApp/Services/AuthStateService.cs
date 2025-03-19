namespace SkiRentalApp.Services
{
	public class AuthStateService
	{
		public bool IsLoggedIn { get; private set; } = false;
		public string? UserName { get; private set; }
		public Guid? EmployeeId { get; private set; }

		public event Action? AuthStateChanged;

		public void Login(string userName, Guid? employeeId)
		{
			IsLoggedIn = true;
			UserName = userName;
			EmployeeId = employeeId;

			NotifyAuthStateChanged();
		}

		public void Logout()
		{
			IsLoggedIn = false;
			UserName = null;
			EmployeeId = null;

			NotifyAuthStateChanged();
		}

		private void NotifyAuthStateChanged()
		{
			AuthStateChanged?.Invoke();
		}
	}
}
