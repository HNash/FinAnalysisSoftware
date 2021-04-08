#ifndef CallableBond_hpp
#define CallableBond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
#include "Bond.hpp"
#include "../Option/BondOption.hpp"
using std::string;

// Derived class from Bond
// Callable bond price = vanilla bond price - call option price
// Inherited Bond price calculation method will be used to get the vanilla bond price element of the above equation
class CallableBond : public Bond
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        CallableBond(){};
        CallableBond(vector<string> inputs) :
            Bond(inputs, stod(inputs.at(1)), stod(inputs.at(2)) / (100 * stod(inputs.at(3))), stod(inputs.at(3)), stod(inputs.at(4))* stod(inputs.at(3)), stod(inputs.at(5)) / (100 * stod(inputs.at(3)))),
            callPrice(stod(inputs.at(6))),
            forwardVol(    (stod(inputs.at(7))/100) * sqrt(1/couponFreq)     ),
            timeToCall(stod(inputs.at(8)) * couponFreq){process();};

        static Item* factory(vector<string> inputs)
        {
            return new CallableBond(inputs);
        }

        virtual ~CallableBond() {};

        // GUI setup
        static constexpr int CALLABLE_PARAM_COUNT = 9;
        static string CALLABLE_PARAM_NAMES[CALLABLE_PARAM_COUNT];

        //----------------------------------------GETTERS----------------------------------------
        vector<string> getResults();

        double getEffectiveDur();

    protected:
        //----------------------------------------FIELDS----------------------------------------
        double callPrice, timeToCall, forwardVol;
        double effectiveDur = 0.0;
        BondOption *embeddedOption = nullptr;
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