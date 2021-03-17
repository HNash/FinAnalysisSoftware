#include "Item.hpp"
#include <cmath>

// Normal CDF function for use by derived classes
double Item::normalCDF(double value)
{
   return 0.5 * erfc(-value * M_SQRT1_2);
}

//----------------------------------------GETTERS----------------------------------------
string Item::getName()
{
    return this->name;
}