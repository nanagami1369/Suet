# VolumeCommandManager

## 概要

システムのボリュームを調整します

## ビルド

### ビルドで使用したコマンド

* [md-to-pdf](https://github.com/simonhaenisch/md-to-pdf)

```bash
dotnet build
```

## 使い方

```text
    VolumeCommandManager [args] [option]

引数
    -m -mode <Mute,Up,Down> どの操作をするのかを指定します

オプション:
    /? -? -h --help   ヘルプ

例:
    VolumeCommandManager -m Mute         ...ミュートにする。すでにミュートなら解除する
    VolumeCommandManager -m Up           ...音量を上げる
    VolumeCommandManager -m Down         ...音量を下げる
```

## 使用したライブラリ

* System.CommandLine
  * Copyright © .NET Foundation and Contributors All rights reserved.
  * [MIT License](https://github.com/dotnet/command-line-api/blob/main/LICENSE.md)
* NAudio.Wasapi
  * Copyright 2020 Mark Heath
  * [MIT License](https://github.com/naudio/NAudio/blob/master/license.txt)
