#include "PortfolioWindow.h"
#include <wx/file.h>
#include "MainWindow.h"

#if _WIN32
#define getcwd _getcwd
#endif // _WIN32

wxBEGIN_EVENT_TABLE(PortfolioWindow, wxFrame)
	EVT_COMBOBOX(2000, OnPortfolioSelection)
	EVT_BUTTON(2001, OnCreateClick)
	EVT_BUTTON(2002, OnCancelClick)
wxEND_EVENT_TABLE()

// Returns current directory
string PortfolioWindow::get_cwd()
{
	const size_t chunkSize = 255;
	const int maxChunks = 10240; // 2550 KiBs of current path are more than enough

	char stackBuffer[chunkSize]; // Stack buffer for the "normal" case
	if (getcwd(stackBuffer, sizeof(stackBuffer)) != NULL)
		return stackBuffer;
	if (errno != ERANGE)
	{
		// It's not ERANGE, so we don't know how to handle it
		throw std::runtime_error("Cannot determine the current path.");
		// Of course you may choose a different error reporting method
	}
	// Ok, the stack buffer isn't long enough; fallback to heap allocation
	for (int chunks = 2; chunks < maxChunks; chunks++)
	{
		// With boost use scoped_ptr; in C++0x, use unique_ptr
		// If you want to be less C++ but more efficient you may want to use realloc
		std::auto_ptr<char> cwd(new char[chunkSize * chunks]);
		if (getcwd(cwd.get(), chunkSize * chunks) != NULL)
			return cwd.get();
		if (errno != ERANGE)
		{
			// It's not ERANGE, so we don't know how to handle it
			throw std::runtime_error("Cannot determine the current path.");
			// Of course you may choose a different error reporting method
		}
	}
	throw std::runtime_error("Cannot determine the current path; the path is apparently unreasonably long");
}

// For viewing/creating portfolios
PortfolioWindow::PortfolioWindow(wxWindow* parent, PURPOSE p) : 
	wxFrame(parent, wxID_ANY, "Portfolios", wxPoint(300, 400), wxSize(600, 120)),
	purpose(p)
{
	SetBackgroundColour(wxColour(240, 240, 240)); // Set background color to light grey
	if (purpose == CREATE)
	{
		// Label, text entry box and create/cancel button
		nameLabel = new wxStaticText(this, wxID_ANY, wxString("New Portfolio:"), wxPoint(60, 27), wxSize(100, 20));
		newPortfolioName = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(210, 25), wxSize(180, 20));
		createBtn = new wxButton(this, 2001, wxString("Create Portfolio"), wxPoint(410, 25), wxSize(100, 20));
		cancelBtn = new wxButton(this, 2002, wxString("Cancel"), wxPoint(250, 80), wxSize(100, 20));
	}
	else if (purpose == VIEW)
	{
		// To store names of existing portfolios
		vector<string> portfolioNames;

		// Create a wxString, which is used to store lines from the text file
		wxString namesRaw;
		wxFile* nameFile = new wxFile(currentDir + wxString("\\portfolios\\names.bat")); // File where names of portfolios are stored
		if (!nameFile->IsOpened())
		{
			return;
		}
		nameFile->ReadAll(&namesRaw); // Read contents into namesRaw
		nameFile->Close();

		string namesStr = namesRaw.ToStdString(); // To store namesRaw converted to std::string
		string line;
		std::stringstream ssin(namesStr);
		while (std::getline(ssin, line, '\n'))
		{
			portfolioNames.push_back(line); // Put the names in the vector
		}

		int size = static_cast<int>(portfolioNames.size());
		wxString* list = new wxString[size]; // To setup the drop down menu with the portfolio names

		for (int i = 0; i < size; ++i)
		{
			list[i] = wxString(portfolioNames[i]);
		}
		// Create label, drop down menu of portfolio names and cancel button
		listLabel = new wxStaticText(this, wxID_ANY, wxString("Existing Portfolio:"), wxPoint(60, 27), wxSize(100, 20));
		portfolioList = new wxComboBox(this, 2000, wxString("Choose Portfolio"), wxPoint(210, 25), wxSize(300, 20), size, list);
		cancelBtn = new wxButton(this, 2002, wxString("Cancel"), wxPoint(250, 80), wxSize(100, 20));
	}
}

// For saving assets into a portfolio
PortfolioWindow::PortfolioWindow(wxWindow * parent, PURPOSE p, vector<string> paramNames, vector<string> params, vector<string> results) : 
	wxFrame(parent, wxID_ANY, "Portfolios", wxPoint(300, 400), wxSize(600, 140)),
	purpose(p),
	parameterNames(paramNames), 
	parameters(params), 
	results(results)
{
	SetBackgroundColour(wxColour(240, 240, 240));
	vector<string> portfolioNames; // To store existing portfolio names

	// Create a wxString, which is used to store lines from the text file
	wxString namesRaw;
	wxFile* nameFile = new wxFile(currentDir + wxString("\\portfolios\\names.bat")); // File where existing portfolio names are stored
	if (!nameFile->IsOpened())
	{
		return;
	}
	nameFile->ReadAll(&namesRaw); // Read file contrents into namesRaw
	nameFile->Close();

	string namesStr = namesRaw.ToStdString(); // Convert contents to std::string
	string line;
	std::stringstream ssin(namesStr);
	while (std::getline(ssin, line, '\n'))
	{
		portfolioNames.push_back(line); // Push contents, line by line, into vector
	}

	int size = static_cast<int>(portfolioNames.size());
	wxString* list = new wxString[size]; // To set up drop down menu of portfolio names

	for (int i = 0; i < size; ++i)
	{
		list[i] = wxString(portfolioNames[i]);
	}

	// Label for existing portfolios and drop down menu of portfolio names
	listLabel = new wxStaticText(this, wxID_ANY, wxString("Existing Portfolio:"), wxPoint(60, 22), wxSize(100, 20));
	portfolioList = new wxComboBox(this, 2000, wxString("Choose Portfolio"), wxPoint(210, 20), wxSize(300, 20), size, list);

	// Label for new portfolio name, text entry box, and create button
	nameLabel = new wxStaticText(this, wxID_ANY, wxString("New Portfolio:"), wxPoint(60, 62), wxSize(100, 20));
	newPortfolioName = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(210, 60), wxSize(180, 20));
	createBtn = new wxButton(this, 2001, wxString("Create Portfolio"), wxPoint(410, 60), wxSize(100, 20));

	// Cancel button
	cancelBtn = new wxButton(this, 2002, wxString("Cancel"), wxPoint(250, 100), wxSize(100, 20));
}

void PortfolioWindow::OnCancelClick(wxCommandEvent& evt)
{
	this->Destroy(); // Close Window
}

// If a portfolio has been selected from the drop down menu
void PortfolioWindow::OnPortfolioSelection(wxCommandEvent& evt)
{
	wxString portfolioName = portfolioList->GetValue();

	// If an asset is being saved, add it to portfolio file. If the portfolio is just being viewed, skip this
	if (purpose == SAVE)
	{
		// Opens up the portfolio file for writing
		wxFile* portfolioFile = new wxFile(currentDir + wxString("\\portfolios\\") + portfolioName + wxString(".bat"), wxFile::write_append);
		if (!portfolioFile->IsOpened())
		{
			return;
		}
		portfolioFile->Write(wxString("-------------------------\n"));
		int paramSize = static_cast<int>(parameterNames.size());
		for (int i = 0; i < paramSize; ++i)
		{
			// Write parameters (inputs of the asset like interest rate, time to maturity, etc.)
			portfolioFile->Write(wxString(parameterNames[i]) + wxString(parameters[i]) + wxString("\n"));
		}
		portfolioFile->Write(wxString("\n"));
		int resultSize = static_cast<int>(results.size());
		for (int i = 0; i < resultSize; ++i)
		{
			// Write valuation results (like price, duration, Greeks)
			portfolioFile->Write(wxString(results[i]) + wxString("\n"));
		}
		portfolioFile->Close();
	}

	// Then, whether it's an asset being saved or just a portfolio viewing, display portfolio
	((MainWindow*)(this->GetParent()))->displayPortfolio(portfolioName, currentDir);

	this->Destroy(); // Close Window
}

void PortfolioWindow::OnCreateClick(wxCommandEvent& evt)
{
	//-----Check if the portfolio name already exists-----
	string newName = newPortfolioName->GetValue().ToStdString();
	// Create a wxString, which is used to store lines from the text file
	wxString namesRaw;
	wxFile* nameFile = new wxFile(currentDir + wxString("\\portfolios\\names.bat")); // File where the existing portfolio names are stored
	if (!nameFile->IsOpened())
	{
		return;
	}
	nameFile->ReadAll(&namesRaw); // Read contents into namesRaw
	nameFile->Close();

	string namesStr = namesRaw.ToStdString(); // Convert contents to std::string
	vector<string> names; // To store existing portfolio names
	string line;
	std::stringstream ssin(namesStr);
	while (std::getline(ssin, line, '\n'))
	{
		names.push_back(line); // Push contents, string by string, into vector
	}
	for (string s : names)
	{
		if (s.compare(newName) == 0) // If the name is found in the list...
		{
			return; //... stop the function call
		}
	}

	//-----Checking if the name is invalid-----
	// Whether the portfolio name is too long or not
	if (newName.length() > 30)
	{
		return;
	}
	// Whether the name is blank or not, or whether it's going to overwrite the file containing all the existing portfolio names
	if (newName.compare("") == 0 || newName.compare("names") == 0)
	{
		return;
	}
	// Whether the name contains characters not allowed by the Windows filesystem
	if (newName.find("\"") != -1 ||
		newName.find("*") != -1 ||
		newName.find("<") != -1 ||
		newName.find(">") != -1 ||
		newName.find("?") != -1 ||
		newName.find("\\") != -1 ||
		newName.find("|") != -1 ||
		newName.find("/") != -1 ||
		newName.find(":") != -1)
	{
		return;
	}

	//-----Writing new name to file containing all names-----
	wxFile* nameFileWrite = new wxFile(currentDir + wxString("\\portfolios\\names.bat"), wxFile::write_append);
	if (!nameFileWrite->IsOpened())
	{
		return;
	}
	nameFileWrite->Write(wxString(newName) + wxString("\n"));
	nameFileWrite->Close();

	//-----Creating new file for the portfolio-----
	string newPortfolioDir = string("\\portfolios\\") + newName + string(".bat");
	wxFile* portfolioFile = new wxFile(currentDir + wxString(newPortfolioDir), wxFile::write_append);
	if (!portfolioFile->IsOpened())
	{
		return;
	}

	//-----If the user is creating the portfolio to save an asset, save it-----
	if (purpose == SAVE)
	{
		portfolioFile->Write(wxString("-------------------------\n")); // Separator, placed between assets in the portfolio
		int paramSize = static_cast<int>(parameterNames.size());
		for (int i = 0; i < paramSize; ++i)
		{
			// Write parameters (inputs used to create the asset, like interest rate, time to maturity, etc.)
			portfolioFile->Write(wxString(parameterNames[i]) + wxString(parameters[i]) + wxString("\n"));
		}
		portfolioFile->Write(wxString("\n"));
		int resultSize = static_cast<int>(results.size());
		for (int i = 0; i < resultSize; ++i)
		{
			// Write valuation results (like price, duration, Greeks, etc.)
			portfolioFile->Write(wxString(results[i]) + wxString("\n"));
		}
		portfolioFile->Close();
	}
	portfolioFile->Close();
	this->Destroy(); // Close Window
}