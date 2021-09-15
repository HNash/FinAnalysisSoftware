using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    class ZeroCouponBond : Bond
    {
        //----------------------------------------CTOR + FACTORY----------------------------------------
        ZeroCouponBond(ArrayList inputs) : base(inputs, dConvert((string)inputs[1]), 0.0, 1.0, dConvert((string)inputs[2]), dConvert((string)inputs[3])) 
        { 
            process(); 
        }
        new public static Item factory(ArrayList inputs)
        {
            return new ZeroCouponBond(inputs);
        }
        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        new void calculateSpot()
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
        new public ArrayList getResults()
        {
            ArrayList results = new ArrayList();
            results.Add("Price: " + spotPrice);
            results.Add("Macaulay Duration: " + macDur);
            results.Add("Modified Duration: " + modDur);
            return results;
        }

        //----------------------------------------PROCESS----------------------------------------
        new void process()
        {
            calculateSpot();
            calculateMacDur();
            calculateModDur();
        }
    }
}