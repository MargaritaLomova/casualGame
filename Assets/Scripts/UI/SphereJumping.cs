using UnityEngine;
public class SphereJumping : MonoBehaviour
{
    /// <summary>
    /// Rigidbody сферы
    /// </summary>
    private Rigidbody rb;
    /// <summary>
    /// Меню
    /// </summary>
    private GameObject menu;
    /// <summary>
    /// Переменная определяющая на земле ли сфера 
    /// </summary>
    private bool stay = false;
    private void Start()
    {
        //Сохраняем меню в переменную
        menu = GameObject.Find("MainMenu");
        //Сохраняем Rigidbody в переменную
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //Если сфера на земле
        if(stay)
        {
            //Подбрасываем сферу
            rb.velocity = GetComponent<Rigidbody>().velocity + Vector3.up * 8;
            //Сфера не на земле
            stay = false;
        }
        //Если меню не отображено
        if(menu.activeInHierarchy == false)
            //Двигаем сферу
            transform.Translate(Vector3.right * 10 / 3 * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Если сфера сталкивается с полом
        if(collision.gameObject.tag  == "floor")
            //Сфера на земле
            stay = true;
    }
}