using UnityEngine;

public class SceneControllerBase : MonoBehaviour
{
    [SerializeField] protected GameManager m_GameManager;

    protected virtual void Awake()
    {
        if (m_GameManager == null)
            m_GameManager = GameManager.Instance;
    }

    protected virtual void Start()
    {
        SetReactiveObservers();
    }

    protected virtual void SetReactiveObservers()
    {
    }
}
