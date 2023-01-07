using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private MenuItem m_MainMenu;
        private Stack<MenuItem> m_CurrentMenusStack = new Stack<MenuItem>();

        public MainMenu(string i_MenuName)
        {
            m_MainMenu = new MenuItem(i_MenuName);
            m_CurrentMenusStack.Push(m_MainMenu);
        }

        public void AddMainMenuItem(MenuItem i_MenuItem)
        {
            m_MainMenu.Items.Add(i_MenuItem);
        }

        public void Show()
        {
            while (m_CurrentMenusStack.Count > 0)
            {
                MenuItem currentMenu = m_CurrentMenusStack.Peek();
                
                showCurrentMenu(currentMenu, m_CurrentMenusStack.Count > 1);
                
                int userInput = getUserInput();

                if (userInput != 0)
                {
                    MenuItem menuItem = currentMenu.Items[userInput - 1].Execute();
                    
                    if (!menuItem.isExecutable)
                    {
                        m_CurrentMenusStack.Push(menuItem);
                    }
                }
                else
                {
                    m_CurrentMenusStack.Pop();
                }
            }
        }

        private int getUserInput()
        {
            string enterRequestMessage = "Enter your request:";
            
            Console.WriteLine(enterRequestMessage);
            
            string input = Console.ReadLine();

            while (!int.TryParse(input, out int choice) || choice < 0 || choice > m_MainMenu.Items.Count)
            {
                Console.WriteLine("Input not valid");
                Console.WriteLine(enterRequestMessage);
                input = Console.ReadLine();
            }

            return int.Parse(input);
        }

        private void showCurrentMenu(MenuItem i_CurrentMenu, bool i_ShowBack)
        {
            Console.Clear();
            
            StringBuilder sb = new StringBuilder();
            
            sb.Append(getMenuNameString(i_CurrentMenu.Name));
            sb.Append(getSeperatorString());

            for (int i = 0; i < i_CurrentMenu.Items.Count; i++)
            {
                sb.Append(getMenuItemString(i + 1, i_CurrentMenu.Items[i].Name));
            }

            sb.Append(getMenuItemString(0, i_ShowBack ? "Back" : "Exit"));
            sb.Append(getSeperatorString());
            Console.WriteLine(sb.ToString());
        }

        private string getSeperatorString()
        {
            return $"{Environment.NewLine}---------------------";
        }

        private string getMenuNameString(string i_MenuName)
        {
            return $"**{i_MenuName}**";
        }

        private string getMenuItemString(int i_MenuItemIndex, string i_MenuName)
        {
            return $"{Environment.NewLine}{i_MenuItemIndex} -> {i_MenuName}";
        }
    }
}
