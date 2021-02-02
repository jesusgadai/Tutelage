using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGameCatalog : MonoBehaviour
{
    List<MiniGame> miniGames = new List<MiniGame>();

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
        miniGames = Resources.LoadAll<MiniGame>("Mini Games").OfType<MiniGame>().ToList();
    }

    public List<MiniGame> GetMiniGames()
    {
        return miniGames;
    }
}
