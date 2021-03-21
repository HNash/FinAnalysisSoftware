#include "ConvertibleBond.hpp"
#include <cmath>
#include <string>
using std::string;

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void ConvertibleBond::calculateSpot()
{
    // Price of convertible = Price of vanilla + Price of embedded option
    Bond::calculateSpot(); // DO NOT REMOVE. Required in effective duration calculation
    embeddedOption = new AmericanOption("embedded", stockPrice, conversionP, timeToMaturity/couponFreq, interestRate*100*couponFreq, vol, 1000, true);
    spotPrice += embeddedOption->getSpot();
    delete embeddedOption;
}

void ConvertibleBond::calculateEffectiveDur()
{
    if(spotPrice == 0.0)
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
double ConvertibleBond::getSpot()
{
    return spotPrice;
}

double ConvertibleBond::getEffectiveDur()
{
    return effectiveDur;
}
//----------------------------------------PROCESS----------------------------------------
void ConvertibleBond::process()
{
    calculateSpot();
    calculateEffectiveDur();
}

