using System;
using UnityEngine;

namespace UniSceneDataTransfer.Example
{
	// タイトルシーンに渡すデータ
	[Serializable]
	public class TitleData
	{
		public string m_message;
	}

	// タイトルシーンのコンポーネント
	public class TitleScene : SimpleSceneBase<TitleScene, TitleData>
	{
		private void Start()
		{
			// 他のシーンから渡されたデータを参照する
			// 他のシーンからデータが渡されていない場合は
			// Inspector で設定したデータが使用される
			// entryData は Awake や OnEnable では参照できないので注意
			Debug.Log( entryData.m_message );
		}

		private void Update()
		{
			// スペースキーが押されたら
			if ( Input.GetKeyDown( KeyCode.Space ) )
			{
				// ステージセレクトシーンを読み込む
				var entryData = new StageSelectData
				{
					m_message = "タイトルシーンからステージセレクトシーンに遷移した", 
				};
				StageSelectScene.Load( entryData );
			}
		}
	}
}