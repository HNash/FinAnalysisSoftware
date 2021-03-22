#include "Item/Item.hpp"
#include "Item/FixedIncome/Bond.hpp"
#include "Item/FixedIncome/CallableBond.hpp"
#include "Item/FixedIncome/ConvertibleBond.hpp"
#include "Item/FixedIncome/ZeroCouponBond.hpp"
#include "Item/FixedIncome/Perpetuity.hpp"
#include "Item/Option/VanillaOption.hpp"
#include "Item/Option/AmericanOption.hpp"
#include "Item/Option/EuropeanOption.hpp"
#include <iostream>

int main()
{
    Bond *b = new Bond("non-callable", 100, 10, 2, 10, 1.5);
    std::cout << "Vanilla Bond - \n";
    std::cout << "\tPrice: " << b->getSpot() << "\n";
    std::cout << "\tMacaulay Duration: " << b->getMacDur() << "\n";
    std::cout << "\tModified Duration: " << b->getModDur() << "\n\n";

    CallableBond *cb = new CallableBond("callable", 100, 10, 2, 10, 1.5, 120, 20, 5);
    std::cout << "Callable Bond - \n";
    std::cout << "\tPrice: " << cb->getSpot() << "\n";
    std::cout << "\tEffective Duration: " << cb->getEffectiveDur() << "\n\n";

    ConvertibleBond *cvb = new ConvertibleBond("convertible", 100, 10, 2, 10, 1.5, 100, 150, 20);
    std::cout << "Convertible Bond - \n";
    std::cout << "\tPrice: " << cvb->getSpot() << "\n";
    std::cout << "\tEffective Duration: " << cvb->getEffectiveDur() << "\n\n";

    ZeroCouponBond *zcb = new ZeroCouponBond("zero coupon", 100, 10, 1.5);
    std::cout << "Zero Coupon Bond - \n";
    std::cout << "\tPrice: " << zcb->getSpot() << "\n";
    std::cout << "\tMacaulay Duration: " << zcb->getMacDur() << "\n";
    std::cout << "\tModified Duration: " << zcb->getModDur() << "\n\n";

    Perpetuity *p = new Perpetuity("perpetuity", 100, 1.5);
    std::cout << "Perpetuity - \n";
    std::cout << "\tPrice: " << p->getSpot() << "\n";
    std::cout << "\tMacaulay Duration: " << p->getMacDur() << "\n";
    std::cout << "\tModified Duration: " << p->getModDur() << "\n\n";

    AmericanOption *ao = new AmericanOption("A. option", 100, 150, 3, 1.5, 50, 1000, true);
    std::cout << "American Option - \n";
    std::cout << "\tPrice: " << ao->getSpot() << "\n\n";

    EuropeanOption *eo = new EuropeanOption("E. option", 100, 150, 3, 1.5, 50, 1000, true);
    std::cout << "European Option - \n";
    std::cout << "\tPrice: " << eo->getSpot() << "\n\n";    

    std::cout << std::endl;
}