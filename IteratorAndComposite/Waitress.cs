using IteratorAndComposite.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorAndComposite
{
    public class Waitress
    {
        MenuComponent _allMenus;

        public Waitress(MenuComponent menuComponent)
        {
            _allMenus = menuComponent;
        }
        public void PrintMenu()
        {
            _allMenus.Print();
        }

        public void PrintBreakfastMenu()
        {
            Console.WriteLine("Print Breakfast Menu");
        }

        public void PrintLunchMenu()
        {
            Console.WriteLine("Print Lunch Menu");
        }

        public void PrintVegetarianMenu()
        {
            Console.WriteLine("Print Vegetarian Menu");
            IEnumerator menuComponentIterator = _allMenus.CreaterIterator();
            while (menuComponentIterator.MoveNext())
            {
                MenuComponent menuComponent = (MenuComponent)menuComponentIterator.Current;
                try
                {
                    if (menuComponent.Vegetarian)
                    {
                        menuComponent.Print();
                    }
                }
                catch (NotSupportedException)
                {
                    // This is expected for menu items
                }
            }
        }
    }
}
