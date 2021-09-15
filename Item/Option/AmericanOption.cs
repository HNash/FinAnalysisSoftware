using System;
using System.Collections;

namespace OAP_CS
{
    [Serializable]
    class AmericanOption : VanillaOption
    {
        static ArrayList blank = new ArrayList() { "" };

        //----------------------------------------CTOR & FACTORY----------------------------------------
        public AmericanOption(ArrayList inputs) : base(inputs)
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
        public AmericanOption(string n, double s0, double k, double t, double r, double vol, int steps, bool call) : base(blank, s0, k, t, r, vol, steps, call)
        {
            process();
        }

        public static Item factory(ArrayList inputs)
        {
            return new AmericanOption(inputs);
        }

        //----------------------------------------PRICE CALCULATIONS----------------------------------------
        public override void calculateSpot()
        {
            // Parameters of trinomial model
            double delta = timeToExpiry / steps;
            double r = Math.Exp(interestRate * delta);

            // Up, down and flat movement multipliers for stock price
            double u = Math.Exp(volatility * Math.Sqrt(2 * delta));
            double d = 1 / u;

            // Intermediate variables just to clean up code
            double d1 = Math.Exp(interestRate * delta * 0.5);
            double d2 = Math.Exp(volatility * Math.Sqrt(delta * 0.5));

            // Risk-neutral probabilities of up, down and flat movements
            double pu = Math.Pow(((d1 - (1 / d2)) / (d2 - (1 / d2))), 2);
            double pd = Math.Pow((d2 - d1) / (d2 - (1 / d2)), 2);
            double pm = 1 - pu - pd;

            // An array that (at first) represents all the final nodes/leaves of the trinomial tree of stock option payoffs
            double[] payoffs = new double[(2 * steps) + 1];

            // The two for loops and statement in between fill in the array with all the possible last period payoffs of the option
            for (int i = 0; i < steps; ++i)
            {
                double sT = stockPrice * Math.Pow(u, steps - i);
                payoffs[i] = payoff(sT);
            }

            payoffs[steps] = payoff(stockPrice);

            for (int i = steps + 1; i < (2 * steps) + 1; ++i)
            {
                double sT = stockPrice * Math.Pow(d, i - steps);
                payoffs[i] = payoff(sT);
            }

            // This iterates backwards over each time step/level in the tree
            for (int n = (2 * steps) + 1; n >= 3; n -= 2)
            {
                //This iterates down the previous time step, calculating the E(payoff) at each node and saving it
                for (int i = 0; i < n - 2; ++i)
                {
                    double payoffHold = ((pu * payoffs[i]) + (pm * payoffs[i + 1]) + (pd * payoffs[i + 2])) / r;
                    double payoffExercise = payoffs[i + 1];
                    payoffs[i] = fmax(payoffHold, payoffExercise);
                }
            }

            spotPrice = payoffs[0];
        }

        public double getSpot() { return spotPrice; } // DO NOT REMOVE, IMPORTANT FOR CONVERTIBLE BOND

        //----------------------------------------PROCESS----------------------------------------
        public override void process()
        {
            calculateSpot();
        }
    }
}