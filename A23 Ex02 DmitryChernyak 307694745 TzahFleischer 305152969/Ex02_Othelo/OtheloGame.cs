using OtheloUI;

namespace Ex02_Othelo
{
    public class OtheloGame
    {
        private UserInterface _userInterface;

        public OtheloGame()
        {
            _userInterface = new UserInterface();
        }

        public void LaunchGame()
        {
            _userInterface.LaunchGame();
        }

        public void PlayGame()
        {
            _userInterface.PlayGame();
        }
    }
}
