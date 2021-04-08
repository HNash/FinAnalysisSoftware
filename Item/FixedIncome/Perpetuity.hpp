#ifndef Perpetuity_hpp
#define Perpetuity_hpp

#include <cmath>
#include <string>
#include "../Item.hpp"
using std::string;
using std::stod;

// Derived class from Bond
class Perpetuity : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        Perpetuity(){};
        Perpetuity(vector<string> inputs) :
            Item(inputs),
            payment(stod(inputs[1])),
            interestRate(stod(inputs[2]) / 100.0) {process();};
        static Item* factory(vector<string> inputs)
        {
            return new Perpetuity(inputs);
        }
        virtual ~Perpetuity() {};

        // GUI setup
        static constexpr int PERP_PARAM_COUNT = 3;
        static string PERP_PARAM_NAMES[PERP_PARAM_COUNT];

        vector<string> getResults();

        double getSpot();
        double getMacDur();
        double getModDur();
    
    protected:
        double payment = 0.0, interestRate;
        double spotPrice = 0.0;
        double macDur = 0.0, modDur = 0.0;
        //----------------------------------------METHODS----------------------------------------
        void process();
        virtual void calculateSpot();
        void calculateMacDur();
        void calculateModDur();
};

#endif