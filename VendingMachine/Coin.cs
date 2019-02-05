using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// Coin that keeps track of value of coin and how many of that value coin are in the machine.
    /// </summary>
    public class Coin
    {
        /// <summary>
        /// Keeps track of how much the coin is worth.
        /// </summary>
        private int value;
        /// <summary>
        /// Keeps track of how many of those coins are in the machine at any given time.
        /// </summary>
        private int numCoins;
        /// <summary>
        /// Dispenses coins and indicates how many of those coins have been dispensed. 
        /// </summary>
        private CoinDispenser coinDispenser;
        /// <summary>
        /// Keeps a tally of how many coins should be dispensed so that value doesn't have to be calculated twice.
        /// </summary>
        private int numberToDispense;

        /// <summary>
        /// Creates a coin
        /// </summary>
        /// <param name="value">Currency value of the coin</param>
        /// <param name="numCoins">Number of coins in machine when initialized</param>
        /// <param name="coinDispenser">Dispenser to dispense change</param>
        public Coin(int value, int numCoins, CoinDispenser coinDispenser)
        {
            this.value = value;
            this.numCoins = numCoins;
            this.coinDispenser = coinDispenser;
            numberToDispense = 0;
        }

        /// <summary>
        /// Gets number of coins in the machine. For debugging purposes only
        /// </summary>
        public int Count
        {
            get
            {
                return numCoins;
            }
        }

        /// <summary>
        /// Adds a coin to the machine
        /// </summary>
        /// <returns>Returns updated number of coins</returns>
        public int AddCoin()
        {
            numCoins++;
            return value;
        }

        /// <summary>
        /// Determines how many of a certain type of coin should be dispensed for change
        /// </summary>
        /// <param name="amount">Amount of change that needs to be made.</param>
        /// <returns>Amount of change that still needs to be made after this type of coin is taken into account</returns>
        public int DetermineCoinsToDispense(int amount)
        {
            numberToDispense = amount / value;
            if (numberToDispense > numCoins || (numberToDispense > 0 && numCoins == 0))
            {
                numberToDispense = numCoins; //Makes sure that coin count does not go negative.
                return amount - (numberToDispense * value);
            }
            return amount - (numberToDispense * value); //Case in which the number of coins available is still greater than 0.
        }

        /// <summary>
        /// Dispenses change based on the value determined in DetermineCoinsToDispense.
        /// </summary>
        public void DispenseChange()
        {
            coinDispenser.Actuate(numberToDispense);
            numCoins -= numberToDispense;
            numberToDispense = 0;
        }

        /// <summary>
        /// Resets values for coins to initialization values
        /// </summary>
        /// <param name="index"></param>
        public void ResetCoins(int index)
        {
            numCoins = VendingMachine.NUMCOINS[index];
            numberToDispense = 0;
            coinDispenser.Clear();
        }
    }
}
