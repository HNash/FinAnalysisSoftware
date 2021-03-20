#include "Perpetuity.hpp"

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
double Perpetuity::getSpot()
{
    return spotPrice;
}

double Perpetuity::getMacDur()
{
    return macDur;
}

double Perpetuity::getModDur()
{
    return modDur;
}