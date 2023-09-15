# Application "SortFiles"
Windows Service application, that sorts the files inserted inside directory.

## Table Of Content
1. How to: Get code and write own changes
2. How to: Install it
3. How to: Maintain this program

### 1. Get code and write own changes
- Clone this repository, or download as .ZIP package:
    - You can do this thru green button with text "<> Code" up here.
- Open .sln with Visual Studio
- As entry point of the code we have "Service1.cs". You can open it by clicking "switch to code view" if code is not visible:
    - Function "OnStart()" is called when service becomes started. 
    - Function "OnStop()" is called when service becomes stopped. 
    - But the real code is inside "DoWork()" function that is called every configured period.

### 2. Install it
- Update "Config" class:
    - All 3 directories must exist!
    - Decide what to do with underscore
    - Set per how many seconds the function should be called.
- Switch inside Visual Studio the Configuration Manager option from "Debug" to "Release"
- Build the project
- Open project files
- Go to "*ApplicationDirectory*\bin\Release" and there should be 3 files: 
    - .exe 
    - .config 
    - .pdb
- Copy them to directory, that will serve as application directory (maybe C:\SortFiles).
- To the same directory copy .bat scripts that I included inside repositoy:
    - "Install_Bat\DeleteService_SortFiles.bat" 
    - "Install_Bat\InstallService_SortFiles.bat"
- Update "InstallService_SortFiles.bat":
    - "binpath=" should lead to SortFiles.exe file (maybe C:\SortFiles\SortFiles.exe)
- Run "InstallService_SortFiles.bat" as admin.
- Done. You can inspect "Services" windows application, there should be "SortFiles" service already working.
- You can always uninstall this application, by running "DeleteService_SortFiles.bat" as admin.

### 3. Maintain this program
- From time to time inspect "Logs.txt" file, that is inside application directory (maybe C:\SortFiles).
- If there is "CriticalLogs.txt" read it and react. Then you may delete this file (I mean "CriticalLogs.txt" only!). It is easier to just check if it exists.
