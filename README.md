# cloud-folder-browser
Desktop application that allows user to browse (filter, sort) shared folders from various cloud services and some web server index folders. 

Cloud storages:
* MEGA
* Allsync
* YandexDisk

Web server indexes:
* h5ai (https://dnd.jambrose.info, https://dl.lynxcore.org, http://www.enthusiasticallyconfused.com/ etc)
* godir (https://thetrove.net/)

## Requirements
* .Net Framework 4.5

![screen](https://i.imgur.com/qdtG7Yt.jpg)

## How to use
### Load and store folders:
* Click [Add new] button
* Select new item in comboBox, edit it name and [Edit] link below, click [Save]
* Click [Load], wait content of folder to load
* Now you can [Save to file] loaded content and later [Open from file] to save time
### Get links and sync files
* On right side choose folder on your computer that you wish to use as save/sync folder
* Check web folders that you wish to download/sync
* Click [Compare folders content]
* New window will display all files, that are missing in your sync folder (so if you want just to download all checked folder, select any empty folder as your sync folder)
* New form will be displayed, check files that you wish to download
* Click [Get JDownloader links]
* JD link container will be created in app folder
* [Mega download] if web folder is mega folder (no spicy JD links for mega)
### Yandex disk support
* If user is logined in yandex, they can add selected files directly to your ya.disk. If there is enough free space, files will be created in folder <yadisk_browser_folder>
