using System;
using System.Collections;

namespace OAP_CS
{
	class BondOption : Item
	{
		static ArrayList blank = new ArrayList() { "" };

		//----------------------------------------FIELDS----------------------------------------
		protected double forwardPrice, callPrice, forwardVol, timeToCall, interestRate;
		protected double callOrPut;
		protected double spotPrice = 0.0;

		//----------------------------------------CTOR & FACTORY----------------------------------------
		public BondOption(ArrayList inputs) : base(inputs)
		{
			parameterNames = new string[7];
			parameterNames[0] = (new string("Name:"));
			parameterNames[1] = (new string("Forward Price: "));
			parameterNames[2] = (new string("Call Price: "));
			parameterNames[3] = (new string("Ann. Forward Vol. (%): "));
			parameterNames[4] = (new string("Time to Call (Yrs): "));
			parameterNames[5] = (new string("Ann. Interest Rate (%): "));
			parameterNames[6] = (new string("Put?: "));

			forwardPrice = dConvert((string)inputs[1]);
			callPrice = dConvert((string)inputs[2]);
			forwardVol = (dConvert((string)inputs[3])) / 100.0;
			timeToCall = dConvert((string)inputs[4]);
			interestRate = dConvert((string)inputs[5]) / 100.0;
			callOrPut = dConvert((string)inputs[6]) == 1.0 ? 1.0 : -1.0;
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