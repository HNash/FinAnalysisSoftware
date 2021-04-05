#ifndef Item_hpp
#define Item_hpp

#include <string>
#include <vector>

using std::vector;
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
        virtual vector<string> getResults() { vector<string> vec; return vec; };
        
    protected:
        //----------------------------------------FIELDS----------------------------------------
        string name;
};


#endif