using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool isNeedToJump { get; private set; }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isNeedToJump = true;
                    break;
            }
        }
#elif UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            isNeedToJump = true;
        }
#endif
        else
        {
            isNeedToJump = false;
        }
    }
}