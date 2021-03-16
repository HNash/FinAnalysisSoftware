#include "Item/Item.hpp"
#include "Item/FixedIncome/Bond.hpp"
#include <iostream>

int main()
{
    Bond *b = new Bond("New Bond", 1000, 10, 4, 10, 1.5);
    b->calculatePrice();
    b->displayPrice();
    std::cout << "done" << std::endl;
}