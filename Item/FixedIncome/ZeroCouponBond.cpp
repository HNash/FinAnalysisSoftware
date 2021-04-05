#include "ZeroCouponBond.hpp"
#include <cmath>
#include <iostream>

string ZeroCouponBond::ZCB_PARAM_NAMES[4] = { "Name: ", "Face Value: ", "Time to Maturity (Yrs): ", "Annual Interest Rate (%)" };

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

//----------------------------------------GETTERS----------------------------------------
vector<string> ZeroCouponBond::getResults()
{
    vector<string> results;
    results.push_back(string("Price: ") + std::to_string(spotPrice));
    results.push_back(string("Macaulay Duration: ") + std::to_string(macDur));
    results.push_back(string("Modified Duration: ") + std::to_string(modDur));
    return results;
}

//----------------------------------------PROCESS----------------------------------------
void ZeroCouponBond::process()
{
    calculateSpot();
    calculateMacDur();
    calculateModDur();
}