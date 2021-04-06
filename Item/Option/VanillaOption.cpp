#include "VanillaOption.hpp"
#include <cmath>

double VanillaOption::payoff(double sT)
{
    return fmax( (callOrPut * (sT - strike)) , 0);
}

double VanillaOption::getSpot()
{
    return spotPrice;
}

vector<string> VanillaOption::getResults()
{
    vector<string> results;
    results.push_back(string("Price: ") + std::to_string(spotPrice));
    return results;
}