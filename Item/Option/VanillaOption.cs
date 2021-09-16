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
        new public static string[] parameterNames = { "Name:", "Stock Price:", "Strike Price:", 
                                                        "Time to Expiry (Yrs):", "Ann. Interest Rate (%):", "Ann. Price Vol. (%):", 
                                                            "Desired Time Steps:", "Put?:"};

        //----------------------------------------CTOR & FACTORY----------------------------------------
        public VanillaOption(ArrayList inputs) : base(inputs)
        {
            stockPrice = (double)inputs[1];
            strike = (double)inputs[2];
            timeToExpiry = (double)inputs[3];
            interestRate =(double)inputs[4] / 100.0;
            volatility = (double)inputs[5] / 100.0;
            steps = (int)((double)inputs[6]);
            callOrPut = (string)inputs[7] == "1.0" ? 1.0 : -1.0;
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

        public override string[] getParameters()
        {
            string[] paramList = new string[parameterNames.Length];
            for (int i = 0; i < parameterNames.Length; ++i)
            {
                paramList[i] = parameterNames[i] + " " + parameters[i];
            }
            return paramList;
        }
        public override ArrayList getResults()
        {
            ArrayList results = new ArrayList();

            results.Add("Price: " + spotPrice);
            return results;
        }

        public override void process() { }
    }
}