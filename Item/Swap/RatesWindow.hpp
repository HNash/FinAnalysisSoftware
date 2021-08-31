#ifndef RatesWindow_hpp
#define RatesWindow_hpp

#include "wx/wx.h"
#include <string>
#include <vector>

using std::string;
using std::vector;


class RatesWindow : public wxFrame
{
	public:
		RatesWindow() {};
		RatesWindow(double, double);

		vector<string> getResults();
		
		bool done = false;

	private:
		wxDECLARE_EVENT_TABLE();
		wxStaticText* prompt = nullptr;
		wxStaticText* labels[50] = { nullptr };
		wxTextCtrl* boxes[50] = { nullptr };
		wxButton* submitBtn = nullptr;
		
		vector<string> results;
		
		void OnSubmitClick(wxCommandEvent&);
};
#endif