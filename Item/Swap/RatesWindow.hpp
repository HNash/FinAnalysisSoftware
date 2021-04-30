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

		vector<string> results = { "" };

	private:
		wxDECLARE_EVENT_TABLE();

		wxStaticText* prompt;
		wxStaticText* labels[50];
		wxTextCtrl* boxes[50];
		wxButton* submitBtn;

		void OnSubmitClick(wxCommandEvent&);
};
#endif