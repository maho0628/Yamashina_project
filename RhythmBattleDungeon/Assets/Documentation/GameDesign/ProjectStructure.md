## フォルダ構成

Assets/
├── Scripts/
│   ├── Rhythm/       ← リズム処理（譜面、判定）
│   ├── Battle/       ← 戦闘処理（攻撃、スキル、HP）
│   ├── UI/           ← UI（スコア、ゲージ表示など）
│   └── Common/       ← 汎用関数など

## 命名ルール
・スクリプト：

役割	命名例	説明
プレイヤー操作	HandlePlayerInput.cs	プレイヤーの入力処理を担当
ノーツ生成	SpawnNotes.cs	ノーツを生成
ノーツ移動	MoveNotes.cs	ノーツの動きを制御
譜面読み込み	LoadChart.cs	JSONから譜面データを読み込み
判定ロジック	JudgeInputTiming.cs	タイミング判定処理
攻撃処理	PerformAttack.cs	プレイヤーの攻撃処理
回避処理	PerformDodge.cs	プレイヤーの回避処理
スキル発動	ActivateSkill.cs	スキルを発動する処理
ゲーム開始	StartGame.cs	ゲーム開始時の処理
ゲーム終了	EndGame.cs	ゲーム終了時の処理
BGM管理	PlayMusic.cs	BGMを再生
アニメーション制御	PlayAnimation.cs	キャラやノーツのアニメ制御
シーン遷移　ChangeScene() – シーンを変更する

🎮 プレハブ名（動詞＋用途）
・プレハブ：
種類	命名例	説明
ノーツ	SpawnedNote_Attack	攻撃用の生成されたノーツ
ノーツ	SpawnedNote_Skill	スキル用の生成されたノーツ
敵キャラ	SpawningEnemy_Goblin	ゴブリン生成用のプレハブ
プレイヤー	PlayerCharacter_Fight	戦闘中のプレイヤーキャラ
攻撃エフェクト	Effect_AttackHit	攻撃ヒット時のエフェクト
音源管理　MusicManagerPrefab 　//複数の音源の切り替えを行うためのプレハブ（シングルトンで生成）
シーン遷移管理　SceneTransitionPrefab //シーン遷移を行うためのプレハブ（シングルトンで生成）

・関数名：
