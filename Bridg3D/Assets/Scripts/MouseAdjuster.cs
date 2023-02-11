using UnityEngine;

public static class MouseAdjuster// : MonoBehaviour
{
    public static void SetState(CursorLockMode state)
    {
        if(Cursor.lockState == state){
            return;
        }
        Cursor.lockState = state;
    }
}
