#ifndef ZeroCouponBond_hpp
#define ZeroCouponBond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
#include "Bond.hpp"
using std::string;

// Derived class from Bond
class ZeroCouponBond : public Bond
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        ZeroCouponBond(){};
        // Name, face value, time to maturity, interest rate
        ZeroCouponBond(vector<string> inputs) :
            Bond(inputs, stod(inputs[1]), 0.0, 1.0, stod(inputs[2]), stod(inputs[3])){process();};
        static Item* factory(vector<string> inputs)
        {
            return new ZeroCouponBond(inputs);
        }
        virtual ~ZeroCouponBond() {};

        // GUI setup
        static constexpr int ZCB_PARAM_COUNT = 4;
        static string ZCB_PARAM_NAMES[ZCB_PARAM_COUNT];        

        vector<string> getResults();

    protected:
        //----------------------------------------METHODS----------------------------------------
        virtual void process();
                
        virtual void calculateSpot(); 
        virtual void calculateForward(double d){}; // Inherited from Bond but not used. Implemntation blank for safety
        virtual void calculateMacDur(); // Macaulay duration is calculated differently here but modified duration is kept the same
};

#endif