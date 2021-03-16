#ifndef Item_hpp
#define Item_hpp

#include <string>
using std::string;

class Item
{
    public:
        // Constructors and destructor
        Item(){};
        Item(const string& n) : name(n){};
        virtual ~Item(){};
            
        virtual void calculatePrice(){};
        //void save();
        //void remove();
        
    protected:
        string name;
};


#endif