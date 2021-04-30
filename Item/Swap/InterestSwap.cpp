#include "InterestSwap.hpp"

string InterestSwap::INTSWAP_PARAM_NAMES[5] = { "Name: ", "Payment Frequency: ", "Time to Expiry (Yrs): ", "Fixed Annual Interest Rate (%): ", "Spread (%): " };
void InterestSwap::calculateSpot()
{
	int periods = couponFreq * timeToMaturity;
}
