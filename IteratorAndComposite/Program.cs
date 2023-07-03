using IteratorAndComposite;
using IteratorAndComposite.Menus;

MenuComponent pancackeHouseMenu = new Menu("Pancake House Menu", "Breakfast");
MenuComponent dinnerMenu = new Menu("Dinner Menu", "Lunch");
MenuComponent cafeMenu = new Menu("Cafe Menu", "Dinner");
MenuComponent dessertMenu = new Menu("Dessert Menu", "Dessert of course!");

MenuComponent allMenus = new Menu("All Menus", "All menus combined");
allMenus.Add(pancackeHouseMenu);
allMenus.Add(dinnerMenu);
allMenus.Add(cafeMenu);

pancackeHouseMenu.Add(new MenuItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", true, 2.99));
pancackeHouseMenu.Add(new MenuItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99));
pancackeHouseMenu.Add(new MenuItem("Blueberry Pancakes", "Pancakes made with fresh blueberries", true, 3.49));
pancackeHouseMenu.Add(new MenuItem("Waffles", "Waffles, with your choice of blueberries or strawberries", true, 3.59));

dinnerMenu.Add(new MenuItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", true, 3.89));
dinnerMenu.Add(dessertMenu);

dessertMenu.Add(new MenuItem("Apple Pie", "Apple pie with a flakey crust, topped with vanilla icecream", true, 1.59));

Waitress waitress = new Waitress(allMenus);
waitress.PrintVegetarianMenu();