#include "Option.hpp"
#include <cmath>

double Option::payoff(double sT)
{
    return fmax( (callOrPut * (sT - strike)) , 0);
}

double Option::getSpot()
{
    return spotPrice;
}