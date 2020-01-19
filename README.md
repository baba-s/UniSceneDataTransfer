[日本語の Readme はこちら](https://github.com/baba-s/unity-scene-data-transfer/blob/master/README_JP.md)  

# Unity Scene Data Transfer

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20200119/20200119140300.png)

Unity package to easily pass data at scene transition.  

![](https://img.shields.io/badge/Unity-2019.2%2B-red.svg)
![](https://img.shields.io/badge/.NET-4.x-orange.svg)
[![](https://img.shields.io/github/license/baba-s/unity-scene-data-transfer.svg)](https://github.com/baba-s/unity-scene-data-transfer/blob/master/LICENSE.md)

## Install

```json
"com.baba_s.unity-scene-data-transfer": "https://github.com/baba-s/unity-scene-data-transfer.git",
```

Add the above dependencies to manifest.json.  

## Usages

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20200119/20200119135440.png)

The name of the scene and the name of the component that controls the scene must be the same.  

```cs
public class ResultData
{
    public int Score;
    public int Rank;
}
```

Define the data class to be passed to the scene.  

```cs
using UnityEngine;

public class ResultScene : MonoBehaviour
{
```

MonoBehaviour to control the scene,   

```cs
using KoganeUnityLib;
using UnityEngine;

public class ResultScene : SimpleSceneBase<ResultScene, ResultData>
{
```

Change to inheritance SimpleSceneBase.  

- Add `using KoganeUnityLib;` .

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

Then, the passed data can be referenced by entryData property.  

```cs
var data = new ResultData
{
    Score = 5000,
    Rank  = 3,
};
ResultScene.Load( data );
```

After that, by writing the above code in another scene, you can transition the scene while passing data.  

## Remarks: Awake, OnEnable

```cs
public class ResultScene : SimpleSceneBase<ResultScene, ResultData>
{
    // Cannot
    private void Awake()
    {
        Debug.Log( entryData.Score );
        Debug.Log( entryData.Rank );
    }
    
    // Cannot
    private void OnEnable()
    {
        Debug.Log( entryData.Score );
        Debug.Log( entryData.Rank );
    }
```

- entryData property is not available for Awake, OnEnable.  

## Remarks: Launch directly

When launching the scene directly, entryData is null.  

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

Therefore, by writing the above code,  
When the scene is started directly, the dummy data can be used.  

```cs
using System;

[Serializable]
public class ResultData
{
    public int Score;
    public int Rank;
}
```

Alternatively, by applying the Serializable attribute to the data class,  

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20200119/20200119135443.png)

entryData used when launching the scene directly can be set in Unity Inspector.  