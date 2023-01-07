using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class MenuWithDelegates
    {
        public static void Run()
        {
            MainMenu mainMenu = new MainMenu("Delegates Main Menu");
            MenuItem mainMenuItem1 = new MenuItem("Version and Uppercase");
            
            mainMenuItem1.AddMenuItem(new MenuItem("Show Version",Methods.ShowVersion));
            mainMenuItem1.AddMenuItem(new MenuItem("Count Uppercase",Methods.CountUppercase));

            MenuItem mainMenuItem2 = new MenuItem("Show Date/Time");
            
            mainMenuItem2.AddMenuItem(new MenuItem("Show Date", Methods.ShowDate));
            mainMenuItem2.AddMenuItem(new MenuItem("Show Time", Methods.ShowTime));
            mainMenu.AddMainMenuItem(mainMenuItem1);
            mainMenu.AddMainMenuItem(mainMenuItem2);
            mainMenu.Show();
        }
    }
}
