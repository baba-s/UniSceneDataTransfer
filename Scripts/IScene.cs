namespace UniSceneDataTransfer
{
	// データの受け渡しが可能なシーンのインターフェイス
	public interface IScene<in TData> where TData : class, new()
	{
		// シーンにデータを渡す関数。SceneLoader でのみ使用する
		void SetEntryData( TData entryData );
	}
}