# 入门阶段需要重点理解的内容

1. Unity 中相关基础概念
2. SUIFW UI框架的基本使用

# 学习流程

1. 下载并安装 Unity
   1. 访问 Unity 官网(https://unity.cn/releases) 下载Unity Hub (可以理解为Unity启动器)。
   2. 下载 Unity 2021.3.1f1c1，链接: https://unity.cn/releases/lts/2021 (下拉找到该本本并点击从 [从HUB下载] 即可在刚刚下载的 HUB 中下载并安装该版本 Unity)。
   3. 打开工程，使用 Unity 打开此链接 clone 出的文件夹，Unity 会自动配置项目，下载相应的包（初次加载可能需要较长时间）。
2. 了解 Unity 的基础知识，以下是一些参考链接：
   1. Unity 10分钟快速入门: 【Unity 10分钟快速入门 #U3D #Unity3D】 https://www.bilibili.com/video/BV1PL4y1e7hy/?share_source=copy_web&vd_source=3f353c941347237d6606524f347af76d
   2. Unity2022新手入门教程_超细节100集课程: https://www.bilibili.com/video/BV1TZ4y1o76s
   3. 游戏UI框架设计: https://www.cnblogs.com/LiuGuozhu/p/6416098.html (这个框架很重要，目前游戏中的UI都由该框架管理，可以通过这个框架方便地添加新界面，共有7篇，重点看前5篇即可)

b站上还有很多不错的资源，可以以【Unity 入门教程】或者【Unity + 具体的问题】多搜搜，挑播放量高的看。

# 目录结构

只需关注 Assets 目录下的内容。

## Animator

保存了一些动画信息，前期不必过度关注。

## Art

## Fonts

存放字体，不必关注。

## Prefabs

存放预制体，大致可以理解为一个拼装好的东西，拖入到场景中可以直接用，详情可以参考b站教程的相关章节。

## Resources

存放了一些资源，这个文件夹的内容可以在游戏运行时动态加载。

## Scenes

存储游戏中的所有场景。

## Script

存放游戏中的所有 C# 脚本。

C#脚本可以理解为，希望某个物体具有什么功能，就写一个相应的脚本，并挂到相应的物体上。

# SUIFW

UI 框架，使用了一个线程的框架，网址如下。

https://www.cnblogs.com/LiuGuozhu/p/6416098.html

共7篇参考博客。

## 创建UI大致流程

UI是使用了一个开源的 UI 框架实现。

创建一个新的UI预制体，然后放到 UIPrefab 目录中。具体地，在一个空的场景中，创建一个Canvas，然后在Canvas中创建一个Panel，这个Panel就是新UI的根物体。把这个Panel拖到 UIPrefab 中就可以了。

在 Resource/UIFormsConfigInfo.json 文件中添加新的UI预制体路径。

之后就可以在框架内调用了。

# 关于发布（前期不必关注）

## Windows

直接发布即可。

## MacOS

因为未知原因，在Windows上直接压缩，再在mac上解压会有奇怪的权限问题。

**Solution:**

必须先用U盘拷贝到一台mac上，在 mac 上压缩或者制作dmg文件后再通过网络传输，就不会出现这个问题。