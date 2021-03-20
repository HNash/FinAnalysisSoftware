#include "ZeroCouponBond.hpp"
#include <cmath>
#include <iostream>

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void ZeroCouponBond::calculateSpot()
{
    // Simple discounting of face value payment
    spotPrice = face / (pow((1 + interestRate), timeToMaturity));
}

//----------------------------------------DURATION CALCULATIONS----------------------------------------
void ZeroCouponBond::calculateMacDur()
{
    macDur = timeToMaturity;
}
// Modified duration calculation is the same as the one for vanilla bond, no need to implement it here

//----------------------------------------PROCESS----------------------------------------
void ZeroCouponBond::process()
{
    calculateSpot();
    calculateMacDur();
    calculateModDur();
}