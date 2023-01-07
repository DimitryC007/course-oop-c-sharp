using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private IAction _action;
        private readonly List<MenuItem> _items = new List<MenuItem>();
        private readonly string _name;
        internal string Name => _name;
        internal List<MenuItem> Items => _items;
        internal bool isExecutable => _items.Count == 0;

        public MenuItem(string name, IAction action = null)
        {
            _name = name;
            _action = action;
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            _items.Add(menuItem);
        }

        internal MenuItem Execute()
        {
            if (isExecutable && _action != null)
            {
                _action.Execute();
            }

            return this;
        }
    }
}
