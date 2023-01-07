using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        public delegate void MenuItemClickEventHandler();
        public event MenuItemClickEventHandler MenuItemAction;
        private readonly List<MenuItem> _items = new List<MenuItem>();
        private readonly string _name;
        internal string Name => _name;
        internal List<MenuItem> Items => _items;
        internal bool isExecutable => _items.Count == 0;

        public MenuItem(string name, MenuItemClickEventHandler menuItemAction = null)
        {
            _name = name;
            MenuItemAction += menuItemAction;
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            _items.Add(menuItem);
        }

        internal MenuItem Execute()
        {
            if (isExecutable)
            {
                OnMenuItemAction();
            }

            return this;
        }

        internal void OnMenuItemAction()
        {
            MenuItemAction?.Invoke();
        }
    }
}
