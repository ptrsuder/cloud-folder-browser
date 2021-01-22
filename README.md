# Cloud Folder Browser
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

![screen](https://i.imgur.com/PkEbP9I.png)

## How to use
### Load and store folders:
* Click [Add new] button
* Select new item in dropdown list, edit its name, click [Edit], edit link below, click [Save]
* Click [Load], wait for folder contents to load
* Now you can save loaded content to .json file by clicking [Save to file] and later load it to save time
### Get links and sync files
* On right side choose folder on your computer that you wish to use as save/sync folder
* Check web folders that you wish to download/sync
* Click [Compare folders content]
* New window will display all files that are missing in your sync folder (so if you want just to download checked folders, select any empty folder as your sync folder)
* New form will be displayed, check files that you wish to download
* Click [Get JDownloader links]
* JD link container with random name will be created in app folder
* Use [Mega download] if web folder is mega folder (no spicy JD links for mega)
* Use [Download] for Allsync files (even password protected) or files from web index folders
