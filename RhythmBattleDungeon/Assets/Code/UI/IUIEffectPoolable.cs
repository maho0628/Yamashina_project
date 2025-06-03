using UnityEngine;

public interface IUIEffectPoolable<T> where T : MonoBehaviour
{
    void OnCreated(UIObjectPool<T> pool);  // プール生成時
    void ReturnToPool();                   // エフェクト終了時に戻す
}
