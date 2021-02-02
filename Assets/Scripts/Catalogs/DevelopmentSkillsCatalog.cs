using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DevelopmentSkillsCatalog : MonoBehaviour
{
    public GameObject developmentSkillPrefab;
    public List<DevelopmentSkillObject> developmentSkillObjects = new List<DevelopmentSkillObject>();
    public bool resourcedLoaded;

    #region Singleton
    public static DevelopmentSkillsCatalog instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple DevelopmentSkillsCatalog(s) found!");
            Destroy(gameObject);
        }

        LoadResources();
    }
    #endregion

    void LoadResources()
    {
        developmentSkillObjects = Resources.LoadAll<DevelopmentSkillObject>("Development Skills").OfType<DevelopmentSkillObject>().ToList();

        resourcedLoaded = true;
    }

    public DevelopmentSkillObject GetDevelopmentSkill(string devName)
    {
        return developmentSkillObjects.Find(r => r.name.Equals(devName));
    }
}
