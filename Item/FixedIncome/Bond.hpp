#ifndef Bond_hpp
#define Bond_hpp

#include <cmath>
#include <string>
#include <vector>
#include <map>
#include "../Item.hpp"
using std::string;
using std::vector;
using std::map;
using std::stod;

// Bond is an Item, inheriting the name field and save/delete methods
class Bond : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        Bond(){};
        // Calls superclass constructor for name. Coupon rate, time to maturity and interest rate are adjusted to account for coupon frequency
        // Input array: name, face, coupon rate, coupon frequency, time to maturity, interest rate
        Bond(vector<string> inputs) :
            Item(inputs),
            face(stod(inputs.at(1))),
            couponRate(  stod(inputs.at(2)) / (100 * stod(inputs.at(3)))  ),
            couponFreq(stod(inputs.at(3))),
            timeToMaturity(stod(inputs.at(4)) * stod(inputs.at(3))),
            interestRate(stod(inputs.at(5)) / (100 * stod(inputs.at(3)))) {process();};

        Bond(vector<string> inputs, double f, double cR, double cF, double t, double r) : 
            Item(inputs), 
            face(f), 
            couponRate(cR/(100*cF)),
            couponFreq(cF),
            timeToMaturity(t*cF), 
            interestRate(r/(100*cF)){process();};

        static Item* factory(vector<string> inputs)
        {
            return new Bond(inputs);
        }

        virtual ~Bond() {};

        // GUI setup
        static constexpr int BOND_PARAM_COUNT = 6;
        static string BOND_PARAM_NAMES[BOND_PARAM_COUNT];
        
        void calculateForward(double d); // Used in pricing formulae in derived classes

        vector<string> getResults();

        double getSpot();
        double getForward();
        double getMacDur();
        double getModDur();

    protected:
        //----------------------------------------FIELDS----------------------------------------
        double face, couponRate, couponFreq, timeToMaturity, interestRate;
        double spotPrice = 0.0, forwardPrice = 0.0; 
        double macDur = 0.0, modDur = 0.0;

        //----------------------------------------METHODS----------------------------------------
        virtual void process();

        virtual void calculateSpot(); // calculateSpot() inherited from Item, implemented here
        virtual void calculateMacDur(); // Macaulay duration calculation is redefined in derived classes. Modified duration calculation is not
        void calculateModDur(); // Same implementation for all, since it's a transformation of macDur
};

#endif