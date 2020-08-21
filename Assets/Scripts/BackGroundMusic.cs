using UnityEngine;
public class BackGroundMusic : MonoBehaviour
{
    private void Awake()
    {
        //Сохраняем количество объектов BackGroundMusic на сцене
        int numMusic = FindObjectsOfType<BackGroundMusic>().Length;
        //Если количество больше 1
        if (numMusic > 1)
            //Уничтожаем текущий объект
            Destroy(gameObject);
        //Не уничтожаем текущий объект при перезагрузке сценны
        DontDestroyOnLoad(gameObject);
    }
}