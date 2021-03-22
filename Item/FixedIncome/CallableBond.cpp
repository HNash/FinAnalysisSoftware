#include "CallableBond.hpp"
#include <cmath>
#include <string>
#include <iostream>
using std::string;

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void CallableBond::calculateSpot()
{
    // Price of callable = Price of vanilla - Price of embedded option
    Bond::calculateSpot(); // DO NOT REMOVE. Required in effective duration calculation
    Bond::calculateForward(timeToCall); // Set forwardPrice using vanilla bond formula
    embeddedOption = new BondOption("embedded", forwardPrice, callPrice, forwardVol, timeToCall, interestRate, true);
    spotPrice -= embeddedOption->getSpot();
    delete embeddedOption;
}

void CallableBond::calculateEffectiveDur()
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

