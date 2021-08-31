#include "RatesWindow.hpp"

wxBEGIN_EVENT_TABLE(RatesWindow, wxFrame)
	EVT_BUTTON(3000, OnSubmitClick)
wxEND_EVENT_TABLE()

RatesWindow::RatesWindow(double count , double frequency) : wxFrame(nullptr, wxID_ANY, wxString("Reference Rates"), wxPoint(50, 50), wxSize(600, 100 + (50 * count)))
{
	SetBackgroundColour(wxColour(240, 240, 240));

	prompt = new wxStaticText(this, wxID_ANY, wxString("Please enter the reference rate for each of the following time frames:"), wxPoint(20, 20), wxSize(580, 20));

	for (double i = 0; i < count; ++i)
	{
		labels[(int)i] = new wxStaticText(this, wxID_ANY, wxString(std::to_string((int)(12.0 / (frequency * i)))) + wxString(" months"), wxPoint(20, 52 + (50 * i)), wxSize(100, 20));
		boxes[(int)i] = new wxTextCtrl(this, wxID_ANY, wxString(""), wxPoint(150, 50 + (50 * i)), wxSize(430, 20));
	}

	submitBtn = new wxButton(this, 3000, wxString("Submit"), wxPoint(480, 50 + (50 * count)), wxSize(100, 20));
}

vector<string> RatesWindow::getResults()
{
	return results;
}

void RatesWindow::OnSubmitClick(wxCommandEvent& evt)
{
	for (wxTextCtrl* box : boxes)
	{
		if (box)
		{
			results.push_back(box->GetValue().ToStdString());
		}
	}
	done = true;
}