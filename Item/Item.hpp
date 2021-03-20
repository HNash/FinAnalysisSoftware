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
        double normalCDF(double);
        
        virtual void process(){};
        //void save();
        //void remove();

        string getName();
        
    protected:
        //----------------------------------------FIELDS----------------------------------------
        string name;
};


#endif