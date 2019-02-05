using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// Creates an instance of one type of drink
    /// </summary>
    public class Drink
    {
        /// <summary>
        /// Number of cans available for drink type
        /// </summary>
        private int numberOfDrinks;
        /// <summary>
        /// Price of the drink
        /// </summary>
        private int costOfDrink;
        /// <summary>
        /// Light to indicate if there are no drinks of this type available
        /// </summary>
        private Light soldOutLight;
        /// <summary>
        /// Light to indicate if, with the amount of money right now, the drink can be purchased
        /// </summary>
        private Light purchasableLight;
        /// <summary>
        /// Device to dispense a can
        /// </summary>
        private CanDispenser canDispenser;

        /// <summary>
        /// Creates a drink type
        /// </summary>
        /// <param name="numberOfDrinks">Indicates number of drinks</param>
        /// <param name="costOfDrink">Indicates price of drink</param>
        /// <param name="soldOutLight">Indicates whether can is sold out</param>
        /// <param name="purchasableLight">Indicates whether user can purchase the drink</param>
        /// <param name="canDispenser">Dispenses cans when purchased</param>
        public Drink(int numberOfDrinks, int costOfDrink, Light soldOutLight, Light purchasableLight, CanDispenser canDispenser)
        {
            this.numberOfDrinks = numberOfDrinks;
            this.costOfDrink = costOfDrink;
            this.soldOutLight = soldOutLight;
            this.purchasableLight = purchasableLight;
            this.canDispenser = canDispenser;
        }

        /// <summary>
        /// Indicates the number of drinks in the machine.
        /// </summary>
        public int Count
        {
            get
            {
                return numberOfDrinks;
            }
        }

        /// <summary>
        /// Turns on purchase light based on the amount of money that has been inserted.
        /// </summary>
        /// <param name="inserted">Amount of money that has been inserted.</param>
        public void TurnOnPurchaseLight(int inserted)
        {
            if (inserted >= costOfDrink && !soldOutLight.IsOn()) { purchasableLight.TurnOn(); }
        }

        /// <summary>
        /// Indicates whether or not the purchase light is on.
        /// </summary>
        /// <returns>Returns true if the drink can be purchased</returns>
        public bool PurchaseOn()
        {
            return purchasableLight.IsOn();
        }

        /// <summary>
        /// Dispenses a can, decreases the number of cans, and turns on soldOutLights accordingly
        /// </summary>
        public void DispenseCan()
        {
            canDispenser.Actuate();
            numberOfDrinks--;
            if (numberOfDrinks == 0) { soldOutLight.TurnOn(); }
        }

        /// <summary>
        /// Determines the amount of change that needs to be returned for a purchased drink
        /// </summary>
        /// <param name="inserted">Amount of money that has been inserted</param>
        /// <returns>Amount of money that needs to be returned</returns>
        public int CalculateChangeValue(int inserted)
        {
            return inserted - costOfDrink;
        }

        /// <summary>
        /// Clears the lights.
        /// </summary>
        public void ClearLights()
        {
            purchasableLight.TurnOff();
        }

        /// <summary>
        /// Returns drink values to what they were set at when the machine was initialized.
        /// </summary>
        public void ResetDrinks()
        {
            numberOfDrinks = VendingMachine.NUMCANTYPES;
            soldOutLight.TurnOff();
            purchasableLight.TurnOff();
            canDispenser.Clear();
        }
    }
}
