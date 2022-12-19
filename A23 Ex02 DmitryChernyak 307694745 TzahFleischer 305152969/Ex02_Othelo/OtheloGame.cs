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

        public void PlayGame()
        {
            bool anotherRound = true;
            while (anotherRound)
            {
                _userInterface.LaunchGame();
                anotherRound = _userInterface.PlayGame();
            }
        }
    }
}
