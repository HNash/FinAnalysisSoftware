//
//  DualNumbers.hpp
//  AAD
//
//  Created by Ahmed Yasser on 3/31/21.
//

#ifndef Dual_hpp
#define Dual_hpp

#include <stdio.h>
#include <iostream>
using std::ostream;

namespace dn {
    struct Dual {
        Dual();
        Dual(double r);
        Dual(double r, double d);
        Dual(const Dual &argument);
        ~Dual() {};

        Dual operator=(const Dual &argument);
        Dual operator+(Dual argument) const;
        Dual operator-(Dual argument) const;
        Dual operator*(Dual argument) const;
        Dual operator++(int notUsed);
        Dual operator--(int notUsed);
        Dual operator++();
        Dual operator--();
    
        friend ostream& operator<<(ostream& os, const Dual& argument);
    
        Dual log(const Dual &dual_number);
        Dual exp(const Dual &dual_number);
        Dual sin(const Dual &dual_number);
        Dual cos(const Dual &dual_number);
        Dual sigmoid(const Dual &dual_number);
    
        double real;
        double dual;
    };
}

#endif /* Dual_hpp */
