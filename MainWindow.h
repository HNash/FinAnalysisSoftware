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
using std::string;
using std::vector;

class MainWindow : public wxFrame
{
	public:
		MainWindow();
		~MainWindow();

	private:
		wxDECLARE_EVENT_TABLE();

		int currentParamCount = 0;

		wxComboBox* assetMenu = nullptr; // Combo box for the user to select the asset they want to create
		wxListBox* displayBox = nullptr; // To display valuation results
		wxStaticText* labels[20] = { nullptr }; // Labels for the the text boxes where the user enters the asset parameters
		wxTextCtrl* textCtrls[20] = { nullptr }; // The text boxes where the params are entered

		wxButton* btn = nullptr; // Button to compute asset valuation/create asset object
		Item* (*factoryPtr)(vector<string>);

		// These fill and empty labels and textCtrls, thereby showing the relevant components on-screen
		void setupForm(const int, string*);
		void destroyForm();

		void OnComputeClick(wxCommandEvent&);
		void OnAssetSelection(wxCommandEvent&);
};