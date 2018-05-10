
# Getting Started with TrueConf SDK for Windows Samples

To create your first video conferencing application based on these samples please install SDK library first:

* Run callx_setup.exe with administrator priviliges.
* Create new Windows Forms Control Library project in Visual Studio.
* Add TrueConf component to a Visual Studio tool box: 
	* Open the Toolbox (Ctrl+Alt+X) and then right click on it.
	* Choose Items from the context menu.
	* Open COM Components tab.
	* Find and check TrueConfCallX Class. Click Ok.
	* Now you can use TrueConf SDK for Windows control tool.
* Add TrueConf SDK for Windows control to your control library project:
	* Open the Toolbox (Ctrl+Alt+X) and then add the TrueConf SDK control to the window in desiner.
	* Set the Dock property of TrueConf SDK control to the Fill.
* Build the Windows Forms Control Library project (Debug or Release).
* Create new Wpf project in Visual Studio.
* Add the AxInterop.TrueConf_CallXLib.dll to the References from the bin\Debug(Release) folder of the Windows Forms Control Library project:
	* Right click on the References in Solution Explorer.
	* Select Add reference from the context menu.
	* Click the Browse... button.
	* Open the bin/Debug(Release) folder of the Windows Forms Library project and select the AxInterop.TrueConf_CallXLib.dll.
	* Click Ok button.
* Add the System.Windows.Forms.dll and WindowsFormsIntegration.dll to the References:
	* Right click on the References in Solution Explorer.
	* Select Add reference from the context menu.
	* Open Assemblies tab.
	* Find and check the Windows.Forms library in the list.
	* Find and check the WindowsFormsIntegration library in the list.
	* Click Ok button.
	
Once done copy and paste contents of the samples from this directory to your project. That's it!
