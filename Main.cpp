#include "Item/Item.hpp"
#include "Item/FixedIncome/Bond.hpp"
#include "Item/FixedIncome/CallableBond.hpp"
#include <iostream>

int main()
{
    CallableBond *cb = new CallableBond ("callable", 1000, 10, 4, 10, 1.5, 1010, 0.1, 8);
    cb->calculateSpot();
    std::cout << "Price of callable: " << cb->getSpot() << std::endl;
}