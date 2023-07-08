using IteratorAndComposite.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            //IEnumerator menuComponentIterator = _allMenus.CreaterIterator();
            //while (menuComponentIterator.MoveNext())
            //{
            //    //FIXME: in c#, by calling MoveNext(), the iterator is already advanced to the next element if it exists
            //    MenuComponent menuComponent = (MenuComponent)menuComponentIterator.Current;
            //    try
            //    {
            //        if (menuComponent.Vegetarian)
            //        {
            //            menuComponent.Print();
            //        }
            //    }
            //    catch (NotSupportedException)
            //    {
            //        // This is expected for menu items
            //    }
            //}
            Desendants(_allMenus).Where(x => x.Vegetarian == true).ToList().ForEach(x => x.Print());

        }
        public IEnumerable<MenuComponent> Desendants(MenuComponent root)
        {
            var nodes = new Stack<MenuComponent>(new[] { root });
            while (nodes.Any())
            {
                MenuComponent node = nodes.Pop();
                yield return node;
                foreach (var n in node.GetChildren()) nodes.Push(n);
            }
        }
    }
}
