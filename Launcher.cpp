#include "Launcher.h"

wxIMPLEMENT_APP(Launcher);

Launcher::Launcher()
{

}

Launcher::~Launcher()
{

}

bool Launcher::OnInit()
{
	// frame1 is an instance of MainWindow
	frame1 = new MainWindow();
	frame1->SetMinSize(wxSize(800, 650));
	frame1->SetMaxSize(wxSize(800, 650));
	frame1->Show();

	return true;
}