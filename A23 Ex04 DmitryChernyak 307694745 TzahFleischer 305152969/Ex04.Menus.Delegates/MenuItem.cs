using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        public delegate void MenuItemClickEventHandler();
        public event MenuItemClickEventHandler MenuItemChoice;
        private readonly List<MenuItem> r_Items = new List<MenuItem>();
        private readonly string r_Name;
        internal string Name => r_Name;
        internal List<MenuItem> Items => r_Items;
        internal bool isExecutable => r_Items.Count == 0;

        public MenuItem(string i_Name, MenuItemClickEventHandler i_MenuItemChoice = null)
        {
            r_Name = i_Name;
            MenuItemChoice += i_MenuItemChoice;
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            r_Items.Add(i_MenuItem);
        }

        internal MenuItem Execute()
        {
            if (isExecutable)
            {
                OnMenuItemChoice();
            }

            return this;
        }

        internal void OnMenuItemChoice()
        {
            MenuItemChoice?.Invoke();
        }
    }
}
