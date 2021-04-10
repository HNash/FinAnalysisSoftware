#pragma once

#include "wx/wx.h"
#include <string>
#include <sstream>
#include <vector>

using std::string;
using std::vector;
using std::ifstream;
using std::ofstream;

class PortfolioWindow : public wxFrame
{
	public:
		enum PURPOSE
		{
			CREATE = 0,
			VIEW = 1,
			SAVE = 2
		};

		PortfolioWindow(wxWindow*, PURPOSE); // For viewing/creating portfolios
		PortfolioWindow(wxWindow*, PURPOSE, vector<string>, vector<string>, vector<string>); // For saving assets into a portfolio
		~PortfolioWindow() {};

	private:
		wxDECLARE_EVENT_TABLE();
		
		string get_cwd();
		void empty();

		PURPOSE purpose;

		vector<string> parameterNames;
		vector<string> parameters;
		vector<string> results;

		wxString currentDir = wxString(get_cwd());

		wxStaticText* listLabel = nullptr;
		wxComboBox* portfolioList = nullptr; 
		
		wxStaticText* nameLabel = nullptr;
		wxTextCtrl* newPortfolioName = nullptr;
		wxButton* createBtn = nullptr; 

		wxButton* cancelBtn = nullptr;

		void OnCreateClick(wxCommandEvent&);
		void OnPortfolioSelection(wxCommandEvent&);
		void OnCancelClick(wxCommandEvent&);
};