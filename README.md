# Cloud Folder Browser

![icon](https://i.imgur.com/jy2KCG1.png) 


![downloads_latest](https://img.shields.io/github/downloads/ptrsuder/cloud-folder-browser/latest/total.svg?color=magenta)
![downloads_total](https://img.shields.io/github/downloads/ptrsuder/cloud-folder-browser/total.svg?label=downloads%40total)

![Clipboard Image (1)](https://github.com/ptrsuder/cloud-folder-browser/assets/18538582/c1e82722-ec59-43dc-bbb0-2cb7458afcf2)

Desktop application that allows user to browse shared folders from few cloud storage services. 

Features:
* Name/date filter, sort, flat file list
* Sync with files on local disk
* Export selected files as JDownloader2 packages 
* Download selected files directly from app

Supported cloud storages:
* MEGA
* Allsync/Qloud (support for password protected)
* YandexDisk (legacy, bad support)

## Requirements
* .NET 6 (https://dotnet.microsoft.com/en-us/download/dotnet/6.0 - .NET Desktop Runtime 6.0.*)

## How to use

### Load and save folders:
* Click [Add] button, fill new link info
* To edit existing link select it in dropdown list, click [Edit]
* Click [Load] to load link, wait for contents to load
* Now you can save loaded content to .json file by clicking [Save to file] and later load it by [Open from file] to save time. Use this only for browsing.
* 
### Find missing files
* Right treeview:
  * Choose folder on your PC that you wish to use as save/sync folder
  * You can refresh folder contents using RMB context menu
* Left treeview:
  * You should check folders that you wish to download/sync
  * *Check* mark means all files within all subfolders will be selected, *square* means only files in top folder will be selected
  * You can use RMB context menu to quickly check/uncheck all folders
* Click [Compare folders content]
* New window will display all files that are missing in your sync folder (so if you want to simply download checked folders, you can select any empty folder as your sync folder)

### Get links or download files
* Check files that you wish to download
  * To generate links for JDownloader click [Get JDownloader links] - with random name will be created in app folder. Drag-n-drop it into JDownloader.  
  * Use [Mega download] if using mega cloud link
  * Use [Mega import] if using mega cloud link and if you did sign-in into your mega account 
  * Use [Download] for Allsync files
