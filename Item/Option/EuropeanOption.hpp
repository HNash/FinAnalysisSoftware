#ifndef EuropeanOption_hpp
#define EuropeanOption_hpp
#include "VanillaOption.hpp"
#include <cmath>
#include <string>
#include <vector>
using std::string;
using std::vector;
using std::stod;

class EuropeanOption : public VanillaOption
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        EuropeanOption(){};
        EuropeanOption(vector<string> inputs) :
            VanillaOption(inputs, stod(inputs[1]), stod(inputs[2]), stod(inputs[3]), stod(inputs[4]), stod(inputs[5]), stoi(inputs[6]), stoi(inputs[7])) {process();};
        EuropeanOption(const string &n, double s0, double k, double t, double r, double vol, int steps, bool call) :
            VanillaOption(vector<string>{ "" }, s0, k, t, r, vol, steps, call) {process();};
        static Item* factory(vector<string> inputs)
        {
            return new EuropeanOption(inputs);
        }
        virtual ~EuropeanOption(){};

        // GUI setup
        static constexpr int EUROPT_PARAM_COUNT = 8;
        static string EUROPT_PARAM_NAMES[EUROPT_PARAM_COUNT];

    protected:
        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot();
        virtual void process();
};

#endif