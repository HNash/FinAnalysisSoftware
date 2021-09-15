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

        //----------------------------------------CTOR + FACTORY----------------------------------------
        Perpetuity(ArrayList inputs) : base(inputs)
        {
            parameterNames = new string[3];
            parameterNames[0] = (new string("Name:"));
            parameterNames[1] = (new string("Payment:"));
            parameterNames[2] = (new string("Ann. Interest Rate (%):"));

            payment = dConvert((string)inputs[1]);
            interestRate = (dConvert((string)inputs[2])) / 100.0;
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