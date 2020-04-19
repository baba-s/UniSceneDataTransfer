# Uni Scene Data Transfer

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20200119/20200119140300.png)

シーン遷移時に簡単にデータを渡すことができる機能

## 使い方

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20200119/20200119135440.png)

シーンの名前とシーンを制御するコンポーネントの名前は同じにしておきます  

```cs
public class ResultData
{
    public int Score;
    public int Rank;
}
```

まず、シーンに渡したいデータを管理するクラスを定義して、  

```cs
using UnityEngine;

public class ResultScene : MonoBehaviour
{
```

シーンを制御する MonoBehaviour のコンポーネントを  

```cs
using KoganeUnityLib;
using UnityEngine;

public class ResultScene : SimpleSceneBase<ResultScene, ResultData>
{
```

SimpleSceneBase を継承するように変更します  

- `using KoganeUnityLib;` を追加する必要があります  
- SimpleSceneBase の型パラメータには、シーンのクラスとデータのクラスを指定します  

```cs
using KoganeUnityLib;
using UnityEngine;

public class ResultScene : SimpleSceneBase<ResultScene, ResultData>
{
    private void Start()
    {
        Debug.Log( entryData.Score );
        Debug.Log( entryData.Rank );
    }
```

すると、entryData プロパティを使用して、他のシーンから渡されたデータを参照できるようになります  

```cs
var data = new ResultData
{
    Score = 5000,
    Rank  = 3,
};
ResultScene.Load( data );
```

あとは、他のシーンで上記のようなコードを記述することで、データを渡しながらシーン遷移できます  

## 補足：Awake や OnEnable では entryData を使用できない

```cs
public class ResultScene : SimpleSceneBase<ResultScene, ResultData>
{
    // ダメ
    private void Awake()
    {
        Debug.Log( entryData.Score );
        Debug.Log( entryData.Rank );
    }
    
    // ダメ
    private void OnEnable()
    {
        Debug.Log( entryData.Score );
        Debug.Log( entryData.Rank );
    }
```

- Awake や OnEnable で entryData プロパティを参照しても正常な値を取得できません  
- entryData プロパティは Start 以降で正しく参照することができます  

## 補足2：シーンを直接起動した時に entryData を使用する方法

シーンを直接起動した時は entryData は null になっています  

```cs
public class ResultScene : SimpleSceneBase<ResultScene, ResultData>
{
    private void Start()
    {
        var data = entryData ?? new ResultData();

        Debug.Log( data.Score );
        Debug.Log( data.Rank );
    }
```

そのため、上記のようなコードを記述することで、  
シーンを直接起動した時は初期値を使用する、といったことができるようになります  

```cs
using System;

[Serializable]
public class ResultData
{
    public int Score;
    public int Rank;
}
```

もしくは、シーンのデータを管理するクラスに Serializable 属性を適用することで  

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20200119/20200119135443.png)

シーンを直接起動した時の entryData のパラメータを Unity の Inspector で設定することができます  