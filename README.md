# AzureLogViewer

Connects to Azure Storage Accounts and easily read the latest content from the various Windows Azure Diagnostics or other custom tables.

## Installation

Binaries are available via a ClickOnce deployment on the following URL:
http://martijn.tikkie.net/apps/AzureLogViewer/AzureLogViewerGui.application

## Main features

![screenshot](http://content.screencast.com/users/Martijn_Stolk/folders/Jing/media/f23c4a1b-9954-4712-93c7-41bb7169745a/2015-08-14_0948.png)

* Add/update/remove storage accounts
* On startup, automatically select today in the From/To boxes
* Presets to quickly select date/time ranges
* High-performance scrolling through the fetched data
* High-performance filtering within the fetched data
* Scrape disk for storage accounts (recursively search connection strings in .config files)
* Export storage accounts to [Azure Storage Explorer](http://azurestorageexplorer.codeplex.com/)
* Copy to clipboard and export to CSV
* Filtering and highlighting
  * Text in the filter box will be filtered on real-time
  * Prefix a filter with ! to find lines not containing that text
  * Prefix a filter with # to highlight lnies containing that text
  * Separate multiple filters using &&
  * Example: "!getting&&#exception" will remove lines with "getting" and highligh lines with "exception"
  
![screenshot](http://content.screencast.com/users/Martijn_Stolk/folders/Jing/media/842841be-e387-4059-a48f-09bce67a7bd6/2015-08-14_0958.png)

* The WADPerformanceCounterTable can be visualized as a graph
* Scale can be set per counter
* Graph feature can be toggled using Extra => Show PerformanceCounters in a chart
* In this example 4 performance counters have been enabled
  * Instance 0 % Processor Time, default scale 1
  * Instance 1 % Processor Time, default scale 1
  * Instance 0 Available MBytes, custom scale 0.1
  * Instance 1 Available MBytes, custom scale 0.1

## Author

Martijn Stolk (http://martijnstolk.blogspot.nl)

## License

Creative Commons Attribution 3.0 Unported (CC BY 3.0)
http://creativecommons.org/licenses/by/3.0/

