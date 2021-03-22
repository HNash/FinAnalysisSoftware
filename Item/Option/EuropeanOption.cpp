#include "EuropeanOption.hpp"
#include <cmath>

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void EuropeanOption::calculateSpot()
{
    // Implementation of Black-Scholes closed form solution for European Options
    double d1 = (log(stockPrice/strike)+(interestRate*timeToExpiry)+(pow(volatility,2)*timeToExpiry*0.5))/(volatility*sqrt(timeToExpiry));
    double d2 = d1 - (volatility*sqrt(timeToExpiry));
    spotPrice = (stockPrice * normalCDF(d1)) - (strike * exp(-1 * interestRate * timeToExpiry) * normalCDF(d2));
}

//----------------------------------------PROCESS----------------------------------------
void EuropeanOption::process()
{
    calculateSpot();
}