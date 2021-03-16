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
        // Constructors and destructor
        Bond(){};
        Bond(const string &n, double f, double cR, double cF, double t, double r) : Item(n), face(f), couponRate(cR/(100*cF)), couponFreq(cF),
            timeToMaturity(t*cF), interestRate(r/(100*cF)){};
            //pow( (1 + (r/100)), (1/cF) ) - 1
        virtual ~Bond() {};

        virtual void calculatePrice();
        void displayPrice();

    protected:
        double face, couponRate, couponFreq, timeToMaturity, interestRate, price;
};

#endif