#ifndef InterestSwap_hpp
#define InterestSwap_hpp

#include "RatesWindow.hpp"
#include "../Item.hpp"

#include <vector>
#include <string>
#include <stdlib.h> 

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
			rWin->SetWindowStyle(wxSTAY_ON_TOP);
			rWin->Refresh();
			rWin->Show();

			// Problem: The above code opens up a window for the user to enter the reference rates, and
			// the below code needs to wait for the user to click "Submit" in that window, then execute

			vector<string> rawRates = rWin->getResults();
			rWin->Destroy();

			
			for (string &s : rawRates)
			{
				inputs.push_back(std::to_string(stod(s) + stod(inputs[4])));
			}
			
			return new InterestSwap(inputs);
			
			return nullptr;
		}
		static Item* factoryFinish(vector<string> inputs)
		{

		};

		vector<string> rawRates;

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