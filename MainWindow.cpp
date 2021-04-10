#include "MainWindow.h"
#include <wx/file.h>
#include <iostream>
#include <cstdlib>
#include <string>
#include <sstream>

using std::string;

wxBEGIN_EVENT_TABLE(MainWindow, wxFrame)
	EVT_COMBOBOX(1000, OnAssetSelection)
	EVT_BUTTON(1001, OnComputeClick)
	EVT_BUTTON(1002, OnSaveClick)
	EVT_BUTTON(1003, OnCreateClick)
	EVT_BUTTON(1004, OnViewClick)
wxEND_EVENT_TABLE()

//-------------------------CTOR - INITIAL WINDOW SETUP-------------------------
MainWindow::MainWindow() : wxFrame(nullptr, wxID_ANY, "Open Asset Pricer", wxPoint(100, 100), wxSize(800, 800))
{
	mainHeight = this->GetSize().GetHeight();
	mainWidth = this->GetSize().GetWidth();

	SetBackgroundColour(wxColour(240, 240, 240)); // Set background colour to light grey

	assetCreateLabel = new wxStaticText(this, wxID_ANY, wxString("Create New Asset:"), wxPoint(50, 50), wxSize(250, 20));
	// Setting up asset menu
	constexpr int size = 11;
	wxString list[size] = {
		wxString("Fixed Income Assets:"), 
		wxString("\u2022Bond"),
		wxString("\u2022Callable Bond"),
		wxString("\u2022Convertible Bond"),
		wxString("\u2022Zero Coupon Bond"),
		wxString("\u2022Perpetuity"),
		wxString(""),
		wxString("Options:"),
		wxString("\u2022American Option"),
		wxString("\u2022European Option"),
		wxString("\u2022Bond Option")
	};
	assetMenu = new wxComboBox(this, 1000, wxString("Choose Asset"), wxPoint(50, 75), wxSize(250, 20), size, list);

	portfolioSecLabel = new wxStaticText(this, wxID_ANY, wxString("Portfolios:"), wxPoint(50, 175), wxSize(250, 20));
	portfolioCreate = new wxButton(this, 1003, wxString("Create Portfolio"), wxPoint(50, 200), wxSize(100, 20));
	portfolioView = new wxButton(this, 1004, wxString("View Portfolios"), wxPoint(200, 200), wxSize(100, 20));
	portfolioBox = new wxListBox(this, wxID_ANY, wxPoint(50, 250), wxSize(250, 450));
}

//-------------------------ENTRY FORM DESTRUCTION AND CREATION-------------------------
// HELPER FUNCTION for setupForm()
void MainWindow::destroyForm()
{
	// Deleting labels of entry boxes
	for (wxStaticText* i : labels)
	{
		if (i)
		{
			i->Destroy(); // wx component-specific function that removes the component
			
		}
	}
	// Deleting entry boxes
	for (wxTextCtrl* i : textCtrls)
	{
		if (i)
		{
			i->Destroy(); // wx component-specific function that removes the component
		}
	}
	// Deleting put check box (for options)
	if (putCheck)
	{
		putCheck->Destroy(); // wx component-specific function that removes the component
		putCheck = nullptr;
	}
	// Deleting compute button (if-statement for safety, first time setup has computeBtn = nullptr)
	if (computeBtn)
	{
		computeBtn->Destroy();
		computeBtn = nullptr;
	}
	// Deleting save button (if-statement for safety, first time setup has saveBtn = nullptr)
	if (saveBtn)
	{
		saveBtn->Destroy();
		saveBtn = nullptr;
	}
	if (resultsLabel)
	{
		resultsLabel->Destroy();
		resultsLabel = nullptr;
	}
	if (displayBox)
	{
		displayBox->Destroy();
		displayBox = nullptr;
	}
}

void MainWindow::setupForm(const int count, string* paramNames)
{
	destroyForm(); // Empties all components
	
	currentParamCount = count; // Global variable indicating number of inputs

	// Position of top-left corner of the entry form
	// The position of all items inside will be set relative to this
	formX = 450;
	formY = (mainHeight/2) - ((25*(count+1)) + 70) - 33; // Centers form vertically. Form height is 50*count + 140

	resultsLabel = new wxStaticText(this, wxID_ANY, wxString("Results"), wxPoint(formX, formY + (50 * count)), wxSize(150, 20));
	displayBox = new wxListBox(this, wxID_ANY, wxPoint(formX, formY + 25 + (50 * count)), wxSize(270, 140));
	computeBtn = new wxButton(this, 1001, wxString("Compute"), wxPoint(formX + 170, formY + 195 + (50 * count)), wxSize(100, 20));
	saveBtn = new wxButton(this, 1002, wxString("Save Asset"), wxPoint(formX, formY + 195 + (50 * count)), wxSize(100, 20));

	// Making labels for input variable text entry boxes
	for (int i = 0; i < count; ++i)
	{
		wxString* convName = new wxString(paramNames[i]); // Gets label names from the param names found in asset class
		labels[i] = new wxStaticText(this, wxID_ANY, *convName, wxPoint(formX, formY + 2 + (50 * i)), wxSize(150, 40));
	}
	// Making text entry boxes
	// Stops at count-1 because for options, call or put needs a checkbox rather than a textbox
	for (int i = 0; i < count - 1; ++i)
	{
		textCtrls[i] = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(formX + 170, formY + (50 * i)), wxSize(100, 20));
	}
	
	// Checks if final param is supposed to be a checkbox (the case for options) and acts accordingly
	if (paramNames[count - 1].compare("Put? ") == 0)
	{
		putCheck = new wxCheckBox(this, wxID_ANY, "", wxPoint(formX + 170, formY + (50 * (count-1))));
	}
	else
	{
		textCtrls[count - 1] = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(formX + 170, formY + (50 * (count-1))), wxSize(100, 20));
	}
}

//-------------------------EVENT HANDLERS FOR BUTTONS/COMBOBOX-------------------------
void MainWindow::OnCreateClick(wxCommandEvent& evt)
{
	portfolioWindow = new PortfolioWindow(this, PortfolioWindow::CREATE);
	portfolioWindow->SetWindowStyle(wxSTAY_ON_TOP);
	portfolioWindow->Refresh();
	portfolioWindow->Show();
}

void MainWindow::OnViewClick(wxCommandEvent& evt)
{
	portfolioWindow = new PortfolioWindow(this, PortfolioWindow::VIEW);
	portfolioWindow->SetWindowStyle(wxSTAY_ON_TOP);
	portfolioWindow->Refresh();
	portfolioWindow->Show();
}
void MainWindow::displayPortfolio(wxString portfolioName, wxString currentDir)
{
	if (portfolioBox)
	{
		portfolioBox->Destroy();
		portfolioBox = new wxListBox(this, wxID_ANY, wxPoint(50, 250), wxSize(250, 450));
	}
	destroyForm();

	// First take name and show it in portfolio display box
	portfolioBox->AppendString(portfolioName);
	portfolioBox->AppendString(wxString("-------------------------\n"));

	// This will be the portfolio filepath
	wxString portfolioPath = (currentDir + wxString("\\portfolios\\") + portfolioName + wxString(".bat"));

	wxFile* portfolioFile = new wxFile(portfolioPath);
	if (!portfolioFile->IsOpened())
	{
		return;
	}
	wxString contentsRaw;
	portfolioFile->ReadAll(&contentsRaw);
	string contents = contentsRaw.ToStdString();

	vector<string> output;

	string line;
	std::stringstream ssin(contents);
	
	while (std::getline(ssin, line, '\n'))
	{
		output.push_back(line);
	}

	for (string s : output)
	{
		portfolioBox->AppendString(s);
	}
}

void MainWindow::OnAssetSelection(wxCommandEvent& evt)
{
	delete asset;
	paramNames.clear();

	// When an asset is selected from the drop down menu, it is stored here
	string selection = assetMenu->GetValue().ToStdString();

	// The factory pointer is set to the factory function corresponding to the selection
	// The form is then set up accordingly
	if (selection.compare("\u2022Bond") == 0)
	{
		factoryPtr = Bond::factory;
		setupForm(Bond::BOND_PARAM_COUNT, Bond::BOND_PARAM_NAMES);
		for (string s : Bond::BOND_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022Callable Bond") == 0)
	{
		factoryPtr = CallableBond::factory;
		setupForm(CallableBond::CALLABLE_PARAM_COUNT, CallableBond::CALLABLE_PARAM_NAMES);
		for (string s : CallableBond::CALLABLE_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022Convertible Bond") == 0)
	{
		factoryPtr = ConvertibleBond::factory;
		setupForm(ConvertibleBond::CONVERTIBLE_PARAM_COUNT, ConvertibleBond::CONVERTIBLE_PARAM_NAMES);
		for (string s : ConvertibleBond::CONVERTIBLE_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022Zero Coupon Bond") == 0)
	{
		factoryPtr = ZeroCouponBond::factory;
		setupForm(ZeroCouponBond::ZCB_PARAM_COUNT, ZeroCouponBond::ZCB_PARAM_NAMES);
		for (string s : ZeroCouponBond::ZCB_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022Perpetuity") == 0)
	{
		factoryPtr = Perpetuity::factory;
		setupForm(Perpetuity::PERP_PARAM_COUNT, Perpetuity::PERP_PARAM_NAMES);
		for (string s : Perpetuity::PERP_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022American Option") == 0)
	{
		factoryPtr = AmericanOption::factory;
		setupForm(AmericanOption::AMOPT_PARAM_COUNT, AmericanOption::AMOPT_PARAM_NAMES);
		for (string s : AmericanOption::AMOPT_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022European Option") == 0)
	{
		factoryPtr = EuropeanOption::factory;
		setupForm(EuropeanOption::EUROPT_PARAM_COUNT, EuropeanOption::EUROPT_PARAM_NAMES);
		for (string s : EuropeanOption::EUROPT_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
	else if (selection.compare("\u2022Bond Option") == 0)
	{
		factoryPtr = BondOption::factory;
		setupForm(BondOption::BONDOPT_PARAM_COUNT, BondOption::BONDOPT_PARAM_NAMES);
		for (string s : BondOption::BONDOPT_PARAM_NAMES)
		{
			paramNames.push_back(s);
		}
	}
}

void MainWindow::OnSaveClick(wxCommandEvent& evt)
{
	if (!asset)
	{
		return;
	}

	//portfolioWindow = new PortfolioWindow();
	portfolioWindow = new PortfolioWindow(this, PortfolioWindow::SAVE, paramNames, asset->getParams(), asset->getResults());
	portfolioWindow->SetWindowStyle(wxSTAY_ON_TOP);
	portfolioWindow->Refresh();
	portfolioWindow->Show();
}

void MainWindow::OnComputeClick(wxCommandEvent& evt)
{
	// Clearing results display box in case it contains the results of an old asset valuation
	displayBox->Destroy();
	displayBox = new wxListBox(this, wxID_ANY, wxPoint(formX, formY + 25 + (50 * currentParamCount)), wxSize(270, 140));

	// string array to store inputs fetched from text entry boxes
	vector<string> inputs;
	
	int n = currentParamCount;

	// If it is an option, the last input will be a boolean from the put check box, not a string from a textbox
	if (putCheck)
	{
		--n;
	}
	for (int i = 0; i < n; ++i)
	{
		// Goes to the ith text entry box, takes the value (wxString type), then converts it to a string
		inputs.push_back(textCtrls[i]->GetValue().ToStdString());
	}
	// Validating inputs
	for (int i = 1; i < n; ++i)
	{
		string s = textCtrls[i]->GetValue().ToStdString();
		if (s.compare("") == 0)
		{
			return;
		}
		for (char &c : s)
		{
			if (c != 46 && c < 48 && c > 57)
			{
				return;
			}
		}
	}
	if (putCheck)
	{
		inputs.push_back(std::to_string((int)(putCheck->GetValue()))); // Convert check box to int then string
	}
	
	asset = factoryPtr(inputs); // Calls the relevant factory function on the arguments taken from the text boxes

	// Fetches results from the asset that was created
	vector<string> results = asset->getResults();

	// Loops through results and displays them on display box
	for (string s : results)
	{
		displayBox->AppendString(wxString("\u2022"+ s));
	}
}