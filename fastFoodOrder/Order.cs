using System;
using System.Collections.Generic;

/*
 *   Created by: Matthew Steffan
 *   date:       3/20/21
 *   
 */

namespace fastFoodOrder
{
    // This is a Sandwich structure. It holds all possible types of Sandwiches and toppings. 
    // It also stores the information about the sandwich.
    struct Sandwich
    {
        public enum SandwichTypes  {Chicken, Hamburger, Hotdog, Turkey, BBQ}
        public enum Toppings       {None, Lettuce, Cheese, Tomato, Bacon, Mayo, Ketchup, Mustard};

        public SandwichTypes  sandwichType;
        public List<Toppings> sandwitchToppings;
                                
        // Constructor that builds the sandwich each time it is created
        public Sandwich(SandwichTypes sdwType, List<Sandwich.Toppings> toppings)
        {
            // assigns the type and price to the sandwich
            sandwichType  = sdwType;

            // Creates a list to hold all the toppings on the sandwich 
            // and adds the toppings passed in to the list
            sandwitchToppings = new List<Toppings>();
            sandwitchToppings = toppings;
        }
    }

    // Contains the items in an order and manages the manipulation of the order
    class Order
    {
        // Constants 
        public const float DRINKPRICE     = 1.75f,
                           FRYPRICE       = 1.30f,
                           SANDWICHPRICE  = 3.00f,
                           TAX            =  .07f;

        // Local variables
        private int   numberOfFries,
                      numberOfDrinks;
        private float totalCostDrinks,
                      totalCostFries,
                      totalCostSandwich,
                      totalCost,
                      totalTax,
                      totalCostAfterTax;

        // Holds the completed order in a string to be displayed
        private String sItemsInOrder = string.Empty; 

        // Master list to hold the Sandwiches created
        List<Sandwich> listOfSandwiches; 
        
        // Order constructor
        public Order()
        {
            // Sets the counts to 0 every time a order is placed.
            numberOfDrinks = 0;
            numberOfFries  = 0;

            // Initializes the master list of Sandwiches
            listOfSandwiches = new List<Sandwich>();
        }

        // Adds the sandwich to the sandwich collection
        public void addSandwich(Sandwich.SandwichTypes chosenSandwich, List<Sandwich.Toppings> toppings)
        {
            listOfSandwiches.Add(new Sandwich(chosenSandwich,  toppings));
            return;
        }

        // Methods to add and remove drinks 
        public void addDrink()
        {
            numberOfDrinks++;
            calculateCost();
            return;
        }

        public void removeDrink()
        {
            // Prevents negative drinks
            if (numberOfDrinks > 0) 
                numberOfDrinks--;

            calculateCost();
            return;
        }

        // Methods to add and remove fries
        public void addFries()
        {
            numberOfFries++;
            calculateCost();
            return;
        }

        public void removeFries()
        {
            // Prevents negative fries
            if (numberOfFries > 0)
                numberOfFries--;

            calculateCost();
            return;
        }

        // Clears all items in the order
        public void clearOrder()
        {
            numberOfDrinks = 0;
            numberOfFries  = 0;
            listOfSandwiches.Clear();
            return;
        }

        public void removeLastSandwich()
        {
            if (listOfSandwiches.Count > 0)
                listOfSandwiches.RemoveAt(listOfSandwiches.Count - 1);
            return;
        }
        
        // Returns a string containing the items to be ordered
        public override string ToString()
        {
            // Resets all strings to hold new values
            sItemsInOrder = string.Empty; 

            // If drinks in order, add them to order string
            if (numberOfDrinks > 0)
                sItemsInOrder += String.Format("{0,-10}     {1,10:C2}\n", $"{numberOfDrinks} Drinks", totalCostDrinks);

            // If Fries in oder, add them to order string
            if (numberOfFries > 0)
                sItemsInOrder += String.Format("{0,-10}     {1,10:C2}\n", $"{numberOfFries} Fries ", totalCostFries);

            // Loops through each sandwich and appends to the order
            foreach (Sandwich sandwich in listOfSandwiches)
            {
                // Adds the sandwich to a string with a label so we can append toppings if needed
                sItemsInOrder += String.Format("{0,-10}     {1,10:C2}\n", sandwich.sandwichType, SANDWICHPRICE);

                // If the sandwich has toppings, add them to the sandwich string
                if (sandwich.sandwitchToppings.Count > 0)
                {
                    // Loops through each topping on the sandwich and adds them to the topping string
                    foreach (Sandwich.Toppings topping in sandwich.sandwitchToppings)
                    {
                        sItemsInOrder += String.Format("  -{0,-10}\n", topping);
                    } 
                }
            }

            // Calculates the cost 
            calculateCost();

            // Appends the end of the reciept to the order string
            sItemsInOrder += String.Format("------------------------- \n" +
                                           "Cost without Tax: {0,7:C2}\n" +
                                           "        Tax Rate: {1,7:P0}\n" +
                                           "             Tax: {2,7:C2}\n" +
                                           "========================= \n" +
                                           "Total:            {3,7:C2}\n",
                                           totalCost, TAX, totalTax,
                                           totalCostAfterTax);
            return sItemsInOrder;
        }

        // Calculates the cost of all items in the order
        public void calculateCost() 
        {
            totalCostDrinks   = DRINKPRICE    * numberOfDrinks;
            totalCostFries    = FRYPRICE      * numberOfFries;
            totalCostSandwich = SANDWICHPRICE * listOfSandwiches.Count;

            totalCost         = totalCostDrinks  + totalCostFries  + totalCostSandwich;
            totalTax          = TAX * totalCost;
            totalCostAfterTax = totalCost + (TAX * totalCost);

            return;
        }
    }
}
