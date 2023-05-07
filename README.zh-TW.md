# 桌面文件管理器

This program sorts files and folders on the user's desktop into specific subfolders based on their file type or other criteria. This is a modified version of the [凌亂的桌面清潔工](https://www.unknowncheats.me/forum/c-/578800-messy-desktop-cleaner.html)程序。

## Translation

| 🇺🇸            | 🇨🇳                    | 🇹🇼                    | 🇮🇳                  | 🇫🇷               | 🇦🇪                | 🇩🇪               | 🇯🇵                | 🇪🇸                 |
| --------------- | ----------------------- | ----------------------- | --------------------- | ------------------ | ------------------- | ------------------ | ------------------- | -------------------- |
| [英語](README.md) | [简体中文](README.zh-CN.md) | [繁体中文](README.zh-TW.md) | [हिंदी](README.hi.md) | [法語](README.fr.md) | [阿拉伯](README.ar.md) | [德語](README.de.md) | [日本人](README.ja.md) | [西班牙語](README.es.md) |

## 特徵

-   Sorts files into subfolders based on file type (e.g., images, audio, videos, etc.)
-   讀取配置文件 (config.json) 以確定哪些文件擴展名屬於哪些類別
-   如果不存在則創建默認配置文件
-   將操作記錄到日誌文件 (log.txt) 以進行故障排除

## 用法

1.  下載程序並將其放在您選擇的目錄中。
2.  運行程序。它會自動對桌面上的文件和文件夾進行排序。
3.  （可選）修改 config.json 文件以自定義排序行為。

## 學分

這個程序是修改後的版本[凌亂的桌面清潔工](https://www.unknowncheats.me/forum/c-/578800-messy-desktop-cleaner.html)程序。代碼被修改了[qzxtu](https://github.com/qzxtu).

## FAQ

**問：這個程序有什麼作用？**

答：該程序根據文件類型或其他標準將用戶桌面上的文件和文件夾分類到特定的子文件夾中。

**問：這個程序如何確定哪些文件進入哪些子文件夾？**

A：程序讀取配置文件（config.json）來確定哪些文件擴展名屬於哪些類別（例如，圖像、音頻、視頻等）。您可以修改此文件以自定義排序行為。

**Q：config.json文件不存在怎麼辦？**

A: If the config.json file doesn’t exist, the program will create a default one with pre-defined values for each category.

## 執照

This project is licensed under the MIT License. See the [執照](LICENSE)文件的詳細信息。
