# HandVR

6DoFやハンドトラッキングに対応した、スマホVR（cardboard）用のアセットおよびサンプルです。

ARM64でARCoreに対応したAndroid上やARKitに対応したiOS上で動作します。

バージョン2019.2.17f1のUnityで動作確認しています。

unitypackageファイルは[こちら](https://github.com/NON906/HandVR/releases)からダウンロードできます。

## アセットの使用方法

1. 「Window→Package Manager」から、以下をインストール（サンプルプロジェクト上では設定済みです）

共通

- AR Foundation（preview.3 - 3.1.0）
- AR Subsystems（preview.2 - 3.1.0）
- Input System（preview.3 - 1.0.0）
- XR Management（2.0.6）

Android

- ARCore XR Plugin（preview.2 - 3.1.0）
- Google VR Android（1.18.4）

iOS

- ARKit XR Plugin（preview.2 - 3.1.0）
- Google VR iOS（1.18.5）

2. 「File→Build Settings...→Player Settings...」から以下を設定

Android

- Other Settings→Identification→Package Name：適当に変更
- Other Settings→Identification→Minimum API Level：Android 7.0 'Nougat' (API Level 24)
- Other Settings→Configuration→Scripting Backend：IL2CPP
- Other Settings→Configuration→Target Architectures：ARM64のみ
- Other Settings→Configuration→Scripting Define Symbols：UNITY\_INPUT\_SYSTEM
- XR Settings→Virtual Reality Supported：オン
- XR Settings→Virtual Reality SDKs：Cardboardを追加

iOS

- Other Settings→Identification→Bundle Identifer：適当に変更
- Other Settings→Configuration→Target minimum iOS Version：11.0
- Other Settings→Configuration→Architecture：ARM64
- Other Settings→Configuration→Scripting Define Symbols：UNITY\_INPUT\_SYSTEM
- XR Settings→Virtual Reality Supported：オン
- XR Settings→Virtual Reality SDKs：Cardboardを追加

3. 配置済みのメインカメラを削除し、「ARVRCamera」プレハブと「HandVR」プレハブをシーンに配置

4. 加えて「SphereHand」プレハブを使うことで簡単に手の表示を行うことができます

5. （iOSのみ）プロジェクトのビルド後、「Unity-iPhone.xcworkspace」を開き、XCodeから以下を設定してアプリのビルドを実行

- Unity-iPhoneをクリックすると出てくる設定画面のGeneral→Frameworks, Libraries, and Embedded Contentから「MultiHandAppLib-fl.a」を削除
- 同画面のBuild Settings→Linking→Other Linker Flagsに``-force_load`` ``Libraries/HandVR/HandVR/Plugins/iOS/MultiHandAppLib-fl.a``の2つを追加
- Unity-iPhoneプロジェクトのData/Rawディレクトリにある``hand_landmark.tflite`` ``multihandtrackinggpu.binarypb`` ``palm_detection_labelmap.txt`` ``palm_detection.tflite``の4つをUnity-iPhone直下にドラッグアンドドロップし、初期設定のままFinishをクリック
