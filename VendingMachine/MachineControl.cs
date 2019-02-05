using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class MachineControl
    {
        /// <summary>
        /// Keeps track of how much money has been inserted in a session.
        /// </summary>
        private int moneyInserted;
        /// <summary>
        /// Keeps a list of drinks so all individual drink types may be accessed.
        /// </summary>
        private List<Drink> drinks;
        /// <summary>
        /// Keeps an array of coins so all individual coin types may be accessed.
        /// </summary>
        private Coin[] coins;
        /// <summary>
        /// Device to display the amount of money that has been entered for a session.
        /// </summary>
        private AmountDisplay amountDisplay;
        /// <summary>
        /// Light to indicate that the machine cannot make change.
        /// </summary>
        private TimerLight noChange;

        /// <summary>
        /// Creates instance of this contoller
        /// </summary>
        /// <param name="drinks">List of drinks for controller to access</param>
        /// <param name="coins">Array of coins for controller to access</param>
        /// <param name="amountDisplay">Device for controller to display the amount of money inserted.</param>
        /// <param name="noChange">Light for contoller to indicate if change cannot be made.</param>
        public MachineControl(List<Drink> drinks, Coin[] coins, AmountDisplay amountDisplay, TimerLight noChange)
        {
            moneyInserted = 0;
            this.drinks = drinks;
            this.coins = coins;
            this.amountDisplay = amountDisplay;
            this.noChange = noChange;
        }

        /// <summary>
        /// Carries out process for when coin has been added including incrementing coins, updating amount, and turning on purchase lights.
        /// </summary>
        /// <param name="index">Index to indicate which type of coin has been inserted.</param>
        public void CoinInserted(int index)
        {
            moneyInserted += coins[index].AddCoin();
            amountDisplay.DisplayAmount(moneyInserted);
            foreach (Drink d in drinks) { d.TurnOnPurchaseLight(moneyInserted); }
        }

        /// <summary>
        /// Carries out process for when client has purchased a can, determining whether or not the transaction can take place and if so, completing the transaction.
        /// </summary>
        /// <param name="index">Keeps track of which type of drink has been purchased.</param>
        public void PurchasedCan(int index)
        {
            int change; //used to keep track of how much money the machine needs to return
            if (!drinks[index].PurchaseOn()) { return; }

            change = drinks[index].CalculateChangeValue(moneyInserted);

            if (!CanMakeChange(change))
            {
                noChange.TurnOn3Sec();
                return;
            }

            drinks[index].DispenseCan();
            MakeChange();
            ClearValues();
        }

        /// <summary>
        /// Carries out process for if the client wants change back.
        /// </summary>
        public void ReturnChangePressed()
        {
            if (moneyInserted <= 0) { return; }
            CanMakeChange(moneyInserted); //Wil always be true, but this method simplfies the process for purchasing a can
            MakeChange();
            ClearValues();
        }

        /// <summary>
        /// Determines whether or not the machine can make cahnge for a given values
        /// </summary>
        /// <param name="change">Value that needs to be converted into change.</param>
        /// <returns>True if the machine can make change, false otherwise</returns>
        public bool CanMakeChange(int change)
        {
            for (int i = VendingMachine.NUMCOINTYPES - 1; i >= 0; i--) { change = coins[i].DetermineCoinsToDispense(change); }
            if (change == 0) { return true; }
            return false;
        }

        /// <summary>
        /// Dispenses all of the change that was determined from CanMakeChange, separate method because it is used both by CanMakeChange and PurchasedCan
        /// </summary>
        public void MakeChange()
        {
            for (int i = VendingMachine.NUMCOINTYPES - 1; i >= 0; i--) { coins[i].DispenseChange(); }
        }

        /// <summary>
        /// Used by both this class and VendingMachine to reset all values for the MachineControl
        /// </summary>
        public void ClearValues()
        {
            moneyInserted = 0;
            amountDisplay.DisplayAmount(moneyInserted);
            foreach (Drink d in drinks) { d.ClearLights(); }
        }
    }
}
