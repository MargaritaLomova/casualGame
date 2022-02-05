using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementController : MonoBehaviour
{
    private int advCount = 0;

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3459778", false);
        }
        else
        {
            Debug.LogError("Platform is not supported!");
        }
    }

    public void PlusAddCount()
    {
        advCount++;
        if (Advertisement.IsReady() && advCount % 3 == 0)
        {
            Advertisement.Show();
            advCount = 0;
        }
    }
}