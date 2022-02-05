using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [Header("Objects From Scene"), SerializeField]
    private SphereController sphere;

    private void Start()
    {
        StartCoroutine(SmoothlyShowSphere());
    }

    private IEnumerator SmoothlyShowSphere()
    {
        while(transform.position.x != sphere.transform.position.x + 1 || transform.position.y != sphere.transform.position.x + 0.2f)
        {
            if(transform.position.x != sphere.transform.position.x + 1)
            {
                transform.position = new Vector3(sphere.transform.position.x + 1, transform.position.y, transform.position.z);
            }

            if(transform.position.y != sphere.transform.position.x + 0.2f)
            {
                transform.position = new Vector3(transform.position.x, sphere.transform.position.y + 0.2f, transform.position.z);
            }

            yield return null;
        }
    }
}