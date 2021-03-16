#include "Bond.hpp"
#include <cmath>
#include <iostream>


void Bond::calculatePrice()
{
    // Regular simple bond pricing formula. Handling of coupon frequency and relevant interest rates in constructor
    price = (face * couponRate)*( (1 - ( pow((1 + interestRate), (-1 * timeToMaturity)) )) / interestRate ) + 
        (face / pow((1 + interestRate), (timeToMaturity)) );
}

// Test code, will be removed at some point
void Bond::displayPrice()
{
    std::cout << "Price of " << this->name << ": " << this->price << std::endl;
}