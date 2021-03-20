#include "CallableBond.hpp"
#include <cmath>
#include <string>
using std::string;

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void CallableBond::calculateSpot()
{
    // Price of callable = Price of vanilla - Price of embedded option
    Bond::calculateSpot(); // Set spotPrice using vanilla bond formula
    Bond::calculateForward(timeToCall); // Set forwardPrice using vanilla bond formula
    
    // Calculating the different components of the Black model formula for embedded bond option price
    double d1 = (log(forwardPrice/callPrice)+((pow(forwardVol,2)/2)*timeToCall))/(forwardVol*pow(timeToCall,0.5));
    double d2 = d1 - (forwardVol * pow(timeToCall, 0.5));

    // Black model formula for price of bond option
    double embeddedCallSpot = exp(-1 * interestRate * timeToCall) * ( (forwardPrice * normalCDF(d1)) - (callPrice * normalCDF(d2)));

    // Spot price is calculated by subtracting price of embedded option from corresponding vanilla bond price
    spotPrice -= embeddedCallSpot;
}

void CallableBond::calculateEffectiveDur()
{
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
double CallableBond::getEffectiveDur()
{
    return effectiveDur;
}

//----------------------------------------PROCESS----------------------------------------
void CallableBond::process()
{
    calculateSpot();
    calculateEffectiveDur();
}

