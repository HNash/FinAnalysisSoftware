using System;
using System.Collections;

namespace OAP_CS
{
	[Serializable]
	class BondOption : Item
	{
		static ArrayList blank = new ArrayList() { "" };

		//----------------------------------------FIELDS----------------------------------------
		protected double forwardPrice, callPrice, forwardVol, timeToCall, interestRate;
		protected double callOrPut;
		protected double spotPrice = 0.0;
		new public static string[] parameterNames = {"Name:", "Forward Price: ", "Call Price: ",
														"Ann. Forward Vol. (%): ", "Time to Call (Yrs): ", "Ann. Interest Rate (%): ",
															"Put?:" };

		//----------------------------------------CTOR & FACTORY----------------------------------------
		public BondOption(ArrayList inputs) : base(inputs)
		{
			forwardPrice = (double)inputs[1];
			callPrice = (double)inputs[2];
			forwardVol = ((double)inputs[3]) / 100.0;
			timeToCall = (double)inputs[4];
			interestRate = ((double)inputs[5]) / 100.0;
			callOrPut = (string)inputs[6] == "1.0" ? 1.0 : -1.0;
			process();
		}

		public BondOption(string n, double fP, double cP, double fVol, double tToCall, double r, double call) : base(blank)
		{
			forwardPrice = (fP);
			callPrice = (cP);
			forwardVol = (fVol);
			timeToCall = (tToCall);
			interestRate = (r);
			callOrPut = (call == 1.0 ? 1.0 : -1.0);
			process();
		}

		public static Item factory(ArrayList inputs)
		{
			return new BondOption(inputs);
		}

		//----------------------------------------PRICE CALCULATIONS----------------------------------------
		void calculateSpot()
		{
			double d1 = (Math.Log(forwardPrice / callPrice) + ((Math.Pow(forwardVol, 2) / 2) * timeToCall)) / (forwardVol * Math.Pow(timeToCall, 0.5));
			double d2 = d1 - (forwardVol * Math.Pow(timeToCall, 0.5));
			double c = callOrPut;
			// Black model formula for price of bond option
			spotPrice = Math.Exp(-1 * interestRate * timeToCall) * c * ((forwardPrice * normalCDF(c * d1)) - (callPrice * normalCDF(c * d2)));
		}

		//----------------------------------------GETTERS----------------------------------------
		public override string[] getParameters()
		{
			string[] paramList = new string[parameterNames.Length];
			for (int i = 0; i < parameterNames.Length; ++i)
			{
				paramList[i] = parameterNames[i] + " " + parameters[i];
			}
			return paramList;
		}
		public override ArrayList getResults()
		{
			ArrayList vec = new ArrayList();

			vec.Add("Price: " + spotPrice.ToString());
			return vec;
		}

		public double getSpot()
		{
			return spotPrice;
		}

		//----------------------------------------PROCESS----------------------------------------
		public override void process()
		{
			calculateSpot();
		}
	}
}