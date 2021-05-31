using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGameCatalog : MonoBehaviour
{
    List<GameEntryData> gameEntryData = new List<GameEntryData>();
    List<Game> games = new List<Game>();

    #region Singleton
    public static MiniGameCatalog instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple MiniGameCatalog(s) found!");
            Destroy(gameObject);
        }

        LoadResources();
    }
    #endregion

    void LoadResources()
    {
        gameEntryData = Resources.LoadAll<GameEntryData>("Mini Games").OfType<GameEntryData>().ToList();

        foreach (GameEntryData gameEntry in gameEntryData)
        {
            games.Add(gameEntry.game);
        }
    }

    public Sprite FindIcon(string gameTitle)
    {
        try
        {
            return games.Find(r => r.title.Equals(gameTitle)).icon;
        }
        catch
        {
            return null;
        }
    }

    public List<GameEntryData> GetMiniGames()
    {
        return gameEntryData;
    }
}
