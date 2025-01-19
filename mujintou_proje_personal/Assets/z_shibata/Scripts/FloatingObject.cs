using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ぷかぷかとオブジェクトを上下に揺らす
// ※落下するオブジェクトには非推奨
public class FloatingObject : MonoBehaviour
{
    [Header("ゆれの速度(負の値で逆回転)")]
    public float speedX = 2.0f;
    public float speedY = 2.0f;
    public float speedZ = 2.0f;

    [Header("ゆれの大きさ")]
    public float amplitudeX = 0.5f;
    public float amplitudeY = 0.5f;
    public float amplitudeZ = 0.5f;

    [Header("動く方向(XYで円運動)")]
    public bool movableX = false;
    public bool movableY = true;
    public bool movableZ = false;

    [Header("Rigidbody2D モード")]
    public bool isRigidbody2dMode;

    [Header("開始時ランダム位相を使うか")]
    public bool isRandomAmplitudePhase = true;

    private float difference;
    private float passingTime;
    private Rigidbody2D rb;

    void Start()
    {
        // 複数のオブジェクトが同時に動かないよう、違いをランダムで設定
        difference = Random.Range(0, 1.0f);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isRigidbody2dMode)
        {
            var move = GetFloatingVector3();
            // キャラのRotationが傾いても真上に動いてほしいので Space.World で
            transform.Translate(move * Time.deltaTime, Space.World);
        }
        
        //Time.timeを三角関数に与えると、リセットする度に挙動が変わった為
        passingTime += Time.deltaTime;
    }

    void FixedUpdate()
    {     
        if (isRigidbody2dMode)
        {
            var move = GetFloatingVector3();
            rb.velocity += (Vector2)move;
        }
    }

    private Vector3 GetFloatingVector3()
    {
        // Sin関数に増え続ける値を引数に与えると-1～+1の値が返ってくる
        // Time.time：シーン開始から経過した時間
        var dif = isRandomAmplitudePhase ? difference : 0;
        var x = Mathf.Cos(passingTime * speedX + dif) * amplitudeX;
        var y = Mathf.Sin(passingTime * speedY + dif) * amplitudeY;
        var z = Mathf.Cos(passingTime * speedZ + dif) * amplitudeZ;

        float moveX = movableX ? x : 0;
        float moveY = movableY ? y : 0;
        float moveZ = movableZ ? z : 0;

        return new Vector3(moveX, moveY, moveZ);
    }
}
