#include "PortfolioWindow.h"
#include <iostream>
#include <fstream>

wxBEGIN_EVENT_TABLE(PortfolioWindow, wxFrame)
	EVT_BUTTON(2001, OnCreateClick)
	EVT_BUTTON(2002, OnCancelClick)
wxEND_EVENT_TABLE()

PortfolioWindow::PortfolioWindow() : wxFrame(nullptr, wxID_ANY, "Portfolios", wxPoint(300, 400), wxSize(600, 140))
{

}

PortfolioWindow::PortfolioWindow(wxWindow * parent, vector<string> paramNames, vector<string> params, vector<string> results) : 
	wxFrame(parent, wxID_ANY, "Portfolios", wxPoint(300, 400), wxSize(600, 140)),
	parameterNames(paramNames), 
	parameters(params), 
	results(results)
{
	SetBackgroundColour(wxColour(240, 240, 240));
	vector<string> portfolioNames;

	// Create a text string, which is used to store lines from the text file
	string line;

	// Read from the text file
	ifstream readFile("/portfolios/names.bat");

	// Use a while loop together with the getline() function to read the file line by line
	while (getline(readFile, line))
	{
		portfolioNames.push_back(line);
	}
	// Close the file
	readFile.close();

	int size = static_cast<int>(portfolioNames.size());

	wxString* list = new wxString[size];

	for (int i = 0; i < size; ++i)
	{
		list[i] = wxString(portfolioNames[i]);
	}

	listLabel = new wxStaticText(this, wxID_ANY, wxString("Existing Portfolio:"), wxPoint(50, 22), wxSize(100, 20));
	portfolioList = new wxComboBox(this, 2000, wxString("Choose Portfolio"), wxPoint(200, 20), wxSize(300, 20), size, list);

	nameLabel = new wxStaticText(this, wxID_ANY, wxString("New Portfolio:"), wxPoint(50, 62), wxSize(100, 20));
	newPortfolioName = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(200, 60), wxSize(150, 20));
	createBtn = new wxButton(this, 2001, wxString("Create Portfolio"), wxPoint(400, 60), wxSize(100, 20));

	cancelBtn = new wxButton(this, 2002, wxString("Cancel"), wxPoint(250, 100), wxSize(100, 20));
}

void PortfolioWindow::OnCancelClick(wxCommandEvent& evt)
{
	this->Destroy();
}

void PortfolioWindow::OnCreateClick(wxCommandEvent& evt)
{
	// Check if the name exists-----
	string name = newPortfolioName->GetValue().ToStdString();

	// Create a text string, which is used to store lines from the text file
	string line;

	ifstream nameFile("/portfolios/names.txt");
	// Use a while loop together with the getline() function to read the file line by line
	while (getline(nameFile, line))
	{
		if (line.compare(name) == 0)
		{
			return;
		}
	}
	nameFile.close();

	ofstream nameFileWrite("/portfolios/names.txt");
	nameFileWrite << name << string("\n");
	nameFileWrite.close();

	string newPortfolioDir = string("/portfolios/") + name + string(".txt");
	ofstream portfolioFile;
	portfolioFile.open(newPortfolioDir);

	int paramSize = static_cast<int>(parameterNames.size());
	for (int i = 0; i < paramSize; ++i)
	{
		portfolioFile << parameterNames[i] << parameters[i] << string("\n");
	}
	
	portfolioFile << "\n";
	
	int resultSize = static_cast<int>(results.size());
	for (int i = 0; i < resultSize; ++i)
	{
		portfolioFile << results[i] << string("\n");
	}

	portfolioFile.close();

	this->Destroy();
}