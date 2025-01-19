using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ItemPickupVisualizer : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            visualize_coroutines.Clear();
            if (is_running_item_visualizing)
            {
                StopCoroutine("MoveImage");
            }
        };
    }

    [SerializeField] private GameObject image_prefab;
    [SerializeField] private int visualize_canvas_order = 5;

    private bool is_running_item_visualizing = false;
    private Queue<IEnumerator> visualize_coroutines = new Queue<IEnumerator>();

    IEnumerator MoveImage(Items.Item_ID id, int num)
    {
        GameObject go = new GameObject("ItemVisualizeCanvas");
        Canvas c = go.AddComponent<Canvas>();
        go.AddComponent<CanvasRenderer>();
        go.AddComponent<CanvasScaler>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = 5;

        (GameObject, Image)[] objects = new (GameObject, Image)[num];
        Sprite sprite = SlotManager.GetItemData(id).icon;
        // -160  <<  320  width : 480 
        float height = num > 6 ? 480.0f / num - 1 : 80;
        for (int i = 0; i < num; i++)
        {
            objects[i].Item1 = Instantiate<GameObject>(image_prefab);
            objects[i].Item1.transform.SetParent(c.transform);
            objects[i].Item1.transform.SetAsFirstSibling();
            objects[i].Item1.transform.localPosition = new Vector3(550, -160 + height * i, 0);
            objects[i].Item2 = objects[i].Item1.GetComponent<Image>();
            objects[i].Item2.sprite = sprite;
        }

        yield return new WaitForSeconds(1.5f);

        int sliding_speed = 320;
        while (objects[num - 1].Item1 != null)
        {
            for (int i = 0; i < num; i++)
            {
                if (!objects[i].Item1) continue;
                objects[i].Item1.transform.Translate(0, -sliding_speed * Time.deltaTime, 0);
                var color = objects[i].Item2.color;
                color.a = Mathf.Clamp01((objects[i].Item1.transform.localPosition.y + 250) / 150);
                if (color.a == 0)
                {
                    Destroy(objects[i].Item1);
                    objects[i].Item1 = null;
                    continue;
                }
                objects[i].Item2.color = color;

            }

            yield return null;
        }
        if (c)
        {
            Destroy(c.gameObject);
        }
       


        if (visualize_coroutines.Count > 0)
        {
            StartCoroutine(visualize_coroutines.Dequeue());
        }
        else
        {
            is_running_item_visualizing = false;
        }
    }
    public void ItemViewVisualize(Items.Item_ID id, int num)
    {


        if (!is_running_item_visualizing)
        {
            StartCoroutine(MoveImage(id, num));
            is_running_item_visualizing = true;
        }
        else
        {
            visualize_coroutines.Enqueue(MoveImage(id, num));
        }



    }

}
