#ifndef BondOption_hpp
#define BondOption_hpp

#include "../Item.hpp"
#include <string>
#include <vector>
using std::string;
using std::vector;
using std::stod;

class BondOption : public Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        BondOption(){};
        BondOption(vector<string> inputs) :
            Item(inputs),
            forwardPrice(stod(inputs[1])),
            callPrice(stod(inputs[2])),
            forwardVol(stod(inputs[3]) / 100.0),
            timeToCall(stod(inputs[4])),
            interestRate(stod(inputs[5]) / 100.0),
            callOrPut(stod(inputs[6]) ? 1.0 : -1.0) {process();};
        BondOption(const string& n, double fP, double cP, double fVol, double tToCall, double r, double call) :
            Item(vector<string>{""}),
            forwardPrice(fP),
            callPrice(cP),
            forwardVol(fVol),
            timeToCall(tToCall),
            interestRate(r / 100),
            callOrPut(call ? 1.0 : -1.0) {process();};

        static Item* factory(vector<string> inputs)
        {
            return new BondOption(inputs);
        }

        virtual ~BondOption() {};

        // GUI setup
        static constexpr int BONDOPT_PARAM_COUNT = 7;
        static string BONDOPT_PARAM_NAMES[BONDOPT_PARAM_COUNT];

        vector<string> getResults();
        double getSpot(); // DO NOT REMOVE, IMPORTANT FOR CALLABLE BOND

    protected:
        double forwardPrice, callPrice, forwardVol, timeToCall, interestRate;
        double callOrPut;
        double spotPrice = 0.0;
        virtual void process();
        virtual void calculateSpot();
};

#endif