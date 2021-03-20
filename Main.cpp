#include "Item/Item.hpp"
#include "Item/FixedIncome/Bond.hpp"
#include "Item/FixedIncome/CallableBond.hpp"
#include "Item/FixedIncome/ZeroCouponBond.hpp"
#include "Item/FixedIncome/Perpetuity.hpp"
#include <iostream>

int main()
{
    Bond *b = new Bond("non-callable", 100, 10, 2, 10, 1.5);
    std::cout << "Vanilla Bond - \n";
    std::cout << "\tPrice: " << b->getSpot() << "\n";
    std::cout << "\tMacaulay Duration: " << b->getMacDur() << "\n";
    std::cout << "\tModified Duration: " << b->getModDur() << "\n\n";

    CallableBond *cb = new CallableBond("callable", 100, 10, 2, 10, 1.5, 105, 1, 8);
    std::cout << "Callable Bond - \n";
    std::cout << "\tPrice: " << cb->getSpot() << "\n";
    std::cout << "\tEffective Duration: " << cb->getEffectiveDur() << "\n\n";

    ZeroCouponBond *zcb = new ZeroCouponBond("zero coupon", 100, 10, 1.5);
    std::cout << "Zero Coupon Bond - \n";
    std::cout << "\tPrice: " << zcb->getSpot() << "\n";
    std::cout << "\tMacaulay Duration: " << zcb->getMacDur() << "\n";
    std::cout << "\tModified Duration: " << zcb->getModDur() << "\n\n";

    Perpetuity *p = new Perpetuity("perpetuity", 100, 1.5);
    std::cout << "Perpetuity - \n";
    std::cout << "\tPrice: " << p->getSpot() << "\n";
    std::cout << "\tMacaulay Duration: " << p->getMacDur() << "\n";
    std::cout << "\tModified Duration: " << p->getModDur() << "\n";

    std::cout << std::endl;
}