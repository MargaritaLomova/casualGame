using UnityEngine;
public class BackGroundMusic : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<BackGroundMusic>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}