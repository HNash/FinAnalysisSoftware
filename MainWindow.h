#pragma once

#include "wx/wx.h"
#include <string>
#include <vector>
#include "Item/Item.hpp"
#include "Item/FixedIncome/Bond.hpp"
#include "Item/FixedIncome/ZeroCouponBond.hpp"
#include "Item/FixedIncome/ConvertibleBond.hpp"
#include "Item/FixedIncome/CallableBond.hpp"
#include "Item/FixedIncome/Perpetuity.hpp"
#include "Item/Option/AmericanOption.hpp"
#include "Item/Option/EuropeanOption.hpp"
#include "Item/Option/BondOption.hpp"
#include "PortfolioWindow.h"

using std::string;
using std::vector;

class MainWindow : public wxFrame
{
	public:
		//-------------------------CTORS & DTORS-------------------------
		MainWindow();
		~MainWindow() {};

	private:
		wxDECLARE_EVENT_TABLE();

		//-------------------------GUI ELEMENTS-------------------------
		int mainWidth, mainHeight;

		wxStaticText* assetCreateLabel = nullptr;
		wxComboBox* assetMenu = nullptr; // Combo box for the user to select the asset they want to create

		int formX, formY; // Position of data entry form for asset creation
		wxStaticText* labels[10] = { nullptr }; // Labels for the the text boxes where the user enters the asset parameters
		wxTextCtrl* textCtrls[10] = { nullptr }; // The text boxes where the params are entered
		wxCheckBox* putCheck = nullptr; // Check box to determine whether options are puts or not
		wxButton* computeBtn = nullptr; // Button to compute asset valuation/create asset object
		wxButton* saveBtn = nullptr; // Button to save asset in portfolio. Opens portfolio window.
		wxStaticText* resultsLabel = nullptr;
		wxListBox* displayBox = nullptr; // To display valuation results

		// These fill and empty labels and textCtrls, thereby showing the relevant components on-screen
		void setupForm(const int, string*);
		void destroyForm(); // Helper function for setupForm()

		//-------------------------FUNDAMENTALS/CONNECTION TO BACKEND-------------------------
		Item* (*factoryPtr)(vector<string>); // Pointer used to access factory function of selected asset
		Item* asset = nullptr; // The asset created after processing input parameters
		int currentParamCount = 0;

		//-------------------------EVENT HANDLERS FOR GUI ELEMENTS-------------------------
		void OnAssetSelection(wxCommandEvent&); // When asset is selected from drop-down menu
		void OnSaveClick(wxCommandEvent&); // When the save button is clicked
		void OnComputeClick(wxCommandEvent&); // When the compute button is clicked

		//-------------------------CHILD PORTFOLIO WINDOW & RELEVANT-------------------------
		PortfolioWindow* portfolioWindow = nullptr; // Actual portfolio window

		wxStaticText* portfolioSecLabel = nullptr;
		wxButton* portfolioCreate = nullptr; // Create Portfolio button
		wxButton* portfolioView = nullptr; // View Portfolios button
		wxListBox* portfolioBox = nullptr; // Listbox to display portfolios when chosen

		vector<string> paramNames; // Parameter names of current asset, to be passed to portfolio window for saving
};