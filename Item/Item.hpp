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
        Item(vector<string> inputs) : 
            name(inputs[0]),
            parameters(inputs){};
        virtual ~Item(){};
        
        //----------------------------------------METHODS----------------------------------------
        double normalCDF(double);
        
        virtual void process(){};
        //void save();
        //void remove();

        string getName();
        vector<string> getParams() { return parameters; }; // Returns the params used to create this Item
        virtual vector<string> getResults() { vector<string> vec; return vec; }; // Implemented by derived classes
        
    protected:
        //----------------------------------------FIELDS----------------------------------------
        string name;
        vector<string> parameters; // The input params used to create this Item
};


#endif