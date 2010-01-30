
namespace DosboxApp {
	public class AppConfig {
		public AppConfig() {
			m_GameListFormConfig = new GameListFormConfig();
			m_GameConfig = new GameConfig();
		}

		public FormConfig GameListFormConfig {
			get { return m_GameListFormConfig; }
		}

		public GameConfig GameConfig {
			get { return m_GameConfig; }
		}

		FormConfig m_GameListFormConfig;
		GameConfig m_GameConfig;
	}
}
