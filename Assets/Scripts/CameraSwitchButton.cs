using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CameraFolloww cameraFolloww;
    public KeyCode switchKey = KeyCode.C; // Press 'C' to switch camera modes

    private void Start()
    {
        if (cameraFolloww == null)
        {
            cameraFolloww = FindAnyObjectByType<CameraFolloww>();

            if (cameraFolloww == null)
            {
                Debug.LogError("No CameraFollow script found in the scene!");
                return;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            cameraFolloww.ToggleCameraMode();
        }
    }
}
