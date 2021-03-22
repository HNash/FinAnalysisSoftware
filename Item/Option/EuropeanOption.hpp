#ifndef EuropeanOption_hpp
#define EuropeanOption_hpp
#include "Option.hpp"
#include <cmath>
#include <string>
using std::string;

class EuropeanOption : public Option
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        EuropeanOption(){};
        EuropeanOption(const string &n, double s0, double k, double t, double r, double vol, int steps, bool call) :
            Option(n, s0, k, t, r, vol, steps, call){process();};
        virtual ~EuropeanOption(){};

    protected:
        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot();
        virtual void process();
};

#endif