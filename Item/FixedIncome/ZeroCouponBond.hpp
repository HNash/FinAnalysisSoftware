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
        ZeroCouponBond(const string &n, double f, double t, double r) : Bond(n, f, 0.0, 1.0, t, r){};
        virtual ~ZeroCouponBond() {};

        virtual void process();

    protected:
        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot(); 
        virtual void calculateForward(double d){}; // Inherited from Bond but not used. Implemntation blank for safety
        virtual void calculateMacDur(); // Macaulay duration is calculated differently here but modified duration is kept the same
};

#endif