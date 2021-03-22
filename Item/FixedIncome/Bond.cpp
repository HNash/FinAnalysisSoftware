#include "Bond.hpp"
#include <cmath>
#include <iostream>

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void Bond::calculateSpot()
{
    // Regular simple bond pricing formula. Handling of coupon frequency and relevant interest rates in constructor
    spotPrice = (face*couponRate)*((1-(pow((1+interestRate),(-1*timeToMaturity))))/interestRate)+(face/pow((1+interestRate),(timeToMaturity)));
}

// This method is an implementation of the proceeds method of calculating bond forward price
// t is the time to forward date before maturity
void Bond::calculateForward(double t)
{
    t *= couponFreq;
    double couponPmt = (face * couponRate);
    double couponsValue = 0.0;

    for(int i = 0; i < t; ++i)
    {
        couponsValue += couponPmt / (pow((1+interestRate), (i+1)));
    }

    if(spotPrice == 0.0)
    {
        calculateSpot();
    }
    
    forwardPrice = (spotPrice - couponsValue) * exp(interestRate * t);
}

//----------------------------------------DURATION CALCULATIONS----------------------------------------
void Bond::calculateMacDur()
{
    if(spotPrice == 0.0)
    {
        calculateSpot();
    }

    double dur = 0.0;

    for(double t = 0.0; t < timeToMaturity; ++t)
    {
        dur += (t * couponRate * face) / (spotPrice * pow((1 + interestRate), t));
    }

    macDur = dur;
}

void Bond::calculateModDur()
{
    if(macDur == 0.0)
    {
        calculateMacDur();
    }

    modDur = macDur / (1 + interestRate);
}

//----------------------------------------PROCESS----------------------------------------

void Bond::process()
{
    calculateSpot();
    calculateMacDur();
    calculateModDur();
}

//----------------------------------------GETTERS----------------------------------------
double Bond::getSpot()
{
    return spotPrice;
}

double Bond::getForward()
{
    return forwardPrice;
}

double Bond::getMacDur()
{
    return macDur;
}

double Bond::getModDur()
{
    return modDur;
}