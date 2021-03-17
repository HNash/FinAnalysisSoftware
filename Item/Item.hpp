#ifndef Item_hpp
#define Item_hpp

#include <string>
using std::string;

class Item
{
    public:
        //----------------------------------------CTORS & DTORS----------------------------------------
        Item(){};
        Item(const string& n) : name(n){};
        virtual ~Item(){};
        
        //----------------------------------------METHODS----------------------------------------
        virtual void calculateSpot(){}; // Inherited and implemented by derivative classes
        virtual double getSpot(){return 0.0d;}; // Inherited and implemented by derivative classes
        
        double normalCDF(double);
        
        //void save();
        //void remove();

        string getName();
        
    protected:
        //----------------------------------------FIELDS----------------------------------------
        string name;
        double spotPrice = 0.0d;
};


#endif