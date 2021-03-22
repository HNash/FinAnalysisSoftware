#ifndef AmericanOption_hpp
#define AmericanOption_hpp
#include "VanillaOption.hpp"
#include <cmath>
#include <string>
using std::string;

class AmericanOption : public VanillaOption
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        AmericanOption(){};
        AmericanOption(const string &n, double s0, double k, double t, double r, double vol, int steps, bool call) :
            VanillaOption(n, s0, k, t, r, vol, steps, call){process();};
        virtual ~AmericanOption(){};

    protected:
        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot();
        virtual void process();
};

#endif