using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Create Item")]
public class Items : ScriptableObject
{
    //アイテムID
    public Item_ID item_ID = Item_ID.EmptyObject;
    //アイテム名
    public LocalizedString item_name = null;
    //アイテムのアイコン
    public Sprite icon = null;
    //使用できるか
    public bool canUse = false;
    //使用した時の効果
    public int Health_Change = 0;
    public int Hunger_Change = 0;
    public int Thirst_Chage = 0;
    public int Luck_Change = 0;
    //説明文
    public LocalizedString Discription = null;
    [Header("追加効果テキスト")]
    public LocalizedString extra_effect = null;
    [Header("料理か")]
    public bool isCooking = false;
    [Header("重複数"),Min(1)]
    public int overlap_num = 999;
    
    public enum Item_ID
    {
        EmptyObject,
        Butterfly,
        Fish,
        Leaf,
        Plank,
        item_mat_stone, item_mat_branch, item_mat_wood, item_mat_crab, item_mat_shell, item_mat_coconut, item_mat_salt, item_mat_bottle, item_mat_rope,
        item_mat_cloth, item_mat_fruit, item_mat_potato, item_mat_snake, item_mat_mash, item_mat_stMash, item_mat_ivy, item_mat_herb, item_mat_baby,
        item_mat_bigFish, item_mat_tinyFish, item_mat_egg, item_mat_tinyBird, item_mat_vegetable, item_mat_honey, item_mat_moss, item_mat_hawkEgg,
        item_mat_metal, item_mat_magma, item_craft_water, item_craft_seaSoup, item_craft_bakedFish, item_craft_bakedBigFish, item_craft_bakedShell,
        item_craft_bakedPotato, item_craft_honnyFish, item_craft_maxPotato, item_craft_bakedEgg, item_craft_boiledEgg, item_craft_proteinSalad,
        item_craft_salad, item_craft_bakedMash, item_craft_familySoup, item_craft_bakedBird, item_craft_superBakedEgg, item_craft_bakedSnake,
        item_craft_forestSalad, item_craft_bakedCrab, item_craft_coconutJuice, item_craft_islandFood, item_craft_torch, item_craft_DIYmedicine,
        item_craft_DIYknife, item_craft_DIYnet, item_craft_flag, item_craft_smoke, item_craft_raft, item_craft_onFireSet, item_special_knife,
        item_special_lighter, item_special_food, item_special_flash, item_special_book, item_special_medicine, item_craft_water2,

        Item_Max
    }

    
}
