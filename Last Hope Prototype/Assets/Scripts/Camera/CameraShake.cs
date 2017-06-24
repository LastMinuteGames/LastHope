using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class CameraShake : MonoBehaviour
{
    public float distanceShake = 40f;
    public bool done = false;

    private Camera cam;
    private bool shaking = false;
    private Vector3 offsetToPivot;
    private Transform camT;
    private Transform pivotT;
    private CameraCollision camCollision;

    PlayerIndex playerIndex;

    void Awake()
    {
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;
        offsetToPivot = camT.localPosition;
        camCollision = GetComponent<CameraCollision>();
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                playerIndex = testPlayerIndex;
            }
        }
    }

    public IEnumerator Shake(float duration = 0.2f, float magnitude = 0.5f, float xMultiplier = 1.0f, float yMultiplier = 1.0f, Transform positionCalled = null)
    {
        if (!shaking && CanShake(positionCalled))
        {
            GamePad.SetVibration(playerIndex, 1f, 1f);

            if (camCollision)
            {
                camCollision.enabled = false;
            }
            done = false;
            shaking = true;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float percentComplete = elapsed / duration;
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

                // map value to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                x *= magnitude * damper * xMultiplier;
                y *= magnitude * damper * yMultiplier;

                camT.localPosition = new Vector3(offsetToPivot.x + x, offsetToPivot.y + y, offsetToPivot.z);

                yield return null;
            }


            camT.localPosition = offsetToPivot;


            done = true;
            shaking = false;
            if (camCollision)
            {
                camCollision.enabled = true;
            }
            GamePad.SetVibration(playerIndex, 0f, 0f);

        }
    }

    public bool CanShake(Transform positionCalled)
    {
        if (!positionCalled)
        {
            return true;
        }
        else
        {
            float distance = Vector3.Distance(positionCalled.position, pivotT.position);
            if (distance <= distanceShake)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
