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
        Bond(const string &n, double f, double cR, double cF, double t, double r) : Item(n), face(f), couponRate(cR/(100*cF)), couponFreq(cF),
            timeToMaturity(t*cF), interestRate(r/(100*cF)){};

        virtual ~Bond() {};

        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot(); // calculateSpot() inherited from Item, implemented here
        virtual double getSpot(); // spotPrice getter, inherited from Item, implemented here
        void calculateForward(double d); // Used in pricing formulae in derived classes
        double getForward();


    protected:
        //----------------------------------------FIELDS----------------------------------------
        double face, couponRate, couponFreq, timeToMaturity, interestRate;
        double forwardPrice = 0.0d; // spotPrice inherited from Item
};

#endif