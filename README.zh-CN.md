# 桌面文件管理器

该程序根据文件类型或其他标准将用户桌面上的文件和文件夹分类到特定的子文件夹中。这是修改后的版本[凌乱的桌面清洁工](https://www.unknowncheats.me/forum/c-/578800-messy-desktop-cleaner.html)程序。

## 翻译

| 🇺🇸            | 🇨🇳                    | 🇹🇼                    | 🇮🇳                | 🇫🇷               | 🇦🇪                | 🇩🇪               | 🇯🇵                | 🇪🇸                 |
| --------------- | ----------------------- | ----------------------- | ------------------- | ------------------ | ------------------- | ------------------ | ------------------- | -------------------- |
| [英语](README.md) | [简体中文](README.zh-CN.md) | [繁体中文](README.zh-TW.md) | [印地语](README.hi.md) | [法语](README.fr.md) | [阿拉伯](README.ar.md) | [德语](README.de.md) | [日本人](README.ja.md) | [西班牙语](README.es.md) |

## 特征

-   根据文件类型（例如图像、音频、视频等）将文件分类到子文件夹中
-   读取配置文件 (config.json) 以确定哪些文件扩展名属于哪些类别
-   如果不存在则创建默认配置文件
-   将操作记录到日志文件 (log.txt) 以进行故障排除

## 用法

1.  下载程序并将其放在您选择的目录中。
2.  运行程序。它会自动对桌面上的文件和文件夹进行排序。
3.  （可选）修改 config.json 文件以自定义排序行为。

## 学分

这个程序是修改后的版本[凌乱的桌面清洁工](https://www.unknowncheats.me/forum/c-/578800-messy-desktop-cleaner.html)程序。代码被修改了[qzxtu](https://github.com/qzxtu).

## FAQ

**问：这个程序有什么作用？**

答：该程序根据文件类型或其他标准将用户桌面上的文件和文件夹分类到特定的子文件夹中。

**问：这个程序如何确定哪些文件进入哪些子文件夹？**

A: The program reads a configuration file (config.json) to determine which file extensions belong to which categories (e.g., images, audio, videos, etc.). You can modify this file to customize the sorting behavior.

**Q：config.json文件不存在怎么办？**

答：如果 config.json 文件不存在，程序将为每个类别创建一个具有预定义值的默认文件。

## 执照

这个项目是根据麻省理工学院许可证获得许可的。见[执照](LICENSE)文件的详细信息。
