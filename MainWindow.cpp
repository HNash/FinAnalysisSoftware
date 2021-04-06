#include "MainWindow.h"
#include <iostream>
#include <cstdlib>
#include <string>

using std::string;

wxBEGIN_EVENT_TABLE(MainWindow, wxFrame)
	EVT_COMBOBOX(1000, OnAssetSelection)
	EVT_BUTTON(1001, OnComputeClick)
wxEND_EVENT_TABLE()

MainWindow::MainWindow() : wxFrame(nullptr, wxID_ANY, "Open Asset Pricer", wxPoint(100, 100), wxSize(800, 650))
{
	SetBackgroundColour(wxColour(240, 240, 240)); // Set background colour to light grey

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
	assetMenu = new wxComboBox(this, 1000, wxString("Pick Asset"), wxPoint(50, 47), wxSize(250, 20), size, list);

	displayBox = new wxListBox(this, wxID_ANY, wxPoint(50, 97), wxSize(250, 425));
}

MainWindow::~MainWindow()
{

}

// HELPER FUNCTION for setupForm()
void MainWindow::destroyForm()
{
	// Clearing display box and recreating it
	displayBox->Destroy();
	displayBox = new wxListBox(this, wxID_ANY, wxPoint(50, 97), wxSize(250, 425));

	for (wxStaticText* i : labels)
	{
		if (i)
		{
			i->Destroy(); // wx component-specific function that removes the component
			
		}
	}
	for (wxTextCtrl* i : textCtrls)
	{
		if (i)
		{
			i->Destroy(); // wx component-specific function that removes the component
		}
	}
	if (putCheck)
	{
		putCheck->Destroy(); // wx component-specific function that removes the component
		putCheck = nullptr;
	}
	if (computeBtn)
	{
		computeBtn->Destroy();
		computeBtn = nullptr;
	}
}

void MainWindow::setupForm(const int count, string* paramNames)
{
	currentParamCount = count; // Global variable indicating number of inputs

	destroyForm(); // Empties all components

	// Position of top-left corner of the entry form
	// The position of all items inside will be set relative to this
	int formX = 450;
	int formY = 100;

	computeBtn = new wxButton(this, 1001, wxString("Compute"), wxPoint(formX + 150, formY + (50 * count)), wxSize(100, 20));

	// Making labels for input variable text entry boxes
	for (int i = 0; i < count; ++i)
	{
		wxString* convName = new wxString(paramNames[i]); // Gets label names from the param names found in asset class
		labels[i] = new wxStaticText(this, wxID_ANY, *convName, wxPoint(formX, formY + (50 * i)), wxSize(150, 40));
	}
	// Making text entry boxes
	// Stops at count-1 because for options, call or put needs a checkbox rather than a textbox
	for (int i = 0; i < count - 1; ++i)
	{
		textCtrls[i] = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(formX + 150, formY + (50 * i)), wxSize(50 + ((int)(i==0) * 50), 20));
	}
	
	// Checks if final param is supposed to be a checkbox and acts accordingly
	if (paramNames[count - 1].compare("Put? ") == 0)
	{
		putCheck = new wxCheckBox(this, wxID_ANY, "", wxPoint(formX + 150, formY + (50 * (count-1))));
	}
	else
	{
		textCtrls[count - 1] = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(formX + 150, formY + (50 * (count-1))), wxSize(50, 20));
	}
}

void MainWindow::OnComputeClick(wxCommandEvent& evt)
{
	displayBox->Destroy();
	displayBox = new wxListBox(this, wxID_ANY, wxPoint(50, 97), wxSize(250, 425));

	// string array to store inputs fetched from text entry boxes
	vector<string> inputs;
	
	int n = currentParamCount;

	if (putCheck)
	{
		--n;
	}
	for (int i = 0; i < n; ++i)
	{
		// Goes to the ith text entry box, takes the value (wxString type), then converts it to a string
		inputs.push_back(textCtrls[i]->GetValue().ToStdString());
	}
	if (putCheck)
	{
		inputs.push_back(std::to_string((int)(putCheck->GetValue())));
	}
	
	Item *asset = factoryPtr(inputs); // Calls the relevant factory function on the arguments taken from the text boxes

	// Fetches results from the asset that was created
	vector<string> results = asset->getResults();

	// Loops through results and displays them on display box
	for (string s : results)
	{
		displayBox->AppendString(wxString(s));
	}

	delete asset;
}

void MainWindow::OnAssetSelection(wxCommandEvent& evt)
{
	// When an asset is selected from the drop down menu, it is stored here
	string selection = assetMenu->GetValue().ToStdString();

	// The factory pointer is set to the factory function corresponding to the selection
	// The form is then set up accordingly
	if (selection.compare("\u2022Bond") == 0)
	{
		factoryPtr = Bond::factory;
		setupForm(Bond::BOND_PARAM_COUNT, Bond::BOND_PARAM_NAMES);
	}
	else if (selection.compare("\u2022Callable Bond") == 0) 
	{
		factoryPtr = CallableBond::factory;
		setupForm(CallableBond::CALLABLE_PARAM_COUNT, CallableBond::CALLABLE_PARAM_NAMES);
	}
	else if (selection.compare("\u2022Convertible Bond") == 0)
	{
		factoryPtr = ConvertibleBond::factory;
		setupForm(ConvertibleBond::CONVERTIBLE_PARAM_COUNT, ConvertibleBond::CONVERTIBLE_PARAM_NAMES);
	}
	else if (selection.compare("\u2022Zero Coupon Bond") == 0)
	{
		factoryPtr = ZeroCouponBond::factory;
		setupForm(ZeroCouponBond::ZCB_PARAM_COUNT, ZeroCouponBond::ZCB_PARAM_NAMES);
	}
	else if (selection.compare("\u2022Perpetuity") == 0)
	{
		factoryPtr = Perpetuity::factory;
		setupForm(Perpetuity::PERP_PARAM_COUNT, Perpetuity::PERP_PARAM_NAMES);
	}
	else if (selection.compare("\u2022American Option") == 0)
	{
		factoryPtr = AmericanOption::factory;
		setupForm(AmericanOption::AMOPT_PARAM_COUNT, AmericanOption::AMOPT_PARAM_NAMES);
	}
	else if (selection.compare("\u2022European Option") == 0)
	{
		factoryPtr = EuropeanOption::factory;
		setupForm(EuropeanOption::EUROPT_PARAM_COUNT, EuropeanOption::EUROPT_PARAM_NAMES);
	}
}