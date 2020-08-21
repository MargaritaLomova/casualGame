using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class SphereController : MonoBehaviour
{
    /// <summary>  
    ///  Текст для очков
    /// </summary>  
    [SerializeField] GameObject scoreText;
    /// <summary>  
    ///  Объект размещающий чанки
    /// </summary> 
    [SerializeField] GameObject chunkPLacer;
    /// <summary>  
    ///  Переменная для скорости
    /// </summary> 
    [SerializeField] float speed;
    /// <summary> 
    /// Переменная устанавливающая высоту прыжка
    /// </summary> 
    [SerializeField] float jumpForce;

    /// <summary>  
    ///  Переменная хранящая степень ускорения
    /// </summary> 
    public int boost;
    /// <summary>
    /// Определяет мертв игрок или нет
    /// </summary>
    public bool isDead;
    /// <summary>  
    ///  Проверяет на земле сфера или нет
    /// </summary> 
    public bool stay = false;

    /// <summary>  
    ///  Счётчик смертей для проигрывания рекламы
    /// </summary> 
    private static int advcount = 0;
    /// <summary>  
    ///  Rigidbody сферы
    /// </summary> 
    private Rigidbody rigidbody;
    /// <summary>  
    ///  Текущая позиция сферы
    /// </summary> 
    private Vector3 currentPos;
    private void Start()
    {
        //Устанавливаем значение для переменной
        isDead = false;
        //Если возможно проигрывать рекламу на этом устройстве
        if (Advertisement.isSupported)
            //Иницилизация рекламной системы
            Advertisement.Initialize("3459778", false);
        //Если невозможно проигрывать рекламу на этом устройстве
        else
            //Сообщение об ошибке
            Debug.Log("Platform is not supported!");
        //Назначение переменной rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //Заморозка вращения сферы
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        //Взятие текущей позиции сферы
        StartCoroutine(GetVector());
        //Прыжок
        Jump();
        //Берём таймер из MenuButtons
        var timer = GameObject.Find("PauseMenuController").GetComponent<MenuButtons>().timer;
        //Увеличение уровня ускорения
        boost = (int)timer / 1000;
        //Если уровень ускорения меньше 1
        if(boost < 1)
            //Перемещение сферы без ускорения
            transform.Translate(Vector3.right * speed /3 * Time.fixedDeltaTime);
        //Если уровень ускорения больше 1
        else
            //Перемещение сферы с ускорением
            transform.Translate(Vector3.right * speed / 3 * boost * Time.fixedDeltaTime);
        //Если текущая позиция по Х равна 0
        if (currentPos.x == 0)
            currentPos.x = transform.position.x;
        //Если текущая позиция меньше чем та что была до этого и сфера на земле(сфера катится назад) 
        if (transform.position.x < currentPos.x && stay)
            Death();
        //Если игрок мертв и время не остановлено
        if (isDead && Time.timeScale != 0)
        {
            //Ускорение равно нулю
            boost = 0;
            //Остановить время
            Time.timeScale = 0;
        }
        //В другом случае
        else
            //Запустить время в обычном режиме
            Time.timeScale = 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //Если сфера столкнулась с полом
            case "floor":
                //Установить значение переменной как "сфера на земле"
                stay = true;
                break;
            //Если сфера столкнулась со смертельным коллайдером(на гранях платформ)
            case "deathCollider":
                //Устанавливаем значение переменной на "мёртв"
                isDead = true;
                //Запускаем метод смерти
                Death();
                break;
        }
    }
    /// <summary>
    /// Метод для осуществления прыжков
    /// </summary>
    private void Jump()
    {
#if UNITY_ANDROID || UNITY_IOS
        //Если игрок нажал на экран и сфера в этот момент на земле
        if ((Input.touchCount > 0) && stay)
        {
            //Переменная для хранения тапов
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //Если первое нажатие
                case TouchPhase.Began:
                    //Осуществление прыжка
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + Vector3.up * jumpForce;
                    //Сфера не на земле
                    stay = false;
                    break;
            }
        }
#endif
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        if (Input.GetKeyDown(KeyCode.Space) && stay)
        {
            //Осуществление прыжка
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + Vector3.up * jumpForce;
            //Сфера не на земле
            stay = false;
        }
#endif
    }
    /// <summary>
    /// Метод для узнавания текущей позиции раз в секунду
    /// </summary>
    private IEnumerator GetVector()
    {
        //Счетчик для задержки обновления позиции
        yield return new WaitForSeconds(1f);
        //Переменная для текущей позиции
        Vector3 nowPos = transform.position;
        //Передача позиции
        currentPos = nowPos;
    }
    /// <summary>
    /// Метод для осуществления смерти
    /// </summary>
    private void Death()
    {
        //Прибавление единицы к счетчику смертей
        advcount++;
        //Сфера не на земле(чтобы игрок не мог управлять сферой)
        stay = false;
        int score = Int32.Parse(GameObject.Find("PauseMenuController").GetComponent<MenuButtons>().scoreText.GetComponent<Text>().text);
        //Установка нового рекорда
        if (PlayerPrefs.GetInt("Score") < score)
            PlayerPrefs.SetFloat("Score", score);
        //Проигрыш рекламы если счетчик смертей равен 3 
        if (Advertisement.IsReady() && advcount % 3 == 0 && isDead)
            Advertisement.Show();
    }
}