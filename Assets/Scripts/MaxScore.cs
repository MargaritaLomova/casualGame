using UnityEngine.UI;
using UnityEngine;
public class MaxScore : MonoBehaviour
{
    private void Start()
    {
        //Запись максимального рекорда игрока в меню
        GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
    }
}