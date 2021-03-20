#ifndef Perpetuity_hpp
#define Perpetuity_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
using std::string;

// Derived class from Bond
class Perpetuity : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        Perpetuity(){};
        Perpetuity(const string &n, double pmt, double r) : Item(n), payment(pmt), interestRate(r){};
        virtual ~Perpetuity() {};

        double getSpot();
        double getMacDur();
        double getModDur();

        void process();
    
    protected:
        double payment = 0.0, interestRate;
        double spotPrice = 0.0;
        double macDur = 0.0, modDur = 0.0;
        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot();
        void calculateMacDur();
        void calculateModDur();
};

#endif