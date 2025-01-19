using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionValueController : MonoBehaviour
{
    [SerializeField]
    GameObject ActionValueImagePrefab;
    [SerializeField]
    Sprite FilledImage;
    [SerializeField]
    Sprite EmptyImage;
    [SerializeField]
    int margin_width = 35;


    List<Image> ActionValueImageRefs = new List<Image>();
   
    void Start()
    {
        //バグが起きるときはPlayerInfoのStartGame()がまだ実行されていない段階でこの関数が実行されている可能性があります
        ActionValueImageRefs.Clear();
        for (int i = 0; i < PlayerInfo.Instance.MaxActionValue; i++)
        {
            var actionValue = Instantiate(ActionValueImagePrefab, transform);
            actionValue.transform.localPosition = new Vector3(i * margin_width, 0, 0);
            ActionValueImageRefs.Add(actionValue.GetComponent<Image>());
            ActionValueImageRefs[i].sprite = PlayerInfo.Instance.ActionValue > i ? FilledImage : EmptyImage;
        }

        PlayerInfo.Instance.OnActionValueChange.AddListener(() => OnActionValueChange());

        PlayerInfo.Instance.OnMaxActionValueChange.AddListener(() =>OnMaxActionValueChange());
    }

    void OnActionValueChange()
    {
        for (int i = 0; i < PlayerInfo.Instance.MaxActionValue; i++)
        {
            ActionValueImageRefs[i].sprite = PlayerInfo.Instance.ActionValue > i ? FilledImage : EmptyImage;
        }
    }
    
   void OnMaxActionValueChange()
    {
        for (int i = 0; i < ActionValueImageRefs.Count; i++)
        {
            Destroy(ActionValueImageRefs[i].gameObject);
            ActionValueImageRefs.Clear();
        }
        for (int i = 0; i < PlayerInfo.Instance.MaxActionValue; i++)
        {
            var actionValue = Instantiate(ActionValueImagePrefab, transform);
            actionValue.transform.localPosition = new Vector3(i * margin_width, 0, 0);
            ActionValueImageRefs.Add(actionValue.GetComponent<Image>());
            ActionValueImageRefs[i].sprite = PlayerInfo.Instance.ActionValue > i ? FilledImage : EmptyImage;
        }
    }

    private void OnDestroy()
    {
        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.OnActionValueChange.RemoveListener(OnActionValueChange);
            PlayerInfo.Instance.OnActionValueChange.RemoveListener(OnMaxActionValueChange);
        }
    }
}
