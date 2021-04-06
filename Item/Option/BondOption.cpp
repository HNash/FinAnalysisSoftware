#include "BondOption.hpp"
#include <cmath>

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void BondOption::calculateSpot()
{
    double d1 = (log(forwardPrice/callPrice)+((pow(forwardVol,2)/2)*timeToCall))/(forwardVol*pow(timeToCall,0.5));
    double d2 = d1 - (forwardVol * pow(timeToCall, 0.5));
    double c = callOrPut;
    // Black model formula for price of bond option
    spotPrice = exp(-1*interestRate*timeToCall)*c*( (forwardPrice * normalCDF(c*d1)) - (callPrice * normalCDF(c*d2)));
}

//----------------------------------------GETTERS----------------------------------------
vector<string> BondOption::getResults()
{
    vector<string> results;
    results.push_back(string("price") + std::to_string(spotPrice));
    return results;
}

double BondOption::getSpot()
{
    return spotPrice;
}

//----------------------------------------PROCESS----------------------------------------
void BondOption::process()
{
    calculateSpot();
}
