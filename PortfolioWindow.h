#pragma once

#include "wx/wx.h"
#include <string>
#include <vector>

using std::string;
using std::vector;
using std::ifstream;
using std::ofstream;

class PortfolioWindow : public wxFrame
{
	public:
		PortfolioWindow(); // For viewing/creating portfolios
		PortfolioWindow(wxWindow*, vector<string>, vector<string>, vector<string>); // For saving assets into a portfolio
		~PortfolioWindow() {};

	private:
		wxDECLARE_EVENT_TABLE();

		vector<string> parameterNames;
		vector<string> parameters;
		vector<string> results;

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