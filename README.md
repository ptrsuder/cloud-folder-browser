# Cloud Folder Browser

![icon](https://i.imgur.com/jy2KCG1.png) 

![downloads_latest](https://img.shields.io/github/downloads/ptrsuder/cloud-folder-browser/latest/total.svg?color=magenta)
![downloads_total](https://img.shields.io/github/downloads/ptrsuder/cloud-folder-browser/total.svg?label=downloads%40total)

Desktop application that allows user to browse (filter, sort) shared folders from various cloud services and some web server index folders. 

Cloud storages:
* MEGA
* Allsync
* YandexDisk (bad support)

Web server indexes:
* h5ai (anything that is "powered by h5ai")
* https://thetrove.is/Browse/

Download files as JDownloader2 packages or directly from app.

## Requirements
* .Net Framework 4.5

## How to use

### Load and save folders:
* Click [Add new] button
* Select new item in dropdown list, edit its name, click [Edit], edit link below, click [Save]
* Click [Load], wait for folder contents to load
* Now you can save loaded content to .json file by clicking [Save to file] and later load it to save time (not supported for mega folders)
* 
### Find missing files
* Right treeview:
  * Choose folder on your PC that you wish to use as save/sync folder
  * You can refresh folder contents using RMB
* Left treeview:
  * You should check web folders that you wish to download/sync
  * Check mark means all files within all subfolders will be selected 
  * Square means only files in top folder will be selected
  * You can use RMB context menu to quiclky check/uncheck all folders
* Click [Compare folders content]
* New window will display all files that are missing in your sync folder (so if you want just to download checked folders, select any empty folder as your sync folder)
### Get links or download files
* Check files that you wish to download
  * To generate links for JDownloader click [Get JDownloader links] - with random name will be created in app folder. Drag-n-drop it into JDownloader.  
  * Use [Mega download] if using mega cloud link
  * Use [Download] for Allsync files (even password protected) or files from web index folders
