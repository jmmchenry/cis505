//////////////////////////////////////////////////////////////////////
//      Vending Machine (Form1.cs)                                  //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010       //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VendingMachine
{
    /// <summary>
    /// Interacts with GUI to keep track of vending machine operations.
    /// </summary>
    public partial class VendingMachine : Form
    {
        // Static Constants
        public const int NUMCANTYPES = 4;
        public const int NUMCOINTYPES = 4;
        public static readonly int[] NUMCANS = { 4, 4, 4, 4 };
        public static readonly int[] CANPRICES = { 120, 170, 130, 110 };
        public static readonly string[] CANNAMES = { "Coca-Cola", "Pepsi", "Dr. Pepper", "Sprite" };

        public static readonly int[] COINVALUES = { 10, 50, 100, 500 };
        public static readonly int[] NUMCOINS = { 15, 10, 5, 2 };
        // 10Yen, 50Yen, 100Yen, 500Yen

        // Boundary Objects
        private AmountDisplay amountDisplay;
        private DebugDisplay displayPrice0, displayPrice1, displayPrice2, displayPrice3;
        private DebugDisplay displayNum10Yen, displayNum50Yen, displayNum100Yen, displayNum500Yen;
        private DebugDisplay displayName0, displayName1, displayName2, displayName3;
        private DebugDisplay displayNumCans0, displayNumCans1, displayNumCans2, displayNumCans3;
        private Light soldOutLight0, soldOutLight1, soldOutLight2, soldOutLight3;
        private TimerLight noChangeLight;
        private Light purchasableLight0, purchasableLight1, purchasableLight2, purchasableLight3;
        private CoinDispenser coinDispenser10Yen, coinDispenser50Yen, coinDispenser100Yen, coinDispenser500Yen;
        private CanDispenser canDispenser0, canDispenser1, canDispenser2, canDispenser3;
        private CoinInserter coinInserter10Yen, coinInserter50Yen, coinInserter100Yen, coinInserter500Yen;
        private PurchaseButton purchaseButton0, purchaseButton1, purchaseButton2, purchaseButton3;
        private CoinReturnButton coinReturnButton;

        // Declare fields for your entity and control objects
        private List<Drink> drinks;
        private Coin[] coins;
        private MachineControl control;


        public VendingMachine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes objects and sets up connections among all of them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            amountDisplay = new AmountDisplay(txtAmount);

            displayNum10Yen = new DebugDisplay(txtNum10Yen);
            displayNum50Yen = new DebugDisplay(txtNum50Yen);
            displayNum100Yen = new DebugDisplay(txtNum100Yen);
            displayNum500Yen = new DebugDisplay(txtNum500Yen);
            displayPrice0 = new DebugDisplay(txtPrice0);
            displayPrice1 = new DebugDisplay(txtPrice1);
            displayPrice2 = new DebugDisplay(txtPrice2);
            displayPrice3 = new DebugDisplay(txtPrice3);
            displayName0 = new DebugDisplay(txtName0);
            displayName1 = new DebugDisplay(txtName1);
            displayName2 = new DebugDisplay(txtName2);
            displayName3 = new DebugDisplay(txtName3);
            displayNumCans0 = new DebugDisplay(txtNumCan0);
            displayNumCans1 = new DebugDisplay(txtNumCan1);
            displayNumCans2 = new DebugDisplay(txtNumCan2);
            displayNumCans3 = new DebugDisplay(txtNumCan3);

            soldOutLight0 = new Light(pbxSOLight0, Color.Orange);
            soldOutLight1 = new Light(pbxSOLight1, Color.Orange);
            soldOutLight2 = new Light(pbxSOLight2, Color.Orange);
            soldOutLight3 = new Light(pbxSOLight3, Color.Orange);

            noChangeLight = new TimerLight(pbxNoChange, Color.Red, timer1);

            purchasableLight0 = new Light(pbxPurLight0, Color.Aqua);
            purchasableLight1 = new Light(pbxPurLight1, Color.Aqua);
            purchasableLight2 = new Light(pbxPurLight2, Color.Aqua);
            purchasableLight3 = new Light(pbxPurLight3, Color.Aqua);

            coinDispenser10Yen = new CoinDispenser(txtChange10Yen);
            coinDispenser50Yen = new CoinDispenser(txtChange50Yen);
            coinDispenser100Yen = new CoinDispenser(txtChange100Yen);
            coinDispenser500Yen = new CoinDispenser(txtChange500Yen);

            // All candispensers share the same output textbox for simulation
            canDispenser0 = new CanDispenser(txtCanDispenser, CANNAMES[0]);
            canDispenser1 = new CanDispenser(txtCanDispenser, CANNAMES[1]);
            canDispenser2 = new CanDispenser(txtCanDispenser, CANNAMES[2]);
            canDispenser3 = new CanDispenser(txtCanDispenser, CANNAMES[3]);

            // Instantiate your entity and control objects
            // Connect these objects
            drinks = new List<Drink>();
            drinks.Add(new Drink(NUMCANS[0], CANPRICES[0], soldOutLight0, purchasableLight0, canDispenser0));
            drinks.Add(new Drink(NUMCANS[1], CANPRICES[1], soldOutLight1, purchasableLight1, canDispenser1));
            drinks.Add(new Drink(NUMCANS[2], CANPRICES[2], soldOutLight2, purchasableLight2, canDispenser2));
            drinks.Add(new Drink(NUMCANS[3], CANPRICES[3], soldOutLight3, purchasableLight3, canDispenser3));

            coins = new Coin[NUMCOINTYPES];
            coins[0] = new Coin(COINVALUES[0], NUMCOINS[0], coinDispenser10Yen);
            coins[1] = new Coin(COINVALUES[1], NUMCOINS[1], coinDispenser50Yen);
            coins[2] = new Coin(COINVALUES[2], NUMCOINS[2], coinDispenser100Yen);
            coins[3] = new Coin(COINVALUES[3], NUMCOINS[3], coinDispenser500Yen);
            control = new MachineControl(drinks, coins, amountDisplay, noChangeLight);
            // You must replace the following default constructors with 
            // constructors with arguments (non-default constructors)
            // to pass (set) the first object that ButtonPressed() will
            // visit
            purchaseButton0 = new PurchaseButton(control, 0);
            purchaseButton1 = new PurchaseButton(control, 1);
            purchaseButton2 = new PurchaseButton(control, 2);
            purchaseButton3 = new PurchaseButton(control, 3);

            // You must replace the following default constructors with
            // constructors that take armuments to pass the first object that
            // the CoinInserted() will call
            coinInserter10Yen = new CoinInserter(control, 0);
            coinInserter50Yen = new CoinInserter(control, 1);
            coinInserter100Yen = new CoinInserter(control, 2);
            coinInserter500Yen = new CoinInserter(control, 3);

            coinReturnButton = new CoinReturnButton(control);

            // Display debug information
            displayCanPricesAndNames();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process when 10Yen coin is inserted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoinInserter10Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter10Yen.CoinInserted();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process when 50Yen coin is inserted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoinInserter50Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter50Yen.CoinInserted();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process when 100Yen coin is inserted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoinInserter100Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter100Yen.CoinInserted();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process when 500Yen coin is inserted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoinInserter500Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter500Yen.CoinInserted();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process for buying Drink #1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurButtn0_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton0.ButtonPressed();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process for buying drnk #2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurButton1_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton1.ButtonPressed();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process for buying drink #3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurButton2_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton2.ButtonPressed();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process for buying drink #4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurButton3_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton3.ButtonPressed();
            updateDebugDisplays();
        }

        /// <summary>
        /// Starts process for returning coins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoinReturn_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinReturnButton.ButtonPressed();
            updateDebugDisplays();
        }

        /// <summary>
        /// Clears debug values for coin dispensers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangePickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            coinDispenser10Yen.Clear();
            coinDispenser50Yen.Clear();
            coinDispenser100Yen.Clear();
            coinDispenser500Yen.Clear();
        }

        /// <summary>
        /// Clears debug values for can dispensers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanPickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            canDispenser0.Clear(); // since all canDispenser objects accesses the
                                   // same textbox object
        }

        /// <summary>
        /// Resets all values in the machine to how they are at initialization.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach (Drink d in drinks) { d.ResetDrinks(); }
            for (int i = 0; i < NUMCOINTYPES; i++) { coins[i].ResetCoins(i); }
            control.ClearValues();
            updateDebugDisplays();
        }

        /// <summary>
        /// Prints names and prices of drinks to respective textboxes. 
        /// </summary>
        private void displayCanPricesAndNames()
        {
            displayPrice0.Display("\\" + CANPRICES[0]);
            displayPrice1.Display("\\" + CANPRICES[1]);
            displayPrice2.Display("\\" + CANPRICES[2]);
            displayPrice3.Display("\\" + CANPRICES[3]);
            displayName0.Display(CANNAMES[0]);
            displayName1.Display(CANNAMES[1]);
            displayName2.Display(CANNAMES[2]);
            displayName3.Display(CANNAMES[3]);
        }

        /// <summary>
        /// Displays how many coins and cans of each type are available. 
        /// </summary>
        private void updateDebugDisplays()
        {
            // You need to change XXX to appropriate "object.property"
            displayNum10Yen.Display(coins[0].Count);
            displayNum50Yen.Display(coins[1].Count);
            displayNum100Yen.Display(coins[2].Count);
            displayNum500Yen.Display(coins[3].Count);
            displayNumCans0.Display(drinks[0].Count);
            displayNumCans1.Display(drinks[1].Count);
            displayNumCans2.Display(drinks[2].Count);
            displayNumCans3.Display(drinks[3].Count);
        }
    }
}