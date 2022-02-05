using System.Collections;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private Rigidbody rb;

    [Header("Controllers"), SerializeField]
    private GameController gameController;
    [SerializeField]
    private InputController inputController;

    [Header("Variables"), SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private bool isStay = false;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "floor":
                isStay = true;
                break;
            case "deathCollider":
                isStay = false;
                gameController.Death();
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "floor":
                isStay = false;
                break;
        }
    }

    public void Move(Vector3 whereToMove)
    {
        transform.Translate(whereToMove * speed);

        //Jump
        if (inputController.isNeedToJump && isStay)
        {
            rb.velocity = rb.velocity + Vector3.up * jumpForce;

            isStay = false;

            rb.AddForce(whereToMove * speed);
        }

        StartCoroutine(PreventingBackMove());
    }

    private IEnumerator PreventingBackMove()
    {
        var previousX = transform.position.x;
        yield return new WaitForSeconds(0.1f);
        if(transform.position.x <= previousX)
        {
            transform.Translate(Vector3.right * speed);
        }
    }

    public bool IsStay()
    {
        return isStay;
    }
}