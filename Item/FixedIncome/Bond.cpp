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
    double couponsValue = 0.0;
    double couponPmt = face * couponRate;

    //timeToMaturity is not in years, it is in coupon payment periods
    for(double i = 0; i < t; ++i)
    {
        couponsValue += couponPmt * exp(interestRate * (timeToMaturity - i));
    }

    if(spotPrice == 0.0)
    {
        calculateSpot();
    }

    // Proceeds method formula for forward price
    forwardPrice = (spotPrice * exp(interestRate * timeToMaturity)) - couponsValue;
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