using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    class CallableBond : Bond
    {
        //----------------------------------------FIELDS----------------------------------------
        protected double callPrice, timeToCall, forwardVol;
        protected double effectiveDur = 0.0;

        //----------------------------------------CTOR + FACTORY----------------------------------------
        public CallableBond(ArrayList inputs) : base(inputs)
        {
            parameterNames = new string[9];
            parameterNames[0] = (new string("Name:"));
            parameterNames[1] = (new string("Face Value: "));
            parameterNames[2] = (new string("Coupon Rate (%): "));
            parameterNames[3] = (new string("Coupon Frequency: "));
            parameterNames[4] = (new string("Time to Maturity (Yrs): "));
            parameterNames[5] = (new string("Ann. Interest Rate (%): "));
            parameterNames[6] = (new string("Call Price: "));
            parameterNames[7] = (new string("Ann. Forward Vol.: "));
            parameterNames[8] = (new string("Time to Call (Yrs): "));

            callPrice = dConvert((string)inputs[6]);
            forwardVol = (dConvert((string)inputs[7])/100) / Math.Sqrt(1/couponFreq);
            timeToCall = dConvert((string)inputs[8]) * couponFreq;
            process();
        }

        new public static Item factory(ArrayList inputs)
        {
            return new CallableBond(inputs);
        }

        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        new void calculateSpot()
        {
            // Price of callable = Price of vanilla - Price of embedded option
            base.calculateSpot(); // DO NOT REMOVE. Required in effective duration calculation
            base.calculateForward(timeToCall); // Set forwardPrice using vanilla bond formula
            BondOption embeddedOption = new BondOption("embedded", forwardPrice, callPrice, forwardVol, timeToCall, interestRate, 1.0);
            spotPrice -= embeddedOption.getSpot();
        }

        void calculateEffectiveDur()
        {
            if (spotPrice == 0.0)
            {
                calculateSpot();
            }

            // Saving the value of interest rate and current price in temporary variables
            double saveR = interestRate;
            double saveP = spotPrice;

            // Calculating price if the interest rate falls to 0.5% below original value
            interestRate -= 0.005;
            calculateSpot();
            double vDown = spotPrice;

            // Calculating price if the interest rate rises to 0.5% above original value
            interestRate += 0.01;
            calculateSpot();
            double vUp = spotPrice;

            // Restoring original value of interest rate and spot price
            interestRate = saveR;
            spotPrice = saveP;

            // Calculating effective duration
            effectiveDur = (vDown - vUp) / (2 * spotPrice * 0.005);
        }

        //----------------------------------------GETTERS----------------------------------------
        public override ArrayList getResults()
        {
            ArrayList vec = new ArrayList();
            vec.Add("Price: " + spotPrice.ToString());
            vec.Add("Macaulay Duration: " + macDur.ToString());
            vec.Add("Modified Duration: " + modDur.ToString());
            return vec;
        }

        //----------------------------------------PROCESS----------------------------------------
        public override void process()
        {
            calculateSpot();
            calculateEffectiveDur();
        }
    }
}