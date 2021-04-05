#include "MainWindow.h"
#include <iostream>
#include <string>

using std::string;

wxBEGIN_EVENT_TABLE(MainWindow, wxFrame)
	EVT_BUTTON(1000, OnComputeClick)
	EVT_COMBOBOX(1001, OnAssetSelection) 
wxEND_EVENT_TABLE()

MainWindow::MainWindow() : wxFrame(nullptr, wxID_ANY, "Open Asset Pricer", wxPoint(100, 100), wxSize(800, 650))
{
	SetBackgroundColour(wxColour(240, 240, 240)); // Set background colour to light grey

	// Setting up asset menu
	constexpr int size = 6;
	wxString list[size] = {
		wxString("---------------Fixed Income---------------"), 
		wxString("Bond"),
		wxString("Callable Bond"),
		wxString("Convertible Bond"),
		wxString("Zero Coupon Bond"),
		wxString("Perpetuity")};
	assetMenu = new wxComboBox(this, 1001, wxString("Pick Asset"), wxPoint(50, 47), wxSize(250, 20), size, list);

	displayBox = new wxListBox(this, wxID_ANY, wxPoint(50, 97), wxSize(250, 425));
}

MainWindow::~MainWindow()
{

}

// HELPER FUNCTION for setupForm()
void MainWindow::destroyForm()
{
	// Clearing display box and recreating it
	delete displayBox;
	displayBox = new wxListBox(this, wxID_ANY, wxPoint(50, 97), wxSize(250, 425));

	for (wxStaticText* i : labels)
	{
		if (i)
		{
			i->Destroy(); // wx component-specific function that removes the component
			i = nullptr;
		}
	}
	for (wxTextCtrl* i : textCtrls)
	{
		if (i)
		{
			i->Destroy(); // wx component-specific function that removes the component
			i = nullptr;
		}
	}
}

void MainWindow::setupForm(const int count, string* paramNames)
{
	currentParamCount = count;

	destroyForm(); // Empties all components
	
	int buttonY = 97 + (50 * count); // The compute button yPos, set to be at the bottom of all the labels
	if (btn)
	{
		btn->Destroy();
		btn = nullptr;
	}
	btn = new wxButton(this, 1000, wxString("Compute"), wxPoint(600, buttonY), wxSize(100, 20));

	for (int i = 0; i < count; ++i)
	{
		wxString* convName = new wxString(paramNames[i]); // Gets label names from the param names found in asset class
		labels[i] = new wxStaticText(this, wxID_ANY, *convName, wxPoint(450, 100 + (50 * i)), wxSize(200, 40));
	}
	for (int i = 0; i < count; ++i)
	{
		textCtrls[i] = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(650, 97 + (50 * i)), wxSize(50, 20));
	}
}

void MainWindow::OnComputeClick(wxCommandEvent& evt)
{
	// string array to store inputs fetched from text entry boxes
	vector<string> inputs;
	for (int i = 0; i < currentParamCount; ++i)
	{
		// Goes to the ith text entry box, takes the value (wxString type), then converts it to a string
		inputs.push_back(textCtrls[i]->GetValue().ToStdString());
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
	if (selection.compare("Bond") == 0)
	{
		factoryPtr = Bond::factory;
		setupForm(Bond::BOND_PARAM_COUNT, Bond::BOND_PARAM_NAMES);
	}
	else if (selection.compare("Callable Bond") == 0) 
	{
		factoryPtr = CallableBond::factory;
		setupForm(CallableBond::CALLABLE_PARAM_COUNT, CallableBond::CALLABLE_PARAM_NAMES);
	}
	else if (selection.compare("Convertible Bond") == 0)
	{
		factoryPtr = ConvertibleBond::factory;
		setupForm(ConvertibleBond::CONVERTIBLE_PARAM_COUNT, ConvertibleBond::CONVERTIBLE_PARAM_NAMES);
	}
	else if (selection.compare("Zero Coupon Bond") == 0)
	{
		factoryPtr = ZeroCouponBond::factory;
		setupForm(ZeroCouponBond::ZCB_PARAM_COUNT, ZeroCouponBond::ZCB_PARAM_NAMES);
	}
	else if (selection.compare("Perpetuity") == 0)
	{
		factoryPtr = Perpetuity::factory;
		setupForm(Perpetuity::PERP_PARAM_COUNT, Perpetuity::PERP_PARAM_NAMES);
	}
}