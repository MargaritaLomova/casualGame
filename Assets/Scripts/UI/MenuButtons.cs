using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    /// <summary>
    /// Очки
    /// </summary>
    public GameObject scoreText;
    /// <summary>
    /// Таймер
    /// </summary>
    public float timer;

    /// <summary>
    /// Кнопка отключения звука
    /// </summary>
    private GameObject volumeButton;
    /// <summary>
    /// КНопка включения звука
    /// </summary>
    private GameObject antiVolumeButton;
    /// <summary>
    /// Меню паузы
    /// </summary>
    private GameObject pauseMenu;
    /// <summary>
    /// Меню смерти
    /// </summary>
    private GameObject deathMenu;
    /// <summary>
    /// Общий объект для кнопок включения/выключения звука
    /// </summary>
    private GameObject volumeGroup;
    /// <summary>
    /// GameObject меню
    /// </summary>
    private GameObject menu;
    /// <summary>
    /// Кнопка паузы
    /// </summary>
    private GameObject pauseButton;
    /// <summary>
    /// Звук нажатия кнопки
    /// </summary>
    private AudioClip buttonSound;
    /// <summary>
    /// Источник звука
    /// </summary>
    private AudioSource source;
    /// <summary>
    /// Переменняая для хранения индекса сцены
    /// </summary>
    private int sceneIndex;
    private void Start()
    {
        //Сохраняем индекс текущей сцены
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Сохраняем источник звука в переменную
        source = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        //Сохраняем зву нажатия кнопки в переменную
        buttonSound = Resources.Load("Sounds/buttonSound") as AudioClip;
        //Сохраняем кнопку отключения звука в переменную
        volumeButton = GameObject.Find("Volume");
        //Сохраняем кнопку включения звука в переменную
        antiVolumeButton = GameObject.Find("AntiVolume");
        //Если сейчас открыта сцена меню
        if (sceneIndex == 0)
            //Сохраняем меню в переменную
            menu = GameObject.Find("MainMenu");
        //Если открыта сцена игры
        else
        {
            //Сохраняем меню паузы в переменную
            pauseMenu = GameObject.Find("PauseMenu");
            //Скрываем меню паузы
            pauseMenu.SetActive(false);
            //Сохраняем меню смерти в переменную
            deathMenu = GameObject.Find("DeathScreen");
            //Скрываем меню смерти
            deathMenu.SetActive(false);
            //Сохраняем кнопку паузы в переменную
            pauseButton = GameObject.Find("Pause");
            //Сохраняем текстовое отображения очков в переменную
            scoreText = GameObject.Find("ScoreText");
            //Сохраняем общую переменную для кнопок звука в переменную
            volumeGroup = GameObject.Find("VolumeGroup");
            //Скрываем кнопки звука
            volumeGroup.SetActive(false);
        }
    }
    private void Update()
    {
        //Если открыто меню или открыта игра и кнопки включения/выключения звука отображены
        if (sceneIndex == 0 || sceneIndex == 1 && volumeGroup.activeInHierarchy)
            SwitchVolume();
        //Если открыта сцена игры
        if (sceneIndex == 1)
        {
            ShowDeathMenu(GameObject.Find("Sphere").GetComponent<SphereController>().isDead);
            //Если время запущено
            if (Time.timeScale == 1)
            {
                //Увеличение таймера
                timer += 0.02f;
                //Формирование переменной очков
                var score = (int)timer;
                //Вывод очков на экран
                scoreText.GetComponent<Text>().text = score.ToString();
            }
        }
    }
    /// <summary>
    /// Метод смены состояния звука
    /// </summary>
    private void SwitchVolume()
    {
        //Если звук отключен
        if (AudioListener.volume == 0)
        {
            //Показать кнопку включения звука
            antiVolumeButton.SetActive(true);
            //Скрыть кнопку отключения звука
            volumeButton.SetActive(false);
        }
        //Если звук включен
        else if (AudioListener.volume == 1)
        {
            //Скрыть кнопку включения звука
            antiVolumeButton.SetActive(false);
            //ПОказать кнопку отключения звука
            volumeButton.SetActive(true);
        }
    }
    /// <summary>
    /// Метод для загрузки игры
    /// </summary>
    private void LoadGame()
    {
        //Загрузить сцену с игрой
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Метод открывающий меню смерти
    /// </summary>
    private void ShowDeathMenu(bool isPlayerDead)
    {
        if(isPlayerDead)
        {
            //Скрываем кнопку паузы
            pauseButton.SetActive(false);
            //Вывод меню смерти на экран
            deathMenu.SetActive(true);
        }
    }
    /// <summary>
    /// Метод выключения звука
    /// </summary>
    public void VolumePressed()
    {
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
        //Выключить звук
        AudioListener.volume = 0;
    }
    /// <summary>
    /// Метод включения звука
    /// </summary>
    public void AntiVolumePressed()
    {
        //Включить звук
        AudioListener.volume = 1;
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
    }
    /// <summary>
    /// Метод для загрузки игры
    /// </summary>
    public void PlayPressed()
    {
        //Скрыть меню
        menu.SetActive(false);
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
        //Загрузить игру через 2 секунды
        Invoke("LoadGame", 2f);
    }
    /// <summary>
    /// Метод для выхода
    /// </summary>
    public void ExitPressed()
    {
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
        //Выйти из игры
        Application.Quit();
        //(Для editor-а)Вывести в консоль сообщение о том, что игрок вышел из игры 
        Debug.Log("Exit pressed!");
    }
    /// <summary>
    /// Метод нажатия кнопки паузы
    /// </summary>
    public void Pause()
    {
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
        //Если меню паузы не открыто
        if (!pauseMenu.activeInHierarchy)
        {
            //Остановить игровое время
            Time.timeScale = 0;
            //Открыть меню паузы
            pauseMenu.SetActive(true);
            //Скрыть очки
            scoreText.SetActive(false);
            //Показать кнопки включения/выключения звука
            volumeGroup.SetActive(true);
        }
        //Если меню паузы было открыто
        else
        {
            //Запустить игровое время
            Time.timeScale = 1;
            //Скрыть меню паузы
            pauseMenu.SetActive(false);
            //Показать очки
            scoreText.SetActive(true);
            //Скрыть кнопки включения/выключения звука
            volumeGroup.SetActive(false);
        }
    }
    /// <summary>
    /// Метод для рестарта уровня
    /// </summary>
    public void Retry()
    {
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
        //Если время было остановлено
        if (Time.timeScale == 0)
            //Запустить время
            Time.timeScale = 1;
        //Загрузить сцену игры
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Метод возвращающий в меню
    /// </summary>
    public void BackToMainMenu()
    {
        //Проиграть звук нажатия кнопки
        source.PlayOneShot(buttonSound);
        //Если время остановлено
        if (Time.timeScale == 0)
            //Запустить время
            Time.timeScale = 1;
        //Загрузить сцену меню
        SceneManager.LoadScene(0);
    }
}