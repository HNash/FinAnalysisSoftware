#include "Bond.hpp"
#include <cmath>
#include <iostream>

string Bond::BOND_PARAM_NAMES[6] = { "Name: ", "Face Value: ", "Annual Coupon Rate (%): ", "Coupon Frequency (Per Yr): ",
            "Time to Maturity (Yrs): ", "Annual Interest Rate (%): " };

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
vector<string> Bond::getResults()
{
    vector<string> vec;
    vec.push_back(string("Price: ") + std::to_string(spotPrice));
    vec.push_back(string("Macaulay Duration: ") + std::to_string(macDur));
    vec.push_back(string("Modified Duration: ") + std::to_string(modDur));
    return vec;
}