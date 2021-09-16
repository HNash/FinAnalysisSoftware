using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    class ZeroCouponBond : Bond
    {
        new public static string[] parameterNames = { "Name:", "Face Value:", "Time to Maturity (Yrs)", "Ann. Interest Rate (%):" };
        //----------------------------------------CTOR + FACTORY----------------------------------------
        ZeroCouponBond(ArrayList inputs) : base(inputs, (double)inputs[1], 0.0, 1.0, ((double)inputs[2]), ((double)inputs[3])) 
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
        public override string[] getParameters()
        {
            string[] paramList = new string[parameterNames.Length];
            for (int i = 0; i < parameterNames.Length; ++i)
            {
                paramList[i] = parameterNames[i] + " " + parameters[i];
            }
            return paramList;
        }
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