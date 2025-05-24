using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace SkiRentalApp.Services
{
	public class AuthStateService
	{
        private readonly ProtectedSessionStorage sessionStorage;

        public bool IsLoggedIn { get; private set; } = false;
		public string? UserName { get; private set; }
		public Guid? EmployeeId { get; private set; }

		public event Action? AuthStateChanged;


		public AuthStateService(ProtectedSessionStorage sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }

		/// <summary>
		/// Loggt den user ein und speichert die values im storage
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="employeeId"></param>
		/// <returns></returns>
        public async Task Login(string userName, Guid employeeId)
		{
			IsLoggedIn = true;
			UserName = userName;
			EmployeeId = employeeId;

			await sessionStorage.SetAsync(nameof(UserName), UserName);
			await sessionStorage.SetAsync(nameof(EmployeeId), EmployeeId);

			NotifyAuthStateChanged();
		}

		/// <summary>
		/// Loggt den user aus und löscht die gespeicherten values im storage
		/// </summary>
		/// <returns></returns>
		public async Task Logout()
		{
			IsLoggedIn = false;
			UserName = null;
			EmployeeId = null;

            await sessionStorage.DeleteAsync(nameof(UserName));
            await sessionStorage.DeleteAsync(nameof(EmployeeId));

            NotifyAuthStateChanged();
		}

		/// <summary>
		/// inialisiert einen im storage gespeicherten user um, falls vorhanden, den authstate islogged in auf true zu setzen
		/// </summary>
		/// <returns></returns>
		public async Task InitializeAuthState()
		{
			var userName = await sessionStorage.GetAsync<string>(nameof(UserName));
			var employeeId = await sessionStorage.GetAsync<Guid>(nameof(EmployeeId));

			if(userName.Success && employeeId.Success)
			{
				IsLoggedIn = true;
				UserName = userName.Value;
				EmployeeId = employeeId.Value;

				NotifyAuthStateChanged();
			}
		}

		private void NotifyAuthStateChanged()
		{
			AuthStateChanged?.Invoke();
		}
	}
}
