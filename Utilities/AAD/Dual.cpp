//
//  Dual.cpp
//  AAD
//
//  Created by Ahmed Yasser on 4/1/21.
//

#include <iostream>
#include <cmath>
#include "Dual.hpp"

//--------------------------------CONSTRUCTORS--------------------------------//

/*
 1st CTOR: Default ctor, instantiates 0 + 0*epsi.
 2nd CTOR: Singular parameterized ctor, instantiates r + 0*epsi.
 3rd CTOR: Double parameterized ctor, instantiates r + d*epsi.
 */

namespace dn {
    Dual::Dual() {
        real = 0;
        dual = 0;
    }

    Dual::Dual(double r) {
        real = r;
        dual = 0;
    }

    Dual::Dual(double r, double d) {
        real = r;
        dual = d;
    }

    Dual::Dual(const Dual &argument) {
        real = argument.real;
        dual = argument.dual;
    }

//------------------------------OPERATOR OVERLOADING------------------------------//

/*
 Here we overload mathematical operators (*, +, ++ (prefix & postix), -, -- (prefix & postix),
 = (copy constructor) and << for iostream's cout.) Similar to the operations of complex numbers,
 but epsilon**2 = 0 in this instance instead.
 */


    Dual Dual::operator+(Dual argument) const {
        Dual temp(real + argument.real,
                  dual + argument.dual);
        return temp;
    }

    Dual Dual::operator-(Dual argument) const {
        Dual temp(real - argument.real,
                  dual - argument.dual);
        return temp;
    }

    Dual Dual::operator*(Dual argument) const {
        Dual temp(real * argument.real,
                  (real * argument.dual) + (dual * argument.real));
        return temp;
    }

    Dual Dual::operator=(const Dual &argument) {
        real = argument.real;
        dual = argument.dual;
        return *this;
    }

    Dual Dual::operator++() {
        Dual temp(real++,
                  dual++);
        return temp;
    }

    Dual Dual::operator++(int notUsed) {
        Dual temp = *this;
        real++;
        dual++;
        return temp;
    }

    Dual Dual::operator--() {
        Dual temp(real--,
                  dual--);
        return temp;
    }

    Dual Dual::operator--(int notUsed) {
        Dual temp = *this;
        real--;
        dual--;
        return temp;
    }

    ostream& operator<<(ostream& os, const Dual& argument) {
        // If the real or dual number is zero, don't display it for aesthetics.
        if (argument.real != 0 && argument.dual != 0)
            os << argument.real << " + " << argument.dual << " * eps" << std::endl;
        else if (argument.real == 0)
            os << argument.dual << " * eps " << std::endl;
        else if (argument.dual == 0)
            os << argument.real << std::endl;
        return os;
    }
}
