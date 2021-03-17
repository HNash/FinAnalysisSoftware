#include "Bond.hpp"
#include <cmath>
#include <iostream>

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void Bond::calculateSpot()
{
    // Regular simple bond pricing formula. Handling of coupon frequency and relevant interest rates in constructor
    spotPrice = (face * couponRate)*( (1 - ( pow((1 + interestRate), (-1 * timeToMaturity)) )) / interestRate ) + 
        (face / pow((1 + interestRate), (timeToMaturity)) );
}

// This method is an implementation of the proceeds method of calculating bond forward price
// t is the time to forward date before maturity
void Bond::calculateForward(double t)
{
    double couponsValue = 0.0d;
    double couponPmt = this->face * this->couponRate;

    //timeToMaturity is not in years, it is in coupon payment periods
    for(double i = 0; i < t; ++i)
    {
        couponsValue += couponPmt * exp(this->interestRate * (this->timeToMaturity - i));
    }

    if(this->spotPrice == 0.0d)
    {
        this->calculateSpot();
    }

    // Proceeds method formula for forward price
    forwardPrice = (this->spotPrice * exp(this->interestRate * this->timeToMaturity)) - couponsValue;
}

//----------------------------------------GETTERS----------------------------------------

double Bond::getSpot()
{
    return this->spotPrice;
}

double Bond::getForward()
{
    return this->forwardPrice;
}

