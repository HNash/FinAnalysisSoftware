#include "Item.hpp"
#include <cmath>

// Normal CDF function for use by derived classes
double Item::normalCDF(double value)
{
   return 0.5 * erfc(-value * 0.707106781186547524401);
}

//----------------------------------------GETTERS----------------------------------------
string Item::getName()
{
    return this->name;
}