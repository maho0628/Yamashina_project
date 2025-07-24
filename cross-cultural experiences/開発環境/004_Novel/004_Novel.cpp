// 004_Novel.cpp : アプリケーションのエントリ ポイントを定義します。
//

#include "framework.h"
#include<mmsystem.h>//サウンド関連のヘッダファイル
#include "004_Novel.h"

#include  "game.h"//ページデータの定義が書かれたヘッダファイル

#pragma comment(lib,"Msimg32.lib")//描画機能を使用するためのライブラリ
#pragma comment(lib,"winmm.lib")//サウンド関連の機能を使用するためのライブラリ

#define  SCREEN_WIDTH   (1280)//クライアント領域の幅(ピクセル)
#define SCREEN_HEIGHT (720)//クライアント領域の高さ(ピクセル)


#define MAX_LOADSTRING 100

//※他のcppファイルにあるグローバル変数を使えるようにexternで宣言します。

//ページデータ（構造体配列)
extern stPageData pageData[];
//全ページの数
extern int pageNum;
// グローバル変数:
HINSTANCE hInst;                                // 現在のインターフェイス
WCHAR szTitle[MAX_LOADSTRING];                  // タイトル バーのテキスト
WCHAR szWindowClass[MAX_LOADSTRING];            // メイン ウィンドウ クラス名

// このコード モジュールに含まれる関数の宣言を転送します:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: ここにコードを挿入してください。

	// グローバル文字列を初期化する
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_MY004NOVEL, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// アプリケーション初期化の実行:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_MY004NOVEL));

	MSG msg;

	// メイン メッセージ ループ:
	while (GetMessage(&msg, nullptr, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}



//
//  関数: MyRegisterClass()
//
//  目的: ウィンドウ クラスを登録します。
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_MY004NOVEL));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = NULL;//MAKEINTRESOURCEW(IDC_MY004NOVEL);//メニューバーをなし（NULL）にする
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassExW(&wcex);
}

//
//   関数: InitInstance(HINSTANCE, int)
//
//   目的: インスタンス ハンドルを保存して、メイン ウィンドウを作成します
//
//   コメント:
//
//        この関数で、グローバル変数でインスタンス ハンドルを保存し、
//        メイン プログラム ウィンドウを作成および表示します。
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // グローバル変数にインスタンス ハンドルを格納する
	//ウインドウの作成（最大化ボタン、最小化ボタンなし、サイズ変更不可
	HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPED | WS_SYSMENU,//WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

	if (!hWnd)
	{
		return FALSE;
	}

	//クライアント領域が希望のサイズになるようにウィンドウ全体のサイズを計算して再設定
	RECT rw, rc;
	GetWindowRect(hWnd, &rw);//ウィンドウ全体のサイズ
	GetClientRect(hWnd, &rc);//クライアント領域のサイズ

	//新しいウィンドウサイズを計算
	int new_width = (rw.right - rw.left) - (rc.right - rc.left) + SCREEN_WIDTH;
	int new_height = (rw.bottom - rw.top) - (rc.bottom - rc.top) + SCREEN_HEIGHT;

	//ウィンドウのサイズを設定
	SetWindowPos(hWnd, NULL, 0, 0, new_width, new_height, SWP_NOMOVE | SWP_NOZORDER);

	//画面のちらつきを防止するための機能(ダブルバッファリング)を有効にする
	SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_COMPOSITED);
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

//
//  関数: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  目的: メイン ウィンドウのメッセージを処理します。
//
//  WM_COMMAND  - アプリケーション メニューの処理
//  WM_PAINT    - メイン ウィンドウを描画する
//  WM_DESTROY  - 中止メッセージを表示して戻る
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	//変数の宣言
	static HBITMAP hBitmap = 0;    //ビットマップのハンドル(画像一枚につき一つ必要)
	static HBITMAP hBG[4] = { 0,0 ,0,0  };    //ビットマップのハンドル(背景画像) ※必要な数だけ配列にする
	static HBITMAP hMsgWnd = 0;    //ビットマップのハンドル(メッセージウィンドウ)
	static HDC     hMemDC = 0;     //メモリデバイスコンテキスト(描画に関する情報群)

	static HFONT  hFont = 0; //フォントのハンドル

	static BLENDFUNCTION blendFn; //半透明描画の情報
	static int   currentPage = 0;//現在表示しているページのID
	static int    mouseX = 0;// マウスカーソルの座標X
	static int    mouseY = 0;//マウスカーソルの座標Y
	static int    buttonCount = 0;//マウスの左ボタンを押した回数
	static int    frameCount = 0;//経過時間(フレーム)

	static MCI_OPEN_PARMS se;//SE(Sound Effect)用の変数(音声ファイルごとに一つ必要)
	switch (message)
	{
	case WM_CREATE: //  ウィンドウが作成される時に来るメッセージ(初期化処理)

		//画像(ビットマップファイル)を読み込む
		hBitmap = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/Foreign.bmp",//ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hBitmap == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}

		//画像(ビットマップファイル)を読み込む
		hBG[0] = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/OIP.bmp",   //ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hBG[0] == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}




		//画像(ビットマップファイル)を読み込む
		hMsgWnd = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/message_window.bmp",   //ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hMsgWnd == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}


		//画像(ビットマップファイル)を読み込む
		hBitmap = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/Foreign.bmp",//ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hBitmap == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}

		//画像(ビットマップファイル)を読み込む
		hBG[1] = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/OIP (1).bmp",//ップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hBG[1] == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}



		//画像(ビットマップファイル)を読み込む
		hMsgWnd = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/message_window.bmp",   //ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hMsgWnd == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}
		//画像(ビットマップファイル)を読み込む
		hBG[2] = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/NewZealand.bmp",//ップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hBG[1] == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}



		//画像(ビットマップファイル)を読み込む
		hMsgWnd = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/message_window.bmp",   //ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hMsgWnd == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}
		//画像(ビットマップファイル)を読み込む
		hBG[3] = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/Philippines.bmp",//ップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hBG[3] == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}



		//画像(ビットマップファイル)を読み込む
		hMsgWnd = (HBITMAP)LoadImage(
			NULL,                       //インスタンスのハンドル（NULL)
			L"data/image/message_window.bmp",   //ビットマップファイルのパス
			IMAGE_BITMAP,               //イメージのタイプ(Bitmap)
			0, 0,                        //読み込む画像の幅と高さ（0:画像ファイル自体のサイズ）
			LR_LOADFROMFILE             // 読み込み方法（ LR_LOADFROMFILE:ファイル指定）
		);

		//読み込みに失敗した時のエラー処理
		if (hMsgWnd == NULL)
		{
			//メッセージボックスを表示
			MessageBox(NULL, L"画像が読み込みませんでした", L"エラー", MB_OK);
			return 0;
		}

		//メモリデバイスコンテキストの作成
		hMemDC = CreateCompatibleDC(NULL);

		//半透明描画の情報の指定
		blendFn.BlendOp = AC_SRC_OVER;//カラーブレンドの操作(AC_SRC_OVER固定)
		blendFn.BlendFlags = 0;//フラグ(0固定)
		blendFn.AlphaFormat = 0;//フォーマット(AC_SRC_ALFA固定)
		blendFn.SourceConstantAlpha = 128;//アルファ値(0:完全透明～255:完全不透明)　◎

		//フォントの作成
		hFont = CreateFont(
			50,                     //フォントの高さ（大きさ）◎
			0,                       //フォントの幅(0;デフォルトの設定
			0,                       //角度
			0,                       //角度
			FW_DONTCARE,             //文字の太さ
			FALSE,                   //イタリック
			FALSE,                   //下線
			FALSE,                   //取り消し線
			SHIFTJIS_CHARSET,        //フォントの文字セット
			OUT_DEFAULT_PRECIS,      //出力精度の設定
			CLIP_DEFAULT_PRECIS,     //クリッピング精度
			DEFAULT_QUALITY,         //フォントの出力品質
			DEFAULT_PITCH,           //フォントのピッチとファミリ
			L"Meiryo"                //フォントのタイプフェイス名の指定(NULL:自動選択)
		);

		//フォントの作成に失敗した時のエラー処理
		if (hFont == NULL)
		{
			//1メッセージボックスを表示
			MessageBox(NULL, L"フォントが作成できませんでした", L"エラー", MB_OK);
			return 0;
		}
		//タイマーを作成し、定期的にWM_TIMERメッセージを発生させる
		SetTimer(
			hWnd,//ウィンドウハンドル
			1,//タイマーID(0以外の値を指定)
			50,//間隔(ミリ秒で指定）
			NULL//コールバック関数(NULL:なし)
		);
		//bgm用のmp3ファイルをオープンし、名前(alias)を"BGN"とする
		mciSendString(L"open data/bgm/0421For_the_first_time_in_forever.mp3 alias BGM", NULL, 0, hWnd);
		//名前(alias)を"BGM"と名付けたデータを再生(play)します。
		mciSendString(L"play BGM", NULL, 0, hWnd);

		//音量を設定する(0～1000,デフォルトは1000)
		//※setaudioの命令で"BGM"というデータのvolumeを200にする
		mciSendString(L"setaudio BGM volume to 250", NULL, 0, hWnd);

		//se用のwavファイルを指定して、再生の準備を行う
		se.lpstrDeviceType = (LPCWSTR)MCI_DEVTYPE_WAVEFORM_AUDIO;
		se.lpstrElementName = L"data/sound/cursor1.wav";
		mciSendCommand(NULL, MCI_OPEN, MCI_OPEN_TYPE | MCI_OPEN_TYPE_ID | MCI_OPEN_ELEMENT, (DWORD_PTR)&se);

		break;
	case WM_TIMER://セットされたタイマーにより、一定間隔で来るメッセージ
		//経過時間をカウントアップする
		frameCount++;

		WCHAR sMsg[256];//BGMの状態を受け取るための文字列
		//名前(alias)を"BGM"と名付けたデータの状態を問い合わせる
		mciSendString(L"status BGM mode", sMsg, 255, hWnd);
		//状態が停止(stopped)であるか文字列を比較する(再生終了していたら最初から再生させるループ処理)
		if (lstrcmp(sMsg, L"stopped") == 0)
		{
			//"BGM"の再生位置を先頭に戻す
			mciSendString(L"seek BGM to start", NULL, 0, hWnd);
			//"BGM"を現在の再生位置(先頭)から再生する
			mciSendString(L"play BGM notify", NULL, 0, hWnd);
		}
		//画面を更新して、新しいページを表示する
		InvalidateRect(hWnd, NULL, TRUE);//描画領域の更新(WM_PAINTメッセージを発行する)
		break;
	case WM_KEYDOWN:              //キーが押された時に来るメッセージ
		if (wParam == VK_ESCAPE)  //押されたキーがEscキーか調べる
		{
			DestroyWindow(hWnd);  // ウィンドウを終了するメッセージを発行する
		}
		if (pageData[currentPage].selectFlag == false) {
			//現在のページが通常ページのときの処理
			if (wParam == VK_SPACE)//押されたキーがSPACEか調べる
			{
				//ページごとに設定された次のページ番号を設定する( WM_LBUTTONDOWNと同じ処理)
				currentPage = pageData[currentPage].nextPage;
				//ページ番号が正しい範囲かチェックする
				if (currentPage < 0)
				{
					//ページ番号がゼロより小さければゼロにする(ゼロ以下のページは表示しない)
					currentPage = 0;
				}
				else if (currentPage >= pageNum)
				{
					//最終ページを超えていればIDを最終ページに修正する
					currentPage = pageNum - 1;
				}
				//画面を更新して、新しいページを表示する→タイマーでの自動更新を行うため削除
			   // InvalidateRect(hWnd, NULL,FALSE );//描画領域の更新(WM_PAINTメッセージを発行する)
			}
		}
		else
		{//現在のページが選択肢の時の処理
			if (wParam == VK_NUMPAD1 || wParam == '1')//押されたキーがSPACEか調べる
				//キーボードの1が押された(選択肢１が選択された)

				//選択肢１に設定された次のページ番号を設定する( WM_LBUTTONDOWNと同じ処理)
				currentPage = pageData[currentPage].jumpPage[0];
			//ページ番号が正しい範囲かチェックする
			if (currentPage < 0)
			{
				//ページ番号がゼロより小さければゼロにする(ゼロ以下のページは表示しない)
				currentPage = 0;
			}
			else if (currentPage >= pageNum)
			{
				//最終ページを超えていればIDを最終ページに修正する
				currentPage = pageNum - 1;

			}
			else if (wParam == VK_NUMPAD1 || wParam == '2')
				//キーボードの2が押された(選択肢2が選択された)

				//選択肢2に設定された次のページ番号を設定する( WM_LBUTTONDOWNと同じ処理)
				currentPage = pageData[currentPage].jumpPage[1];
			//ページ番号が正しい範囲かチェックする
			if (currentPage < 0)
			{
				//ページ番号がゼロより小さければゼロにする(ゼロ以下のページは表示しない)
				currentPage = 0;
			}
			else if (currentPage >= pageNum)
			{
				//最終ページを超えていればIDを最終ページに修正する
				currentPage = pageNum - 1;

			}
			else if (wParam == VK_NUMPAD1 || wParam == '3')
				//キーボードの2が押された(選択肢2が選択された)

				//選択肢2に設定された次のページ番号を設定する( WM_LBUTTONDOWNと同じ処理)
				currentPage = pageData[currentPage].jumpPage[2];
			//ページ番号が正しい範囲かチェックする
			if (currentPage < 0)
			{
				//ページ番号がゼロより小さければゼロにする(ゼロ以下のページは表示しない)
				currentPage = 0;
			}
			else if (currentPage >= pageNum)
			{
				//最終ページを超えていればIDを最終ページに修正する
				currentPage = pageNum - 1;

			}
			//SEを再生する
			mciSendCommand(se.wDeviceID, MCI_SEEK, MCI_SEEK_TO_START, 0);//再生位置を先頭にする
			mciSendCommand(se.wDeviceID, MCI_PLAY, 0, 0);//再生する

		}


		break;
	case WM_MOUSEMOVE:           //マウスが動いたときに来るメッセージ
		mouseX = LOWORD(lParam); //マウスカーソルのx座標を確保
		mouseY = HIWORD(lParam); //マウスカーソルのy座標を確保
		break;
	case WM_LBUTTONDOWN:        //マウスの左ボタンが押されたときに来るメッセージ
		buttonCount++;         //ボタンが押された回数をインクリメント(1増やす)
		if (pageData[currentPage].selectFlag == false)
		{   //現在のページが通常ページのときの処理
			 //ページが選択肢の場合は、キーボードのみの操作になるので、ここでは処理しない

			  //ページごとに設定された次のページ番号を設定する( WM_LBUTTONDOWNと同じ処理)

			currentPage = pageData[currentPage].nextPage;
			//ページ番号が正しい範囲かチェックする
			if (currentPage < 0)
			{
				//ページ番号がゼロより小さければゼロにする(ゼロ以下のページは表示しない)
				currentPage = 0;
			}
			else if (currentPage >= pageNum)
			{
				//最終ページを超えていればIDを最終ページに修正する
				currentPage = pageNum - 1;
			}
			//SEを再生する
			mciSendCommand(se.wDeviceID, MCI_SEEK, MCI_SEEK_TO_START, 0);//再生位置を先頭にする
			mciSendCommand(se.wDeviceID, MCI_PLAY, 0, 0);//再生する

		}
		break;
	case WM_PAINT://ウィンドウ (クライアント領域)を描画するときに来るメッセージ
	{
		//メッセージウインドウに表示する文字列（3行)
		WCHAR message1[] = L"メッセージウインドウ1行目";
		WCHAR message2[] = L"メッセージウインドウ2行目";
		WCHAR message3[] = L"メッセージウインドウ3行目";

		//表示する文字列(3行)
		WCHAR text1[] = L"テストメッセージ";
		WCHAR text2[256];
		WCHAR text3[256];
		WCHAR text4[256];
		WCHAR text5[256];


		//表示する文字列の作成
		//WCHARのStringに対してprintf()
		wsprintf(text2, L"マウスの左ボタンを押した回数:%d", buttonCount);
		wsprintf(text3, L"マウスカーソルの座標(%d,%d)", mouseX, mouseY);
		wsprintf(text4, L"経過時間(%dフレーム)", frameCount);
		wsprintf(text5, L"現在のページ：%d / %d)", currentPage, pageNum);

		PAINTSTRUCT ps;
		//描画の開始
		HDC hdc = BeginPaint(hWnd, &ps);
		// TODO: HDC を使用する描画コードをここに追加してください...

//現在のページ情報に設定されたIDの背景画像を選択
		int bgID = pageData[currentPage].bgID;
		SelectObject(hMemDC, hBG[bgID]);
		\
			//画像の描画(Bit-block transfer)
			BitBlt(
				hdc,//転送先のデバイスコンテキストへのハンドル
				0,//転送先の左上のx座標
				0,//転送先の左上のy座標
				1280,//転送元および転送先の長方形の幅
				720,//転送元および転送先の長方形の高さ
				hMemDC,//転送元のデバイスコンテキストへのハンドル
				0,//転送元の左上のx座標
				0,//転送元の左上のy座標
				SRCCOPY//カラーデータの結合方法(SRCCOPY:転送元を転送先に直接コピー)
			);

		//ビットマップの選択 (キャラクター画像)
		SelectObject(hMemDC, hBitmap);

		//透過色を指定して画像の描画
	TransparentBlt(
			hdc,//転送先のデバイスコンテキストへのハンドル
			880,//転送先の左上のx座標 ◎
			0,//転送先の左上のy座標　◎
			369,//転送先の長方形の幅
			275,//転送先の長方形の高さ
			hMemDC,//転送元のデバイスコンテキストへのハンドル
			0,//転送元の左上のx座標　0固定
			0,//転送元の左上のy座標　0固定
			369,
			275,
			(UINT)RGB(0,255,0)
		);

		//ビットマップの選択 (メッセージウインドウ)
		SelectObject(hMemDC, hMsgWnd);

		//半透明で画像の描画
		AlphaBlend(
			hdc,//転送先のデバイスコンテキストへのハンドル
			40,//転送先の左上のx座標
			480,//転送先の左上のy座標
			1200,//転送先の長方形の幅
			200,//転送先の長方形の高さ
			hMemDC,//転送元のデバイスコンテキストへのハンドル
			0,//転送元の左上のx座標　
			0,//転送元の左上のy座標
			1200,//転送先の長方形の幅
			200,//転送先の長方形の高さ
			blendFn //半透明描画の情報


		);
		//フォントの選択
		SelectObject(hdc, hFont);
		//背景モードの選択
		SetBkMode(hdc, OPAQUE);           //不透明
		//SetBkMode(hdc,TRANSPARENT);     //透明
		SetBkColor(hdc, RGB(10, 10, 10));         //文字の背景色を設定
		SetTextColor(hdc, RGB(240, 240, 240));  //文字の色を設定

		////文字列の描画(デバッグ情報/完成したら消す)
		//TextOut(
		//    hdc,//描画先のデバイスコンテキストへのハンドル
		//   0,//るX座標
		//    0,//画を始めるY座標
		//    text1,//描画する文字列
		//    lstrlen(text1)//描画する文字数
		//);//1行目
		//TextOut(hdc,0,60, text2, lstrlen(text2));//2行目
		//TextOut(hdc, 0,120, text3, lstrlen(text3));//3行目
		//TextOut(hdc, 0, 180, text4, lstrlen(text4));//4行目
		//TextOut(hdc, 0, 240, text5, lstrlen(text5));//5行目

		//メッセージウインドウへ文字列の描画
		SetBkMode(hdc, TRANSPARENT);//透明
		SetTextColor(hdc, RGB(0, 0, 0));//文字の色を設定
		TextOut(hdc, 100, 520, pageData[currentPage].text[0], lstrlen(pageData[currentPage].text[0]));//1行目
		TextOut(hdc, 100, 570, pageData[currentPage].text[1], lstrlen(pageData[currentPage].text[1]));//2行目
		TextOut(hdc, 100, 620, pageData[currentPage].text[2], lstrlen(pageData[currentPage].text[2]));//3行目

		//描画を終了
		EndPaint(hWnd, &ps);
	}
	break;
	case WM_DESTROY://ウィンドウが破棄されるときに来るメッセージ(終了処理)

		//ビットマップの削除
		DeleteObject(hBitmap);
		DeleteObject(hBG);
		DeleteObject(hBitmap);
		DeleteObject(hMsgWnd);


		//フォントの削除
		DeleteObject(hFont);

		//メモリデバイスコンテキストの解放
		DeleteDC(hMemDC);

		//設定したIDを持つタイマーの削除
		KillTimer(hWnd, 1);

		//"BGM" を停止
		mciSendString(L"stop BGM", NULL, 0, hWnd);
		//"BGM"のファイルを閉じる
		mciSendString(L"close BGM", NULL, 0, hWnd);

		//seのファイルを閉じる
		mciSendCommand(se.wDeviceID, MCI_CLOSE, 0, 0);


		//WM_QUITメッセージを発行する(プログラムを終了する)
		PostQuitMessage(0);

		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// バージョン情報ボックスのメッセージ ハンドラーです。
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}



