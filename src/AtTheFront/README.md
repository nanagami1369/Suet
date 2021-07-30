# AtTheFront

## 概要

フォーカスのあたっているウィンドウを最前面に固定するアプリ

すでにウィンドウが最前面に固定されている場合は解除する

## ビルド

### ビルドで使用したコマンド

* [md-to-pdf](https://github.com/simonhaenisch/md-to-pdf)

```bash
dotnet build
```

## 使い方

```text
オプション:
    /? -h --help ヘルプ
    -s /s [Key]スタンドアローンモード アプリ自体が常駐して
    [key]に指定したキーが入力された場合に最前面に表示します

例:
    AtTheFront.exe                 ...実行
    AtTheFront.exe -s Ctrl+Shift+K ...Ctrl+Shift+Kを同時に押すと実行
    AtTheFront.exe -s Ctrl+Alt+S   ...Ctrl+Alt+Sを同時に押すと実行
    AtTheFront.exe -s Shift+F11    ...Shift+F11を同時に押すと実行
```

## 使用したライブラリ

* NHotKey
  * Apache License Version 2.0, January 2004 <http://www.apache.org/licenses/>
* System.CommandLine
  * Copyright © .NET Foundation and Contributors All rights reserved.
  * [MIT License](https://github.com/dotnet/command-line-api/blob/main/LICENSE.md)
