# FinAnalysisSoftware

Financial analysis software for the valuation of various fixed income securities and derivatives.

----------Methods----------
Fixed Income Securities:
-Plain vanilla bonds are valued with a Discounted Cash Flows (DCF) model.
-Callable bonds are valued by subtracting the price of the embedded call option, valued with the Black Model, from the corresponding non-callable DCF price of the bond.

Options:
-European options are valued using the relevant Black-Scholes derived formula
