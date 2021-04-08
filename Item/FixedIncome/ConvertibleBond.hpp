#ifndef ConvertibleBond_hpp
#define ConvertibleBond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
#include "Bond.hpp"
#include "../Option/AmericanOption.hpp"
using std::string;
using std::stod;

// Derived class from Bond
// Convertible bond price = vanilla bond price + embedded call option price
class ConvertibleBond : public Bond
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        ConvertibleBond(){};
        
        // Input array: name, face, coupon rate, coupon frequency, time to maturity, interest rate, stock price, conversion price, volatility
        ConvertibleBond(vector<string> inputs) :
            Bond(inputs, stod(inputs[1]), stod(inputs[2])/(100*stod(inputs[3])), stod(inputs[3]), stod(inputs[4])* stod(inputs[3]), stod(inputs[5])/(100*stod(inputs[3]))),
            stockPrice (stod(inputs[6])),
            conversionP (stod(inputs[7])),
            vol (stod(inputs[8])){process();};

        static Item* factory(vector<string> inputs)
        {
            return new ConvertibleBond(inputs);
        }

        virtual ~ConvertibleBond(){};

        // GUI setup
        static constexpr int CONVERTIBLE_PARAM_COUNT = 9;
        static string CONVERTIBLE_PARAM_NAMES[CONVERTIBLE_PARAM_COUNT];

        //----------------------------------------GETTERS----------------------------------------
        vector<string> getResults();

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