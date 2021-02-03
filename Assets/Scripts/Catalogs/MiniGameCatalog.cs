using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGameCatalog : MonoBehaviour
{
    List<MiniGame> miniGames = new List<MiniGame>();
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
        miniGames = Resources.LoadAll<MiniGame>("Mini Games").OfType<MiniGame>().ToList();

        foreach (MiniGame miniGame in miniGames)
        {
            List<Game> gs = miniGame.gameCategory.games;
            foreach (Game game in gs)
            {
                Game nextGame;
                try
                {
                    nextGame = gs[gs.IndexOf(game) + 1];
                }
                catch
                {
                    nextGame = null;
                }

                game.nextGame = nextGame;
                game.categoryIcon = miniGame.gameCategory.icon;
                game.categoryTitle = miniGame.gameCategory.title;

                games.Add(game);
            }
        }
    }

    public Sprite FindIcon(string gameTitle)
    {
        try
        {
            return games.Find(r => r.title.Equals(gameTitle)).categoryIcon;
        }
        catch
        {
            return null;
        }
    }

    public List<MiniGame> GetMiniGames()
    {
        return miniGames;
    }
}
