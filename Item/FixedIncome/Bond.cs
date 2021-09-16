using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    class Bond : Item
    {
        //----------------------------------------FIELDS----------------------------------------
        protected double face, couponRate, couponFreq, timeToMaturity, interestRate;
        protected double spotPrice = 0.0, forwardPrice = 0.0;
        protected double macDur = 0.0, modDur = 0.0;
        new public static string[] parameterNames = { "Name:", "Face Value: ", "Coupon Rate (%): ",
                                                        "Coupon Frequency: ", "Time to Maturity (Yrs): ", "Ann. Interest Rate (%): "};



        //----------------------------------------CTOR + FACTORY----------------------------------------

        static ArrayList blank = new ArrayList() { "" };
        public Bond() : base(blank) { }
        public Bond(ArrayList inputs) : base(inputs)
        {
            face = (double)inputs[1];
            couponFreq = (double)inputs[3];
            couponRate = (double)inputs[2] / (100.0 * couponFreq);
            timeToMaturity = (double)inputs[4] * couponFreq;
            interestRate = (double)inputs[5] / (100.0 * couponFreq);
            process();
        }

        // Used in Zero Coupon Bond constructor
        public Bond(ArrayList inputs, double f, double cR, double cF, double t, double r) : base(inputs)
        {
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
            ArrayList vec = new ArrayList();

            vec.Add("Price: " + spotPrice.ToString());
            vec.Add("Macaulay Duration: " + macDur.ToString());
            vec.Add("Modified Duration: " + modDur.ToString());
            return vec;
        }
    }
}