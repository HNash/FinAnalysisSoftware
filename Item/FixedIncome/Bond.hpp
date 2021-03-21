#ifndef Bond_hpp
#define Bond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
using std::string;

// Bond is an Item, inheriting the name field and save/delete methods
class Bond : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        Bond(){};
        // Calls superclass constructor for name. Coupon rate, time to maturity and interest rate are adjusted to account for coupon frequency
        Bond(const string &n, double f, double cR, double cF, double t, double r) : 
            Item(n), 
            face(f), 
            couponRate(cR/(100*cF)),
            timeToMaturity(t*cF), 
            interestRate(r/(100*cF)){process();};

        virtual ~Bond() {};
        
        void calculateForward(double d); // Used in pricing formulae in derived classes

        double getSpot();
        double getForward();
        double getMacDur();
        double getModDur();

    protected:
        //----------------------------------------FIELDS----------------------------------------
        double face, couponRate, timeToMaturity, interestRate;
        double spotPrice = 0.0, forwardPrice = 0.0; 
        double macDur = 0.0, modDur = 0.0;

        //----------------------------------------METHODS----------------------------------------
        virtual void process();

        virtual void calculateSpot(); // calculateSpot() inherited from Item, implemented here
        virtual void calculateMacDur(); // Macaulay duration calculation is redefined in derived classes. Modified duration calculation is not
        void calculateModDur(); // Same implementation for all, since it's a transformation of macDur
};
#endif