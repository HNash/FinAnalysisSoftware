#ifndef AmericanOption_hpp
#define AmericanOption_hpp
#include "VanillaOption.hpp"
#include <cmath>
#include <string>
#include <vector>
using std::string;
using std::vector;
using std::stod;

class AmericanOption : public VanillaOption
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        AmericanOption() {};
        AmericanOption(vector<string> inputs) :
            VanillaOption(inputs, stod(inputs[1]), stod(inputs[2]), stod(inputs[3]), stod(inputs[4]), stod(inputs[5]), stoi(inputs[6]), stoi(inputs[7])){process(); };
        AmericanOption(const string &n, double s0, double k, double t, double r, double vol, int steps, bool call) :
            VanillaOption(vector<string>{""}, s0, k, t, r, vol, steps, call) {process();};
        static Item* factory(vector<string> inputs)
        {
            return new AmericanOption(inputs);
        }

        virtual ~AmericanOption(){};

        // GUI setup
        static constexpr int AMOPT_PARAM_COUNT = 8;
        static string AMOPT_PARAM_NAMES[AMOPT_PARAM_COUNT];

        protected:
            //----------------------------------------METHODS----------------------------------------
            virtual void calculateSpot();
            virtual void process();
};

#endif