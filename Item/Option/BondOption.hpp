#ifndef BondOption_hpp
#define BondOption_hpp
#include "../Item.hpp"

class BondOption : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        BondOption(){};
        BondOption(const string &n, double fP, double cP, double fVol, double tToCall, double r, bool call) :
            Item(n),
            forwardPrice(fP),
            callPrice(cP),
            forwardVol(fVol),
            timeToCall(tToCall),
            interestRate(r),
            callOrPut(call ? 1.0 : -1.0){process();};

        double getSpot();

    protected:
        double forwardPrice, callPrice, forwardVol, timeToCall, interestRate;
        double callOrPut;
        double spotPrice = 0.0;
        virtual void process();
        virtual void calculateSpot();
};

#endif