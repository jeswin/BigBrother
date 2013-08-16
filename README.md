BigBrother
==========

.Net Debugger for capturing Method Parameters and Return values

Based on Microsoft's MDbg Managed Debugger.
Monitors method entry and exit, and captures input parameters are return values.

Structure
---------
BigBrother.dll is the Debugger Core.
Snoopy is a command line application that hosts BigBrother.dll.

How to Run
----------
Snoopy.exe -c ..\\..\\src\\ExampleApp\\exampleapp.config.json -e ..\\..\\src\\ExampleApp\\bin\\Debug\\ExampleApp.exe

Help
----
Snoopy.exe -h to see help.
