using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class MenuWithInterfaces
    {
        public static void Run()
        {
            MainMenu mainMenu = new MainMenu("Interfaces Main Menu");
            MenuItem mainMenuItem1 = new MenuItem("Version and Uppercase");
            
            mainMenuItem1.AddMenuItem(new MenuItem("Show Version", new ShowVersion()));
            mainMenuItem1.AddMenuItem(new MenuItem("Count Uppercase", new CountUppercase()));

            MenuItem mainMenuItem2 = new MenuItem("Show Date/Time");
            
            mainMenuItem2.AddMenuItem(new MenuItem("Show Date", new ShowDate()));
            mainMenuItem2.AddMenuItem(new MenuItem("Show Time", new ShowTime()));
            mainMenu.AddMainMenuItem(mainMenuItem1);
            mainMenu.AddMainMenuItem(mainMenuItem2);
            mainMenu.Show();
        }
    }
}
