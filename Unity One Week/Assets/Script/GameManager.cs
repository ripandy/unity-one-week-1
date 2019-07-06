using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "GameManager", order = 0)]
public class GameManager : ScriptableObject
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
                instance = Resources.Load<GameManager>(typeof(GameManager).ToString());
            if (!instance)
            {
                var gm = Resources.FindObjectsOfTypeAll<GameManager>();
                if (gm != null && gm.Length > 0)
                    instance = Resources.FindObjectsOfTypeAll<GameManager>()[0];
            }
            if (!instance)
                instance = CreateInstance<GameManager>();
            return instance;
        }
    }
}
