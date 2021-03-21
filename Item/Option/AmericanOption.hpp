#ifndef AmericanOption_hpp
#define AmericanOption_hpp
#include "../Item.hpp"
#include <cmath>
#include <string>
using std::string;

class AmericanOption : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        AmericanOption(const string &n, double s0, double k, double t, double r, double vol, int steps, bool call) :
            Item(n),
            stockPrice(s0),
            strike(k),
            timeToExpiry(t),
            interestRate(r/100),
            volatility(vol/100),
            steps(steps),
            callOrPut(call ? 1.0 : -1.0){process();};
        virtual ~AmericanOption(){};

        //----------------------------------------GETTERS----------------------------------------
        double getSpot();

    protected:
        //----------------------------------------FIELDS----------------------------------------
        double stockPrice, strike, timeToExpiry, interestRate, volatility;
        int steps;
        double callOrPut; // Multiplier to determine whether payoff uses ST - k or k - ST
        double spotPrice = 0.0;

        //----------------------------------------METHODS----------------------------------------
        double payoff(double);
        void calculateSpot();

        void process();
};

#endif