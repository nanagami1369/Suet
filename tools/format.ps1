$ProjectRootDir = "$PSScriptRoot/../"
# プロジェクトのディレクトリへ移動
Push-Location $ProjectRootDir
# フォーマット
jb cleanupcode .\Suet.sln
# 元のディレクトリへ
Pop-Location
