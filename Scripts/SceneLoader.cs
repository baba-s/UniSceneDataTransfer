using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace KoganeUnityLib
{
	// シーンを読み込んだ時にデータを渡すことができるクラス
	public static class SceneLoader
	{
		// シーンにデータを渡してからシーンを読み込む
		public static void Load<TScene, TData>( TData entryData )
			where TScene : IScene<TData>
			where TData : class, new()
		{
			Load( typeof( TScene ), entryData );
		}
		
		// シーンにデータを渡してからシーンを読み込む
		public static void Load<TData>( Type type, TData entryData )
			where TData : class, new()
		{
			// SceneManager.sceneLoaded に登録するローカル関数
			void OnSceneLoaded( Scene scene, LoadSceneMode mode )
			{
				SceneManager.sceneLoaded -= OnSceneLoaded;

				// 読み込んだシーンから IScene を実装したコンポーネントを取得する
				var monoBehaviour = scene
						.GetRootGameObjects()
						.SelectMany( c => c.GetComponentsInChildren( type, true ) )
						.FirstOrDefault( c => c != null )
					;

				// 取得したコンポーネントを IScene 型にキャストして
				var sceneComponent = monoBehaviour as IScene<TData>;

				// シーンにデータを渡す
				sceneComponent.SetEntryData( entryData );
			}

			// シーンにデータを渡すローカル関数を登録しておく
			SceneManager.sceneLoaded += OnSceneLoaded;

			// シーンを読み込む
			var sceneName = type.Name;
			SceneManager.LoadScene( sceneName );
		}
	}
}