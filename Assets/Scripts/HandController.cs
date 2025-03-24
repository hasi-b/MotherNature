using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mediapipe;

public class HandController : MonoBehaviour
{
    public static HandController Instance { get; private set; }
    public Vector3 RightWristPosition { get => rightWristPosition; set => rightWristPosition = value; }

    private Vector3 rightWristPosition;

    [SerializeField] GameObject ObjectToMove;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectToMove!= null && rightWristPosition!= null)
        {
            float screenX = rightWristPosition.x * Screen.width;
            float screenY = (1 - rightWristPosition.y) * Screen.height; // Invert Y for screen space

            // Convert screen coordinates to world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, Camera.main.nearClipPlane + 5f));

            // Move the object based on the right wrist position
            ObjectToMove.transform.position = Vector3.Lerp(ObjectToMove.transform.position, worldPosition, Time.deltaTime * 5f);

        }
    }

    public void MoveHand(float screenX,float screenY,GameObject objectToMove)
    {
        // Convert screen coordinates to world position (assuming a 2D camera)
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, Camera.main.nearClipPlane + 5f));

        // Move the object
        if (objectToMove != null)
        {
            objectToMove.transform.position = worldPosition;
        }
    }
}
