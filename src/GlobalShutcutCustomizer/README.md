# GlobalShutcutCustomizer

## 概要

任意のグローバルショートカットを追加できるアプリ

## ビルド

```bash
dotnet build
```

## 使い方

* `settings.xlsx.tmplate`を`settings.xlsx`にリネーム
* `settings.xlsx`にキーボードショートカットとアプリのパスを書いてGlobalShutcutCustomizerを起動する。

### 以下のルールにしたがって書く

| 名前 | 引数         | キー                       | パス         |
| ---- | ------------ | -------------------------- | ------------ |
| 名前 | アプリの引数 | 登録するショートカットキー | アプリのパス |


![設定の例](https://raw.githubusercontent.com/nanagami1369/Suet/App/1/GlobalShutcutCustomizer/src/GlobalShutcutCustomizer/readme-images/setting-example.png)
