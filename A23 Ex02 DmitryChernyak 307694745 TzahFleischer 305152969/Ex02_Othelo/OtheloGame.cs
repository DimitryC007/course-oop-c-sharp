using OtheloUI;

namespace Ex02_Othelo
{
    public class OtheloGame
    {
        private UserInterface m_UserInterface;

        public OtheloGame()
        {
            m_UserInterface = new UserInterface();
        }

        public void PlayGame()
        {
            bool anotherRound = true;
            while (anotherRound)
            {
                m_UserInterface.LaunchGame();
                anotherRound = m_UserInterface.PlayGame();
            }
        }
    }
}
