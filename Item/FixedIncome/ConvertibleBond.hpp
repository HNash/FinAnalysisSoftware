#ifndef ConvertibleBond_hpp
#define ConvertibleBond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
#include "Bond.hpp"
#include "../Option/AmericanOption.hpp"
using std::string;

// Derived class from Bond
// Convertible bond price = vanilla bond price + embedded call option price
class ConvertibleBond : public Bond
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        ConvertibleBond(){};
        ConvertibleBond(const string &n, double f, double cR, double cF, double t, double r, double s0, double conversionP, double vol) :
        Bond(n, f, cR, cF, t, r),
        couponFreq(cF),
        stockPrice(s0),
        conversionP(conversionP),
        vol(vol){process();};
        virtual ~ConvertibleBond(){};

        //----------------------------------------GETTERS----------------------------------------
        double getSpot();
        double getEffectiveDur();
    
    protected:
        //----------------------------------------FIELDS----------------------------------------
        AmericanOption *embeddedOption = nullptr;
        double couponFreq, stockPrice, conversionP, vol;
        double effectiveDur = 0.0;

        //----------------------------------------METHODS----------------------------------------
        virtual void process();
        
        virtual void calculateSpot();

        // The following methods are inherited from Bond but are not used, so the implementation is blank for safety
        virtual void calculateForward(double d){};
        virtual void calculateMacDur(){};
        virtual void calculateModDur(){};

        void calculateEffectiveDur();
};

#endif