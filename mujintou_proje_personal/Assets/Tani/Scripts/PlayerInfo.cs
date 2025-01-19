using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerInfo : SingletonMonoBehaviour<PlayerInfo>
{

    public (int day,bool isDayTime) Day
    {
        get
        {
            if (day % 2 == 0) return (day / 2, true);
            else return (day / 2, false);
        }

        private set
        {
            day = value.Item1;
        }


    }
    public Weather weather { get; set; }

    

    [SerializeField] private int _Max_Player_Health = 100;
    [SerializeField] private int _Max_Player_Hunger = 100;

    [SerializeField] private int _Max_Player_Thirst = 100;
    [SerializeField] private int _Max_Player_Luck = 100;

    [Space]
    [SerializeField] private int water_gain = 5;
    [SerializeField] private int fire_decrease = 5;

    [Space]
    [SerializeField] private List<TextureData> textureDatas;
    [SerializeField] private SlotManager inventry;
    [SerializeField] private bool Excute_StartGame_FuncHere = false;
    [SerializeField] SceneObject true_end;
    [SerializeField] SceneObject bad_end;



    private int _player_Health;
    private int _player_Hunger;
    private int _player_Thirst;
    private int _player_Luck;
    private int _player_current_action_value = 5;
    private int _player_max_action_value = 5;

    private uint _player_condition = 0;
    private int water_value = 0;
    private int fire_value = 0;
    private int day = 2;
    private List<Texture2D> cursor_textures;

    //坂本用
    private bool doukutuCheck = true;

    public UnityAction OnHealthSet { get;  set;}
    public UnityAction OnHungerSet {  get; set; }
    public UnityAction OnThirstSet {  get; set; }
    public UnityAction OnWaterSet {  get; set; }
    public UnityAction OnFireSet {  get; set; }


    public int Health
    {
        get { return _player_Health; }
        set
        {
            _player_Health = value;
            _player_Health = Mathf.Clamp(_player_Health,0, _Max_Player_Health);
            if(OnHealthSet != null)
            {
                OnHealthSet.Invoke();
            }
            
        }
    }
    

    public int Hunger
    {
        get { return _player_Hunger; }
        set
        {
            _player_Hunger = value;
            _player_Hunger = Mathf.Clamp(_player_Hunger, 0, _Max_Player_Hunger);
            OnHungerSet?.Invoke();
        }
    }

    public int Thirst
    {
        get { return _player_Thirst; }
        set
        {
            _player_Thirst = value;
            _player_Thirst = Mathf.Clamp(_player_Thirst, 0, _Max_Player_Thirst);
            OnThirstSet?.Invoke();
        }
    }
    public int Luck
    {
        get { return _player_Luck;}
        set
        {
            _player_Luck = value;
            _player_Luck = Mathf.Clamp(_player_Luck, 0, _Max_Player_Luck);
        }
    }

    public int Water
    {
        get { return water_value; }
        set
        {
            water_value = value;
            water_value = Mathf.Clamp(water_value, 0, 100);
            OnWaterSet?.Invoke();
        }
    }

    public int Fire
    {
        get { return fire_value; }
        set
        {
            fire_value = value;
            fire_value = Mathf.Clamp(fire_value, 0, 100);
            OnFireSet?.Invoke();
        }
    }

    public int StartArea { get; set; } = 0;

    public SlotManager Inventry => inventry;


    public int ActionValue
    {
        get
        {
            return _player_current_action_value;
        }
        set
        {
            _player_current_action_value = Mathf.Clamp(value,0,_player_max_action_value);
            OnActionValueChange.Invoke();
        }
    }

    public int MaxActionValue
    {
        get { return _player_max_action_value; }
        set
        {
            if(value > _player_current_action_value)
            {
                _player_max_action_value = value;
                OnMaxActionValueChange.Invoke();
            }
        }
    }

    public int FirstItemId { get; set; } = (int)Items.Item_ID.EmptyObject;
    public UnityEvent OnActionValueChange { get; } = new UnityEvent();

    public UnityEvent OnMaxActionValueChange { get; } = new UnityEvent();

    public enum Weather
    {
        Sunny,Cloudy,Rainy,
        Weather_Max
    }

    public void StartGame(bool override_current_data = false)
    {
        if (DataManager.DoesSaveExist())
        {
            
            print("save exist");
            if (override_current_data)
            {
                DataManager.Instance.InitializeSaveData();
            }
            LoadData();

        }
        else
        {
            print("save not exist");
            DataManager.Instance.Save(new SaveData());
            DataManager.Instance.InitializeSaveData();
            LoadData();
        }

        this.Inventry.SwitchVisible();
        this.inventry.SwitchVisible();


        //Inventry.GetItem(Items.Item_ID.item_craft_coconutJuice, 5);

    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        OnHungerSet += () =>
        {
            if(Hunger <= 20)
            {
                AddPlayerCondition(Condition.Hungry);
            }
            else
            {
                EraseCondition(Condition.Hungry);
            }
        };

        OnThirstSet += () =>
        {
            if (Thirst <= 20) AddPlayerCondition(Condition.Thirsty);
            else EraseCondition(Condition.Thirsty);
        };
    }

    private void Start()
    {
        if (Excute_StartGame_FuncHere)
        {
            StartGame(true);
        }
    

        cursor_textures = new List<Texture2D>();
        foreach (var n in textureDatas)
        {

            cursor_textures.Add(ResizeTexture(n.width, n.height, n.cursor));
            
        }
    }

    
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        SavePalyerData();
    //        SetInventryLock(true);
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {

    //        Health = 100;
    //        Hunger = 100;
    //        Thirst = 100;
    //        ResetCondition();
    //        SceneManager.LoadScene("TrueEndingScene");
    //    }
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {

    //        Health = 30;
    //        Hunger = 10;
    //        Thirst = 10;
    //        print((Items.Item_ID)FirstItemId);
            
    //    }
    //}

    #region Condition
    public enum Condition
    {
        Good, Poisoned, Thirsty, Hungry, ThirstyAndHungry, CONDIITON_MAX
    }


    public void AddPlayerCondition(Condition condition)
    {
        _player_condition |= (uint)1 << (int)condition;
    }

    public List<Condition> GetPlayerAllConditions()
    {
        List<Condition> conditions = new List<Condition>();
        for (int i = 0; i < (int)Condition.CONDIITON_MAX; i++)
        {
            if (IsPlayerConditionEqualTo((Condition)i))
            {
                conditions.Add((Condition)i);
            }
        }
        return conditions;
    }

    public bool IsPlayerConditionEqualTo(Condition condition)
    {
        return (_player_condition & ((uint)1 << (int)condition)) != 0;
    }

    public void ResetCondition()
    {
        _player_condition = 0;
    }

    public void EraseCondition(Condition condition)
    {
        _player_condition &= ~((uint)1 << (int)condition);
    }
    #endregion 

    public void DoAction()
    {
        
        Fire -= fire_decrease;

        Water += water_gain;
        Day = (day + 1,true);
        weather = (Weather)((int)Random.Range(0, (int)Weather.Weather_Max));
        if (Day.isDayTime)
        {
            SavePalyerData();
        }

    }

    public uint GetConditionRawData()
    {
        return _player_condition;
    }
    
    public float GetStatusPercent(int index)
    {
        switch (index)
        {
            case 0:
                return (float)Health / (float)_Max_Player_Health;
            case 1:
                return (float)Hunger / (float)_Max_Player_Hunger;
            case 2:
                return (float)Thirst / (float)_Max_Player_Thirst;
            default:
                return 0;
        }
    }


    static public Texture2D ResizeTexture(int new_x, int new_y, Texture2D src)
    {
        Texture2D resizedTexture = new Texture2D(new_x, new_y, TextureFormat.RGBA32, false);
        Graphics.ConvertTexture(src, resizedTexture);
        return resizedTexture;

    }

    public void SetMouseCursor(int? index)
    {
        if (index.HasValue)
        {
            if ((int)index >= 0 && (int)index < cursor_textures.Count)
            {
                Cursor.SetCursor(cursor_textures[(int)index], Vector2.zero, CursorMode.ForceSoftware);
            }
            else Debug.LogError("Index OutOfRange");

        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

  
    


    void LoadData()
    {
        SaveData data = DataManager.Instance.Load();
        _player_Health = data.player_health;
        _player_Hunger = data.player_hunger;
        _player_Thirst = data.player_thirst;
        _player_Luck = data.player_luck;
        _player_condition = data.player_condition;
        day = data.day;
        weather = (Weather)data.weather;
        water_value = data.water;
        fire_value = data.fire;
        ActionValue = data.action;
        MaxActionValue = data.max_action;
        FirstItemId = data.firstItemId;
       // UnityEngine.SceneManagement.SceneManager.LoadScene()
    }

    public void SavePalyerData()
    {
        SaveData saveData = new SaveData();
        saveData.MakeSaveData();
        Debug.Log($"プレイヤーデータが保存されました");
        DataManager.Instance.Save(saveData);
    }

    public void SetInventryLock(bool b)
    {
        foreach (GameObject item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            var inv_lock = item.GetComponentInChildren<Inv_lock>();
            if (inv_lock)
            {
                inv_lock.SetInventryLock(b);
            }
        }
    }


    public void OnHover(int i)
    {
        SetMouseCursor(i);
    }
    public void OnUnhover()
    {
        SetMouseCursor(null);
    }

    public void DestroySelf()
    {
        SavePalyerData();
        Destroy(gameObject);

    }

    public void CheckPlayerDeath()
    {
        if (Health == 0)
        {
            var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
            fade.Fade(Fading.type.FadeIn);
            SceneManager.LoadScene(bad_end);
            Inventry.SetVisible(false);
        }
    }

    public void OnVisibilityChanged()
    {
        SlotManager.selectedItem.slotManager = null;
    }

    public void SetdoukutuCheck(bool condition)
    {
        doukutuCheck = condition;
    }
    public bool GetdoukutuCheck()
    {
        return doukutuCheck;
    }
}

[System.Serializable]
public class TextureData
{
    public Texture2D cursor;
    public int width;
    public int height;
}

