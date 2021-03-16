#ifndef CallableBond_hpp
#define CallableBond_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
using std::string;

// Bond is an Item, inheriting the name field and save/delete methods
class CallableBond : public Bond
{
    public:
        // Constructors and destructor
        CallableBond(){};
        CallableBond(const string &n, double f, double cR, double cF, double t, double r) : Bond(n, f, cR, cF, t, r){};
        virtual ~CallableBond() {};

        virtual void calculatePrice();

    protected:
};

#endif