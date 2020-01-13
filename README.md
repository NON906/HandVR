# HandVR

6DoFやハンドトラッキングに対応した、AndroidスマホVR（cardboard）用のアセットおよびサンプルです。

ARM64でARCoreに対応したAndroid上で動作します。

バージョン2019.2.17f1のUnityで動作確認しています。

## アセットの使用方法

1. 「Window→Package Manager」から、以下をインストール（サンプルプロジェクト上では設定済みです）

- AR Foundation（preview.3 - 3.1.0）
- AR Subsystems（preview.2 - 3.1.0）
- ARCore XR Plugin（preview.2 - 3.1.0）
- Google VR Android（1.18.4）
- Input System（preview.3 - 1.0.0）
- XR Management（2.0.6）

2. 「File→Build Settings...→Player Settings...」から以下を設定

- Other Settings→Identification→Package Name：適当に変更
- Other Settings→Identification→Minimum API Level：Android 7.0 'Nougat' (API Level 24)
- Other Settings→Configuration→Scripting Backend：IL2CPP
- Other Settings→Configuration→Target Architectures：ARM64のみ
- Other Settings→Configuration→Scripting Define Symbols：UNITY\_INPUT\_SYSTEM
- XR Settings→Virtual Reality Supported：オン
- XR Settings→Virtual Reality SDKs：Cardboardを追加

3. 配置済みのメインカメラを削除し、「ARVRCamera」プレハブと「HandVR」プレハブをシーンに配置

4. 加えて「SphereHand」プレハブを使うことで簡単に手の表示を行うことができます
