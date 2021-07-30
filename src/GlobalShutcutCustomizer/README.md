# GlobalShutcutCustomizer

## 概要

任意のグローバルショートカットを追加できるアプリ

## ビルド

### ビルドで使用したコマンド

* [md-to-pdf](https://github.com/simonhaenisch/md-to-pdf)

```bash
dotnet build
```

## 使い方

* `settings.xlsx.template`を`settings.xlsx`にリネーム
* `settings.xlsx`にキーボードショートカットとアプリのパスを書いてGlobalShutcutCustomizerを起動する。

### 以下のルールにしたがって書く

| 名前 | 引数         | キー                       | パス         |
| ---- | ------------ | -------------------------- | ------------ |
| 名前 | アプリの引数 | 登録するショートカットキー | アプリのパス |

![設定の例](https://raw.githubusercontent.com/nanagami1369/Suet/App/1/GlobalShutcutCustomizer/src/GlobalShutcutCustomizer/readme-images/setting-example.png)

## 使用したライブラリ

* NHotKey
  * Apache License Version 2.0, January 2004 <http://www.apache.org/licenses/>
* ExcelDataReader
  * Copyright (c) 2014 ExcelDataReader
  * [MIT License](https://github.com/ExcelDataReader/ExcelDataReader/blob/develop/LICENSE)
