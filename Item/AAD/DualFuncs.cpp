//
//  DualFuncs.cpp
//  AAD
//
//  Created by Ahmed Yasser on 4/1/21.
//

#include <stdio.h>
#include <cmath>
#include "Dual.hpp"

//------------------------------INSTANCE CHECKER------------------------------//

/*
 'Java Style' instance checker, to check if argument passed is
  an instance of the Dual class.
 */

template<typename Base, typename T>
inline bool instanceof(const T*) {
   return std::is_base_of<Base, T>::value;
}

//----------------------------------FUNCTIONS----------------------------------//

/*
  Primative function building blocks, based on
  Taylor expansion of dual numbers:
      f(a + b*e) = f(a) + b * f'(a) * e
  recalling e**2 = 0.
 */

Dual Dual::log(const Dual &dual_number) {
    Dual answer(std::log(dual_number.real),
                dual_number.dual / dual_number.real);
    return answer;
}

Dual Dual::exp(const Dual &dual_number) {
    Dual answer(std::exp(dual_number.real),
                std::exp(dual_number.real) * dual_number.dual);
    return answer;
}

Dual Dual::sin(const Dual &dual_number) {
    Dual answer(std::sin(dual_number.real),
                std::cos(dual_number.real) * dual_number.dual);
    return answer;
}

Dual Dual::cos(const Dual &dual_number) {
    Dual answer(std::cos(dual_number.real),
                std::sin(dual_number.real) * -1 * dual_number.dual);
    return answer;
}

Dual Dual::sigmoid(const Dual &dual_number) {
    const double a = 1 / (std::exp(-1 * dual_number.real) + 1);
    const double b = (dual_number.dual * std::exp(-1 * dual_number.real))
                                       / std::pow((1 + std::exp(-1 * dual_number.real)), 2);
    Dual answer(a, b);
    return answer;
}
