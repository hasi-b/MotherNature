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
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform circleUI;
    private Vector3 lastKnownPosition = Vector3.zero;
    [SerializeField] private float smoothSpeed = 0.1f; // Adjust this value to control the smoothness
    [SerializeField] private float movementThreshold = 0.1f;



    #region
    [SerializeField] GameObject motherNature;
    [SerializeField] Vector2 threshold;
    [SerializeField] float movementSpeed;
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (circleUI!= null && rightWristPosition!= null)
        {
            float screenX = rightWristPosition.x * Screen.width;
            float screenY = (1 - rightWristPosition.y) * Screen.height; // Invert Y for screen space
            float distance = Vector3.Distance(lastKnownPosition, new Vector3(screenX, screenY, 0f));

            // Only move the UI element if the distance is greater than the threshold
            if (distance > movementThreshold)
            {
                Vector3 targetPosition = new Vector3(screenX, screenY, 0f);

                // Apply smoothing to the target position (exponential smoothing)
                circleUI.position = Vector3.Lerp(circleUI.position, targetPosition, smoothSpeed);
                lastKnownPosition = targetPosition;

            }


            float mappedRotation = Mathf.Lerp(threshold.x, threshold.y, Mathf.InverseLerp(0f, Screen.width, screenX));

            // Rotate the target object smoothly (using Lerp to smooth the rotation)
            float currentRotationY = Mathf.LerpAngle(motherNature.transform.eulerAngles.y, mappedRotation, movementSpeed);
            motherNature.transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);



        }
    }

   
}
