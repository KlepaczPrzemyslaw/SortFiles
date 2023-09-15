ECHO "This script will install SortFiles as a service and will run it"

SC CREATE "SortFiles" binpath="C:\InsertPathToExeFileHere\SortFiles.exe" start="auto"
SC START "SortFiles"

ECHO "DONE!"
@pause