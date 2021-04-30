#ifndef InterestSwap_hpp
#define InterestSwap_hpp

#include "RatesWindow.hpp"
#include "../Item.hpp"

#include <vector>
#include <string>
using std::vector;
using std::string;
using std::stod;

class InterestSwap : public Item
{
	public:
		InterestSwap(vector<string> inputs) :
			Item(inputs),
			couponFreq(stod(inputs[1])),
			timeToMaturity(stod(inputs[2])),
			fixedRate(stod(inputs[3])),
			spread(stod(inputs[4])) 
			{
				for (double d = 0; d < couponFreq * timeToMaturity; ++d)
				{
					floatingRates.push_back(stod(inputs[5 + d]) + spread);
				}
				process();
			};

		static Item* factory(vector<string> inputs)
		{
			RatesWindow* rWin = new RatesWindow((stod(inputs[1]) * stod(inputs[2])), (stod(inputs[1])));

			while ((rWin->results[stod(inputs[1]) * stod(inputs[2])]).compare("") == 0)
			{

			}

			vector<string> rawRates = rWin->results;
			rWin->Destroy();

			for (string &s : rawRates)
			{
				inputs.push_back(std::to_string(stod(s) + stod(inputs[4])));
			}

			return new InterestSwap(inputs);
			//Open window for user to enter reference rates for 12*i/couponFreq months ahead, take them and push them into inputs.
			//Then call the constructor on inputs (look at for loop in constructor).
			//PROBLEM: How to get factory() to wait for inputs to be pushed back before calling constructor.
		}

		virtual ~InterestSwap() {};

		// GUI setup
		static constexpr int INTSWAP_PARAM_COUNT = 5;
		static string INTSWAP_PARAM_NAMES[INTSWAP_PARAM_COUNT];

		void calculateSpot();
		void calculateFixedPV();
		void calculateFloatingPV();
		

	private:
		double couponFreq, timeToMaturity, fixedRate;
		vector<double> floatingRates;
		double spread;

		double fixedPV, floatingPV;
};
#endif