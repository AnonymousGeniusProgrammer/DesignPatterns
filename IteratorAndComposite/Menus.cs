using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace IteratorAndComposite
{
    namespace Menus
    {
        /// <summary>
        /// This acts as the common interface for both menus and menu items and allow us to treat them uniformly, which means we can call the same methods on both.
        /// </summary>
        public abstract class MenuComponent
        {
            public virtual void Add(MenuComponent menuComponent)
            {
                throw new NotImplementedException();
            }
            public virtual void Remove(MenuComponent menuComponent)
            {
                throw new NotImplementedException();
            }
            public virtual MenuComponent GetChild(int i)
            {
                throw new NotImplementedException();
            }

            public virtual void Print()
            {
                throw new NotImplementedException();
            }

            public virtual IEnumerator CreaterIterator()
            {
                return new NullIterator();
            }

            public string Name { get; set; }
            public string Description { get; set; }
            public bool Vegetarian { get; set; }
            public double Price { get; set; }

        }

        public class MenuItem : MenuComponent
        {
            public MenuItem(string name, string description, bool vegetarian, double price)
            {
                Name = name;
                Description = description;
                Vegetarian = vegetarian;
                Price = price;
            }

            public override IEnumerator CreaterIterator()
            {
                return new NullIterator();
            }

            public override void Print()
            {
                Console.Write("  " + Name);
                if(Vegetarian)
                {
                    Console.Write("(v)");
                }
                Console.WriteLine(", " + Price);
                Console.WriteLine("     -- " + Description);
            }
        }

        public class NullIterator : IEnumerator<MenuComponent>
        {

            public bool MoveNext()
            {
                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public MenuComponent Current => null;

            object IEnumerator.Current => Current;

            public void Dispose()
            {

            }
        }

        public class Menu: MenuComponent
        {
            List<MenuComponent> _menuComponents = new List<MenuComponent>();


            public Menu(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public override void Add(MenuComponent menuComponent)
            {
                _menuComponents.Add(menuComponent);
            }
            public override void Remove(MenuComponent menuComponent)
            {
                _menuComponents.Remove(menuComponent);
            }
            public override MenuComponent GetChild(int i)
            {
                return (MenuComponent)_menuComponents[i];
            }

            public override IEnumerator<MenuComponent> CreaterIterator()
            {
                return new CompositeIterator(_menuComponents.GetEnumerator());
            }

            public override void Print()
            {
                // here we have used the iterator internally to iterate over the whole composite
                // but we could also provide the iterator in the interface so the client can control and define their own iteration behavior
                Console.Write("\n" + Name);
                Console.WriteLine(", " + Description);
                Console.WriteLine("---------------------");

                IEnumerator iterator = _menuComponents.GetEnumerator();
                while (iterator.MoveNext())
                {
                    MenuComponent menuComponent = (MenuComponent)iterator.Current;
                    menuComponent.Print();
                }
            }
        }

        public class CompositeIterator : IEnumerator<MenuComponent>
        {
            readonly Stack<IEnumerator> _stack = new Stack<IEnumerator>();
            public CompositeIterator(IEnumerator<MenuComponent> iterator)
            {
                _stack.Push(iterator);
            }
            public bool MoveNext()
            {
                  if(_stack.Count == 0)
                {
                    return false;
                }
                
                else
                {
                    IEnumerator iterator = _stack.Peek();
                    if(!iterator.MoveNext())
                    {
                        _stack.Pop();
                        return MoveNext();
                    }
                    else
                    {
                        MenuComponent menuComponent = (MenuComponent)iterator.Current;
                        if(menuComponent is Menu)
                        {
                            _stack.Push(menuComponent.CreaterIterator());
                        }
                        return true;
                    }
                }
            }

            public MenuComponent Current
            {
                get
                {
                    var iterator = _stack.Peek();
                    var menuComponent = (MenuComponent)iterator.Current;
                    if(menuComponent is Menu)
                    {
                        _stack.Push(menuComponent.CreaterIterator());
                    }
                    return menuComponent;
                }
            }

            object IEnumerator.Current => Current;

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                
            }

        }
    }
}
