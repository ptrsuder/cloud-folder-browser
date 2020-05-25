# Cloud Folder Browser
![downloads_latest](https://img.shields.io/github/downloads/ptrsuder/cloud-folder-browser/latest/total.svg?color=magenta)
![downloads_total](https://img.shields.io/github/downloads/ptrsuder/cloud-folder-browser/total.svg?label=downloads%40total)

Desktop application that allows user to browse (filter, sort) shared folders from various cloud services and some web server index folders. 

Cloud storages:
* MEGA
* Allsync
* YandexDisk

Web server indexes:
* h5ai (anything that is "powered by h5ai")
* godir (https://thetrove.net/)

## Requirements
* .Net Framework 4.5

![screen](https://i.imgur.com/qdtG7Yt.jpg)

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
### Yandex disk support
* If user is logined in yandex, they can add selected files directly to ya.disk. If there is enough free space, files will be created in folder named <yadisk_browser_folder>
