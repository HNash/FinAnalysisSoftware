using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    abstract class VanillaOption : Item
    {
        //----------------------------------------FIELDS----------------------------------------
        protected double stockPrice, strike, timeToExpiry, interestRate, volatility;
        protected int steps;
        protected double callOrPut; // Multiplier to determine whether payoff uses ST - k or k - ST
        protected double spotPrice = 0.0;

        //----------------------------------------CTOR & FACTORY----------------------------------------
        public VanillaOption(ArrayList inputs) : base(inputs)
        {
            stockPrice = dConvert((string)inputs[1]);
            strike = dConvert((string)inputs[2]);
            timeToExpiry = dConvert((string)inputs[3]);
            interestRate = dConvert((string)inputs[4]) / 100;
            volatility = dConvert((string)inputs[5]) / 100;
            steps = (int)dConvert((string)inputs[6]);
            callOrPut = dConvert((string)inputs[7]) == 1.0 ? 1.0 : -1.0;
        }
        public VanillaOption(ArrayList n, double s0, double k, double t, double r, double vol, int steps, bool call) : base(n)
        {
            stockPrice = s0;
            strike = k;
            timeToExpiry = t;
            interestRate = r / 100;
            volatility = vol / 100;
            this.steps = steps;
            callOrPut = call ? 1.0 : -1.0;
        }

        // Helper methods used in valuation
        public double payoff(double sT)
        {
            return fmax((callOrPut * (sT - strike)), 0);
        }

        public double fmax(double a, double b)
        {
            if (a >= b)
            {
                return a;
            }
            return b;
        }

        // To be implemented by child classes
        public abstract void calculateSpot();

        public override ArrayList getResults()
        {
            ArrayList results = new ArrayList();
            results.Add("Price: " + spotPrice);
            return results;
        }

        public override void process() { }
    }
}