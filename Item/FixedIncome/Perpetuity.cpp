#include "Perpetuity.hpp"

string Perpetuity::PERP_PARAM_NAMES[3] = { "Name: ", "Annual Payment: ", "Annual Interest Rate (%): " };

//----------------------------------------PRICE CALCULATIONS----------------------------------------
void Perpetuity::calculateSpot()
{
    spotPrice = payment / interestRate;
}

//----------------------------------------DURATION CALCULATIONS----------------------------------------
void Perpetuity::calculateMacDur()
{
    macDur = (1 + interestRate) / interestRate;
}

void Perpetuity::calculateModDur()
{
    modDur = 1 / interestRate;
}

//----------------------------------------PROCESS----------------------------------------
void Perpetuity::process()
{
    calculateSpot();
    calculateMacDur();
    calculateModDur();
}

//----------------------------------------GETTERS----------------------------------------
vector<string> Perpetuity::getResults()
{
    vector<string> results;
    results.push_back(string("Price: ") + std::to_string(spotPrice));
    results.push_back(string("Macaulay Duration: ") + std::to_string(macDur));
    results.push_back(string("Modified Duration: ") + std::to_string(modDur));
    return results;
}