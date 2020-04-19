using System;
using UnityEngine;

namespace UniSceneDataTransfer.Example
{
	// ステージセレクトシーンに渡すデータ
	[Serializable]
	public class StageSelectData
	{
		public string m_message;
	}
	
	// ステージセレクトシーンのコンポーネント
	public class StageSelectScene : SimpleSceneBase<StageSelectScene, StageSelectData>
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
				// タイトルシーンを読み込む
				var entryData = new TitleData
				{
					m_message = "ステージセレクトシーンからイトルシーンに遷移した", 
				};
				TitleScene.Load( entryData );
			}
		}
	}
}