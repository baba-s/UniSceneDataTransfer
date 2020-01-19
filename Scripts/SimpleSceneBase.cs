using UnityEngine;

namespace KoganeUnityLib
{
	// データの受け渡しを可能にするシーンの基底クラス
	public abstract class SimpleSceneBase<TScene, TData> :
		MonoBehaviour,
		IScene<TData>
		where TScene : IScene<TData>
		where TData : class, new()
	{
		// 渡されたデータ
		[SerializeField] private TData m_entryData = new TData();

		// 渡されたデータを派生クラスで参照できるようにするプロパティ
		protected TData entryData => m_entryData;
		
		// シーンにデータを渡す関数。SceneLoader でのみ使用する
		public void SetEntryData( TData entryData )
		{
			m_entryData = entryData;
		}

		// シーン遷移のコードを簡潔に記述できる関数
		public static void Load( TData entryData )
		{
			SceneLoader.Load<TScene, TData>( entryData );
		}
	}
}