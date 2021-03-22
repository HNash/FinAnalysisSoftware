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