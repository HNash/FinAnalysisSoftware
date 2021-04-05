#pragma once

#include "wx/wx.h"
#include "MainWindow.h"

class Launcher : public wxApp
{
	public:
		Launcher();
		~Launcher();

		virtual bool OnInit();

	private:
		MainWindow* frame1 = nullptr;
};