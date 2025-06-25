using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GenreTimeRecommendations
{
    internal static readonly Dictionary<GameGenre, float> RecommendedDurations = new Dictionary<GameGenre, float>
    {
        { GameGenre.Fighting, 1.0f },
        { GameGenre.Action, 1.5f },
        { GameGenre.General, 2.5f },
        { GameGenre.RPG, 4.0f }
    };

    internal static GameGenre GetClosestGenre(float duration)
    {
        return RecommendedDurations
            .OrderBy(kvp => Mathf.Abs(kvp.Value - duration))
            .First()
            .Key;
    }

    internal static string GetLabel(GameGenre genre)
    {
        return genre switch
        {
            GameGenre.Fighting => "超高速（格闘ゲーム向け）",
            GameGenre.Action => "高速（アクション向け）",
            GameGenre.General => "標準（一般的）",
            GameGenre.RPG => "ドラマチック（RPG向け）",
            _ => "長すぎる（要調整）"
        };
    }

    internal static float GetRecommendedTime(GameGenre genre)
    {
        return RecommendedDurations.TryGetValue(genre, out var time) ? time : -1f;
    }
}
