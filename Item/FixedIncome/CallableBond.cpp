#include "CallableBond.hpp"
#include <cmath>
#include <string>
using std::string;

//----------------------------------------PRICE CALCULATIONS----------------------------------------
// Implementation of Black Model formula for price of embedded call option
void CallableBond::calculateSpot()
{
    Bond *intermediate = new Bond("Intermediate", this->face, this->couponRate, this->couponFreq, this->timeToMaturity, this->interestRate);
    intermediate->calculateForward(this->timeToCall);
    double interSpot = intermediate->getSpot();
    double interForward = intermediate->getForward();

    double d1 = (log(interForward / this->callPrice) + ((pow(this->forwardVol, 2) / 2) * this->timeToCall))  / 
                    (this->forwardVol * pow(this->timeToCall, 0.5));

    double d2 = d1 - (this->forwardVol * pow(this->timeToCall, 0.5));
    

    double embeddedCallSpot = exp(-1 * this->interestRate * this->timeToCall) * ( (interForward * normalCDF(d1)) - (this->callPrice * normalCDF(d2)));


    spotPrice = interSpot - embeddedCallSpot;

    delete intermediate;
}

//----------------------------------------GETTERS----------------------------------------
double CallableBond::getSpot()
{
    return this->spotPrice;
}