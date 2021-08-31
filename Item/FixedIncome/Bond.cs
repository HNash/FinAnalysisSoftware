using System;
using System.Collections;

namespace OAP_CS
{
    class Bond : Item
    {
        //----------------------------------------FIELDS----------------------------------------
        protected double face, couponRate, couponFreq, timeToMaturity, interestRate;
        protected double spotPrice = 0.0, forwardPrice = 0.0;
        protected double macDur = 0.0, modDur = 0.0;

        //----------------------------------------CTOR + FACTORY----------------------------------------

        public Bond(ArrayList inputs) : base(inputs)
        {
            parameterNames = new string[6];
            parameterNames[0] = (new string("Name:"));
            parameterNames[1] = (new string("Face Value: "));
            parameterNames[2] = (new string("Coupon Rate (%): "));
            parameterNames[3] = (new string("Coupon Frequency: "));
            parameterNames[4] = (new string("Time to Maturity (Yrs): "));
            parameterNames[5] = (new string("Ann. Interest Rate (%): "));

            face = dConvert((string)inputs[1]);
            couponRate = (dConvert((string)inputs[2])) / (100 * (dConvert((string)inputs[3])));
            couponFreq = dConvert((string)inputs[3]);
            timeToMaturity = dConvert((string)inputs[4]) * dConvert((string)inputs[3]);
            interestRate = dConvert((string)inputs[5]) / (100 * dConvert((string)inputs[3]));
            process();
        }

        public Bond(ArrayList inputs, double f, double cR, double cF, double t, double r) : base(inputs)
        {
            parameterNames = new string[6];
            parameterNames[0] = (new string("Name:"));
            parameterNames[1] = (new string("Face Value: "));
            parameterNames[2] = (new string("Coupon Rate (%): "));
            parameterNames[3] = (new string("Coupon Frequency: "));
            parameterNames[4] = (new string("Time to Maturity (Yrs): "));
            parameterNames[5] = (new string("Ann. Interest Rate (%): "));

            face = f;
            couponRate = cR/100;
            couponFreq = cF;
            timeToMaturity = t * cF;
            interestRate = r / (100 * cF);
            process();
        }

        public static Item factory(ArrayList inputs)
        {
            return new Bond(inputs);
        }

        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        public void calculateSpot()
        {
            // Regular simple bond pricing formula. Handling of coupon frequency and relevant interest rates in constructor
            spotPrice = (face * couponRate) * ((1 - (Math.Pow((1 + interestRate), (-1 * timeToMaturity)))) / interestRate) + (face / Math.Pow((1 + interestRate), (timeToMaturity)));
        }

        // This method is an implementation of the proceeds method of calculating bond forward price
        // t is the time to forward date before maturity
        public void calculateForward(double t)
        {
            t *= couponFreq;
            double couponPmt = (face * couponRate);
            double couponsValue = 0.0;

            for (int i = 0; i < t; ++i)
            {
                couponsValue += couponPmt / (Math.Pow((1 + interestRate), (i + 1)));
            }

            if (spotPrice == 0.0)
            {
                calculateSpot();
            }

            forwardPrice = (spotPrice - couponsValue) * Math.Exp(interestRate * t);
        }

        //----------------------------------------DURATION CALCULATIONS----------------------------------------
        void calculateMacDur()
        {
            if (spotPrice == 0.0)
            {
                calculateSpot();
            }

            double dur = 0.0;

            for (double t = 0.0; t < timeToMaturity; ++t)
            {
                dur += (t * couponRate * face) / (spotPrice * Math.Pow((1 + interestRate), t));
            }

            macDur = dur;
        }

        void calculateModDur()
        {
            if (macDur == 0.0)
            {
                calculateMacDur();
            }

            modDur = macDur / (1 + interestRate);
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
            ArrayList vec = new ArrayList();
            vec.Add("Price: " + spotPrice.ToString());
            vec.Add("Macaulay Duration: " + macDur.ToString());
            vec.Add("Modified Duration: " + modDur.ToString());
            return vec;
        }
    }
}