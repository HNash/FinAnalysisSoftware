using System;
using System.Collections;

namespace OAP_CS
{
    class ZeroCouponBond : Bond
    {
        ZeroCouponBond(ArrayList inputs) : base(inputs, dConvert((string)inputs[1]), 0.0, 1.0, dConvert((string)inputs[2]), dConvert((string)inputs[3])) 
        { 
            process(); 
        }
        public static Item factory(ArrayList inputs)
        {
            return new ZeroCouponBond(inputs);
        }
        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        void calculateSpot()
        {
            // Simple discounting of face value payment
            spotPrice = face / (Math.Pow((1 + interestRate), timeToMaturity));
        }

        //----------------------------------------DURATION CALCULATIONS----------------------------------------
        void calculateMacDur()
        {
            macDur = timeToMaturity;
        }
        void calculateModDur()
        {
            if (macDur == 0.0)
            {
                calculateMacDur();
            }

            modDur = macDur / (1 + interestRate);
        }

        //----------------------------------------GETTERS----------------------------------------
        ArrayList getResults()
        {
            ArrayList results = new ArrayList();
            results.Add("Price: " + spotPrice);
            results.Add("Macaulay Duration: " + macDur);
            results.Add("Modified Duration: " + modDur);
            return results;
        }

        //----------------------------------------PROCESS----------------------------------------
        void process()
        {
            calculateSpot();
            calculateMacDur();
            calculateModDur();
        }
    }
}