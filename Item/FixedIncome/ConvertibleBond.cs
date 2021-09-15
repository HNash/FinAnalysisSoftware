using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    class ConvertibleBond : Bond
    {
        //----------------------------------------FIELDS----------------------------------------
        protected double stockPrice, conversionP, vol;
        protected double effectiveDur = 0.0;

        //----------------------------------------CTOR + FACTORY----------------------------------------
        // Input array: name, face, coupon rate, coupon frequency, time to maturity, interest rate, stock price, conversion price, volatility
        public ConvertibleBond(ArrayList inputs) : base(inputs)
        {
            parameterNames = new string[9];
            parameterNames[0] = (new string("Name:"));
            parameterNames[1] = (new string("Face Value: "));
            parameterNames[2] = (new string("Coupon Rate (%): "));
            parameterNames[3] = (new string("Coupon Frequency: "));
            parameterNames[4] = (new string("Time to Maturity (Yrs): "));
            parameterNames[5] = (new string("Ann. Interest Rate (%): "));
            parameterNames[6] = (new string("Stock Price: "));
            parameterNames[7] = (new string("Conversion Price: "));
            parameterNames[8] = (new string("Ann. Price Vol. (%): "));
            stockPrice = dConvert((string)inputs[6]);
            conversionP = dConvert((string)inputs[7]);
            vol = dConvert((string)inputs[8]);
            process();
        }

        new public static Item factory(ArrayList inputs)
        {
            return new ConvertibleBond(inputs);
        }
        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        new void calculateSpot()
        {
            // Price of convertible = Price of vanilla + Price of embedded option
            spotPrice = (face * couponRate) * ((1 - (Math.Pow((1 + interestRate), (-1 * timeToMaturity)))) / interestRate) + (face / Math.Pow((1 + interestRate), (timeToMaturity)));
            AmericanOption embeddedOption = new AmericanOption("embedded", stockPrice, conversionP, timeToMaturity / couponFreq, interestRate * 100 * couponFreq, vol, 1000, true);
            spotPrice += embeddedOption.getSpot();
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
            ArrayList results = new ArrayList();
            results.Add("Price: " + spotPrice);
            results.Add("Effective Duration: " + effectiveDur);
            return results;
        }
        //----------------------------------------PROCESS----------------------------------------
        public override void process()
        {
            calculateSpot();
            calculateEffectiveDur();
        }
    }
}