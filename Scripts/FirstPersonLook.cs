using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
      
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); 
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

     
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
