using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private IAction m_Action;
        private readonly List<MenuItem> r_Items = new List<MenuItem>();
        private readonly string r_Name;
        internal string Name => r_Name;
        internal List<MenuItem> Items => r_Items;
        internal bool IsExecutable => r_Items.Count == 0;

        public MenuItem(string i_Name, IAction i_Action = null)
        {
            r_Name = i_Name;
            m_Action = i_Action;
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            r_Items.Add(i_MenuItem);
        }

        internal MenuItem Execute()
        {
            if (IsExecutable && m_Action != null)
            {
                m_Action.Execute();
            }

            return this;
        }
    }
}
