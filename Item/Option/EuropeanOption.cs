using System;
using System.Collections;

namespace OAP_CS
{
    class EuropeanOption : VanillaOption
    {
        static ArrayList blank = new ArrayList() { "" };

        //----------------------------------------CTOR & FACTORY----------------------------------------
        public EuropeanOption(ArrayList inputs) : base(inputs)
        {
            parameterNames = new string[8];
            parameterNames[0] = (new string("Name:"));
            parameterNames[1] = (new string("Stock Price: "));
            parameterNames[2] = (new string("Strike Price: "));
            parameterNames[3] = (new string("Time to Maturity (Yrs): "));
            parameterNames[4] = (new string("Ann. Interest Rate (%): "));
            parameterNames[5] = (new string("Ann. Price Vol. (%): "));
            parameterNames[6] = (new string("Desired Time Steps: "));
            parameterNames[7] = (new string("Put?: "));
            process();
        }
        public EuropeanOption(string n, double s0, double k, double t, double r, double vol, int steps, bool call) : base(blank, s0, k, t, r, vol, steps, call)
        {
            process();
        }
        public static Item factory(ArrayList inputs)
        {
            return new EuropeanOption(inputs);
        }
        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        public override void calculateSpot()
        {
            // Implementation of Black-Scholes closed form solution for European Options
            double d1 = (Math.Log(stockPrice / strike) + (interestRate * timeToExpiry) + (Math.Pow(volatility, 2) * timeToExpiry * 0.5)) / (volatility * Math.Sqrt(timeToExpiry));
            double d2 = d1 - (volatility * Math.Sqrt(timeToExpiry));
            if (callOrPut == 1.0)
            {
                spotPrice = (stockPrice * normalCDF(d1)) - (strike * Math.Exp(-1 * interestRate * timeToExpiry) * normalCDF(d2));
            }
            else
            {
                spotPrice = (strike * Math.Exp(-1 * interestRate * timeToExpiry) * (1 - normalCDF(d2))) - (stockPrice * (1 - normalCDF(d1)));
            }
        }

        //----------------------------------------PROCESS----------------------------------------
        public override void process()
        {
            calculateSpot();
        }
    }
}