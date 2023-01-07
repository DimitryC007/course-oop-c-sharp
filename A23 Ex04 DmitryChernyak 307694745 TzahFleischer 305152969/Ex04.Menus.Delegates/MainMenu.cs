using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private MenuItem _mainMenu;
        private Stack<MenuItem> _stack = new Stack<MenuItem>();

        public MainMenu(string menuName)
        {
            _mainMenu = new MenuItem(menuName);
            _stack.Push(_mainMenu);
        }

        public void AddMainMenuItem(MenuItem menuItem)
        {
            _mainMenu.Items.Add(menuItem);
        }

        public void Show()
        {
            while (_stack.Count > 0)
            {
                MenuItem currentMenu = _stack.Peek();
                showCurrentMenu(currentMenu, _stack.Count > 1);
                int userInput = getUserInput();

                if (userInput != 0)
                {
                    MenuItem menuItem = currentMenu.Items[userInput - 1].Execute();
                    
                    if (!menuItem.isExecutable)
                    {
                        _stack.Push(menuItem);
                    }
                }
                else
                {
                    _stack.Pop();
                }
            }
        }

        private int getUserInput()
        {
            string enterRequestMessage = "Enter your request:";
            Console.WriteLine(enterRequestMessage);
            string input = Console.ReadLine();

            while (!int.TryParse(input, out int choise) || choise < 0 || choise > _mainMenu.Items.Count)
            {
                Console.WriteLine("Input not valid");
                Console.WriteLine(enterRequestMessage);
                input = Console.ReadLine();
            }

            return int.Parse(input);
        }

        private void showCurrentMenu(MenuItem currentMenu, bool showBack)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            sb.Append(getMenuNameString(currentMenu.Name));
            sb.Append(getSeperatorString());

            for (int i = 0; i < currentMenu.Items.Count; i++)
            {
                sb.Append(getMenuItemString(i + 1, currentMenu.Items[i].Name));
            }

            sb.Append(getMenuItemString(0, showBack ? "Back" : "Exit"));
            sb.Append(getSeperatorString());
            Console.WriteLine(sb.ToString());
        }

        private string getSeperatorString()
        {
            return $"{Environment.NewLine}---------------------";
        }

        private string getMenuNameString(string menuName)
        {
            return $"**{menuName}**";
        }

        private string getMenuItemString(int i, string name)
        {
            return $"{Environment.NewLine}{i} -> {name}";
        }
    }
}
