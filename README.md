# ransomware-poc

簡易的勒索軟體，丟virustotal大概個位數檢測出來，360掃毒、火絨離線版都有過(都沒有加殼)

有些加了殼，防毒脫不掉，直接判你有毒

## 環境

- win7
- visual stuido C# .net 4

## 流程

- 把 aes 和 rsa 的 key 共三支丟到 gas 上
- 受害端只保留 rsa 公鑰(我沒做aes key用rsa 加密，因為懶惰)
- 利用 rsa 公鑰作為唯一識別
- 被加密後的檔案結尾會加上aes key的md5，避免檔案亂用key解密被解爛

## 發現

如果有用 mutex，會比較容易把你判斷成有惡意

[link1](https://www.virustotal.com/gui/file/9129230f7c08fed30e23ed04a75208940b3511781889c85d9a4678ce4a94be1f/behavior/VirusTotal%20ZenBox)

[link2](https://www.virustotal.com/gui/file/9f3dff202d376013c6c1f2e012174c37cc59a671d2f34b6d03a2baf37d7906e7/behavior/VirusTotal%20Jujubox)

如果有用到 `System.Diagnostics.Process.Start(url)` ，同樣也會被判成惡意

[link1](https://www.virustotal.com/gui/file/714292fdb38b70ae0a32de64d106bfb1f50e8915ccac2224eed7a2066bbc6e14/detection)

[link2](https://www.virustotal.com/gui/file/793ce3f5f81c80baccca0a919f8c17fa0fe3263ce78d8e77f14431ad7d036447/detection)



Ref:

https://github.com/aaaddress1/my-Little-Ransomware

https://www.slideshare.net/MaHauo/ss-59732934