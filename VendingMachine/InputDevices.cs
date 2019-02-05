//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010, 2011 //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    // For each class, you can (must) add fields and overriding constructors
    /// <summary>
    /// Creates coin inserter which keeps track of what coins have been inserted.
    /// </summary>
    public class CoinInserter
    {
        private MachineControl control;
        private int index;

        public CoinInserter()
        {
        }

        public CoinInserter(MachineControl control, int index)
        {
            this.control = control;
            this.index = index;
        }

        public void CoinInserted()
        {
            control.CoinInserted(index);
        }

    }

    /// <summary>
    /// Creates purchase button which keeps track of which drink has been purchased.
    /// </summary>
    public class PurchaseButton
    {
        MachineControl control;
        int index;

        public PurchaseButton()
        {
        }

        public PurchaseButton(MachineControl control, int index)
        {
            this.control = control;
            this.index = index;
        }

        public void ButtonPressed()
        {
            control.PurchasedCan(index);
        }
    }

    /// <summary>
    /// Creates coin return button which keeps track of whether the customer wants to have their change returned. 
    /// </summary>
    public class CoinReturnButton
    {
        MachineControl control;

        public CoinReturnButton()
        {
        }

        public CoinReturnButton(MachineControl control)
        {
            this.control = control;
        }

        public void ButtonPressed()
        {
            control.ReturnChangePressed();
        }
    }
}
