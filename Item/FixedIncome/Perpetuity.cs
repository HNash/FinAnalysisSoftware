using System;
using System.Collections;

namespace OAP_CS

{
    [Serializable]
    class Perpetuity : Item
    {
        //----------------------------------------FIELDS----------------------------------------
        double payment = 0.0, interestRate;
        double spotPrice = 0.0;
        double macDur = 0.0, modDur = 0.0;
        new public static string[] parameterNames = { "Name:", "Payment:", "Ann. Interest Rate (%):" };

        //----------------------------------------CTOR + FACTORY----------------------------------------
        Perpetuity(ArrayList inputs) : base(inputs)
        {
            payment = (double)inputs[1];
            interestRate = (double)inputs[2] / 100.0;
            process();
        }
        public static Item factory(ArrayList inputs)
        {
            return new Perpetuity(inputs);
        }

        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        void calculateSpot()
        {
            spotPrice = payment / interestRate;
        }

        //----------------------------------------DURATION CALCULATIONS----------------------------------------
        void calculateMacDur()
        {
            macDur = (1 + interestRate) / interestRate;
        }

        void calculateModDur()
        {
            modDur = 1 / interestRate;
        }

        //----------------------------------------PROCESS----------------------------------------
        public override void process()
        {
            calculateSpot();
            calculateMacDur();
            calculateModDur();
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
        public override ArrayList getResults()
        {
            ArrayList results = new ArrayList();

            results.Add("Price: " + spotPrice);
            results.Add("Macaulay Duration: " + macDur);
            results.Add("Modified Duration: " + modDur);
            return results;
        }
    }
}