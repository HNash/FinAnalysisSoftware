#include "AmericanOption.hpp"
#include <cmath>

string AmericanOption::AMOPT_PARAM_NAMES[AMOPT_PARAM_COUNT] = { "Name: ", "Stock Price: ", "Strike Price: ", "Time to Expiry (Yrs): ", "Annual Interest Rate (%): ",
                                                                        "Annual Volatility (%): ", "Desired Time Steps: ", "Put? " };

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void AmericanOption::calculateSpot()
{
    // Parameters of trinomial model
    const double delta = timeToExpiry / steps;
    const double r = exp(interestRate * delta);

    // Up, down and flat movement multipliers for stock price
    const double u = exp(volatility * sqrt(2 * delta));
    const double d = 1 / u;
    const double m = 1;

    // Intermediate variables just to clean up code
    const double d1 = exp(interestRate * delta * 0.5);
    const double d2 = exp(volatility * sqrt(delta * 0.5));
    
    // Risk-neutral probabilities of up, down and flat movements
    const double pu = pow( ((d1 - (1/d2)) / (d2 - (1/d2))), 2);
    const double pd = pow( (d2 - d1) / (d2 - (1/d2)), 2);
    const double pm = 1 - pu - pd;

    // An array that (at first) represents all the final nodes/leaves of the trinomial tree of stock option payoffs
    double *payoffs = new double[(2 * steps)+ 1];
    
    // The two for loops and statement in between fill in the array with all the possible last period payoffs of the option
    for(int i = 0; i < steps; ++i)
    {
        const double sT = stockPrice * pow(u, steps-i);
        payoffs[i] = payoff(sT);
    }

    payoffs[steps] = payoff(stockPrice);

    for(int i = steps+1; i < (2*steps) + 1; ++i)
    {
        const double sT = stockPrice * pow(d, i-steps);
        payoffs[i] = payoff(sT);
    }

    // This iterates backwards over each time step/level in the tree
    for(int n = (2*steps) + 1; n >= 3; n-=2)
    {
        //This iterates down the previous time step, calculating the E(payoff) at each node and saving it
        for(int i = 0; i < n-2; ++i)
        {
            double payoffHold = ((pu * payoffs[i]) + (pm * payoffs[i+1]) + (pd * payoffs[i+2])) / r;
            double payoffExercise = payoffs[i+1];
            payoffs[i] = fmax(payoffHold, payoffExercise);
        }
    }

    spotPrice = payoffs[0];     
}

//----------------------------------------PROCESS----------------------------------------
void AmericanOption::process()
{
    calculateSpot();
}