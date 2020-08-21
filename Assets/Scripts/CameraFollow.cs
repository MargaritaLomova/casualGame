using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Сфера как объект
    /// </summary>
    private GameObject sphere;
    /// <summary>
    /// Скрипт сферы
    /// </summary>
    private SphereController sphereScript;
    private void Start()
    {
        //Находим сферу и записываем её в переменную
        sphere = GameObject.Find("Sphere");
        //Находим скрипт сферы и сохраняем в переменную
        sphereScript = sphere.GetComponent<SphereController>();
    }
    private void FixedUpdate()
    {
        //ЗАставляем камеру следить за сферой по оси х
        transform.position = new Vector3(sphere.transform.position.x + 1, transform.position.y, transform.position.z);
        //Если сфера на платформе
        if (sphereScript.stay == true)
            //Задаём положение камеры по оси у
            transform.position = new Vector3(transform.position.x, sphere.transform.position.y + 0.7f, transform.position.z);     
    }
}