#ifndef CallableBond_hpp
#define CallableBond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
#include "Bond.hpp"
using std::string;

// Derived class from Bond
// Callable bond price = vanilla bond price - call option price
// Inherited Bond price calculation method will be used to get the vanilla bond price element of the above equation
class CallableBond : public Bond
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        CallableBond(){};
        CallableBond(const string &n, double f, double cR, double cF, double t, double r, double callP, double fVol, double tToCall) : 
            Bond(n, f, cR, cF, t, r),
            callPrice(callP), 
            forwardVol(fVol), 
            timeToCall(tToCall * cF){process();};

        virtual ~CallableBond() {};

        //----------------------------------------GETTERS----------------------------------------
        double getEffectiveDur();

    protected:
        //----------------------------------------FIELDS----------------------------------------
        double callPrice, timeToCall, forwardVol;
        double effectiveDur;
        //----------------------------------------METHODS----------------------------------------
        virtual void process();

        virtual void calculateSpot(); // This will use both Bond::calculateSpot() and Bond::calculateForward in its implementation
        
        // The following methods are inherited from Bond but are not used, so the implementation is blank for safety
        virtual void calculateForward(double d){};
        virtual void calculateMacDur(){};
        virtual void calculateModDur(){};

        // Instead of Macaulay or Modified duration, the Effective Duration is calculated for callable bonds
        void calculateEffectiveDur();

};

#endif