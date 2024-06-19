using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/*
 *   Created by: Matthew Steffan
 *   date:       3/20/21
 *   
 */

namespace fastFoodOrder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creates a new Order
        Order order = new Order();

        // Sets the items on screen to variables to allow for easier addition of new items
        public MainWindow()
        {
            InitializeComponent();

            // Assigns a sandwich type to each radio button 
            sandwichRadio1.Content = Sandwich.SandwichTypes.Chicken;
            sandwichRadio2.Content = Sandwich.SandwichTypes.Hamburger;
            sandwichRadio3.Content = Sandwich.SandwichTypes.Hotdog;
            sandwichRadio4.Content = Sandwich.SandwichTypes.Turkey;
            sandwichRadio5.Content = Sandwich.SandwichTypes.BBQ;

            // Assigns a topping to each check box
            toppingCheckBox1.Content = Sandwich.Toppings.Lettuce;
            toppingCheckBox2.Content = Sandwich.Toppings.Cheese;
            toppingCheckBox3.Content = Sandwich.Toppings.Tomato;
            toppingCheckBox4.Content = Sandwich.Toppings.Bacon;
            toppingCheckBox5.Content = Sandwich.Toppings.Mayo;
            toppingCheckBox6.Content = Sandwich.Toppings.Ketchup;
            toppingCheckBox7.Content = Sandwich.Toppings.Mustard;

            // Adds the prices to the labels displaying the cost of each item
            sandwichPriceLabel.Content = String.Format("Sandwich: {0:C2}", Order.SANDWICHPRICE);
            drinksPriceLabel.Content   = String.Format("Drinks: {0:C2}"  , Order.DRINKPRICE);
            friesPriceLabel.Content    = String.Format("Fries: {0:C2}"   , Order.FRYPRICE);

            // Adds the reciept to the output box
            refreshOrderScreen();
        }

        // When add sandwich is pressed
        private void addSandwich(object sender, RoutedEventArgs e)
        {

            // Clears previous errors from warning box
            exceptionOutput.Content = null;

            try
            {
                // Adds the new sandwich and toppings to a list in Oder.cs
                order.addSandwich(GetSandwich(), GetToppings());

                // Refreshes the order on the screen
                refreshOrderScreen();
            }
            catch(ArgumentException ex)
            {
                exceptionOutput.Content = ex.Message;
            }
 
            return;
        }

        // Gets which sandwich radio button is pressed, 
        // gets the sandwich associated with that button
        private Sandwich.SandwichTypes GetSandwich()
        {
            // Creates a new sandwich object to hold the selected sandwich
            Sandwich.SandwichTypes chosenSandwich = new Sandwich.SandwichTypes();
            
            // If the sandwich is selected, set chosen sandwich and
            // remove the check to prepare form for next sandwich order
            if (sandwichRadio1.IsChecked == true)
            {
                chosenSandwich = (Sandwich.SandwichTypes)sandwichRadio1.Content;
                sandwichRadio1.IsChecked = false;
            }
            else if (sandwichRadio2.IsChecked == true)
            {
                chosenSandwich = (Sandwich.SandwichTypes)sandwichRadio2.Content;
                sandwichRadio2.IsChecked = false;
            }
            else if (sandwichRadio3.IsChecked == true)
            {
                chosenSandwich = (Sandwich.SandwichTypes)sandwichRadio3.Content;
                sandwichRadio3.IsChecked = false;
            }
            else if (sandwichRadio4.IsChecked == true)
            {
                chosenSandwich = (Sandwich.SandwichTypes)sandwichRadio4.Content;
                sandwichRadio4.IsChecked = false;
            }
            else if (sandwichRadio5.IsChecked == true)
            {
                chosenSandwich = (Sandwich.SandwichTypes)sandwichRadio5.Content;
                sandwichRadio5.IsChecked = false;
            }
            else // No sandwich selected
                throw new ArgumentException("No sandwich selected");
            
            return chosenSandwich;
        }

        // Checks which toppings have been selected and adds them to a list
        private List<Sandwich.Toppings> GetToppings()
        {
            // Creates a list that will hold the toppings for each sandwich
            List<Sandwich.Toppings> listOfToppings = new List<Sandwich.Toppings>();

            // If the box is checked, add to list and uncheck for next sandwich
            if (toppingCheckBox1.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox1.Content);
                toppingCheckBox1.IsChecked = false;
            }
            if (toppingCheckBox2.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox2.Content);
                toppingCheckBox2.IsChecked = false;
            }
            if (toppingCheckBox3.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox3.Content);
                toppingCheckBox3.IsChecked = false;
            }
            if (toppingCheckBox4.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox4.Content);
                toppingCheckBox4.IsChecked = false;
            }
            if (toppingCheckBox5.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox5.Content);
                toppingCheckBox5.IsChecked = false;
            }
            if (toppingCheckBox6.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox6.Content);
                toppingCheckBox6.IsChecked = false;
            }
            if (toppingCheckBox7.IsChecked == true)
            {
                listOfToppings.Add((Sandwich.Toppings)toppingCheckBox7.Content);
                toppingCheckBox7.IsChecked = false;
            }
            
            return listOfToppings;
        }
        #region drinks/Fries addition/subtraction from order
        private void addDrink(object sender, RoutedEventArgs e)
        {
            order.addDrink();
            refreshOrderScreen();
        }

        private void removeDrink(object sender, RoutedEventArgs e)
        {
            order.removeDrink();
            refreshOrderScreen();
        }

        private void addFries(object sender, RoutedEventArgs e)
        {
            order.addFries();
            refreshOrderScreen();
        }

        private void removeFries(object sender, RoutedEventArgs e)
        {
            order.removeFries();
            refreshOrderScreen();
        }
        #endregion drinks/Fries addition/subtraction from order
        
        private void refreshOrderScreen() => orderOutput.Text = order.ToString();

        // When clear order button is presed, clears items in order and refreshes order screen
        private void clearOrderPressed(object sender, RoutedEventArgs e)
        {
            order.clearOrder();
            refreshOrderScreen();
        }

        private void removeSandwich(object sender, RoutedEventArgs e)
        {
            order.removeLastSandwich();
            refreshOrderScreen();
        }
    }
}
