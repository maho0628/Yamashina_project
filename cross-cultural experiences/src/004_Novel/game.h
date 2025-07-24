#pragma once// ヘッダファイルの１行目に書いておくプリプロセッサ(インクルードガード)

#define LINE_NUM (3)// 1ページ当たりに表示できる行数の定義

//1ページ当たりの情報の定義(構造体)
struct stPageData
{
	WCHAR text[LINE_NUM][256];//3行分のテキストデータ(各行64文字まで)
	int nextPage;//次に表示するページの番号(このページが選択肢でない場合)
	int bgID;//このページで表示する背景画像のID
	int selectFlag;// このページが選択肢か示すフラグ
	int jumpPage[LINE_NUM];//このページが選択肢であった場合の、各選択肢ごとの次のページ番号
};