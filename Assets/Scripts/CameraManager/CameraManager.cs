using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; set; }

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform initialCamPos, swanCamPos, breadCamPos;

    [SerializeField] private float camSpeed;
    [SerializeField] private float zoomCamSpeed;
    private float currentCamSpeed;
    private float currentZoomAmount;


    private Camera cam;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        cam = cameraTransform.GetComponent<Camera>();
        currentZoomAmount = 6f;
    }

    private bool Distance(Vector3 positionOne, Vector3 positionTwo)
    {
        if(Vector3.Distance(positionOne, positionTwo) < 0.05f)
        {
            return true;
        }

        return false;
    }

    public IEnumerator CamBackToInitialPos()
    {
        while (!Distance(cameraTransform.position, initialCamPos.position))
        {
            yield return null;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, initialCamPos.position, camSpeed * Time.deltaTime);
        }

        yield return new WaitUntil(() => !Distance(cameraTransform.position, initialCamPos.position));
        Debug.Log("Looking at succesfully!");
    }

    public IEnumerator CamLookSwan()
    {
        while (!Distance(cameraTransform.position, swanCamPos.position))
        {
            yield return null;

            cameraTransform.position = Vector3.LerpUnclamped(cameraTransform.position, swanCamPos.position, camSpeed * Time.deltaTime);
            currentCamSpeed = Vector3.Distance(cameraTransform.position, swanCamPos.position);
        }

        yield return new WaitUntil(() => !Distance(cameraTransform.position, swanCamPos.position));
        Debug.Log("Looking at succesfully!");
    }

    public IEnumerator CamLookBread()
    {
        while (!Distance(cameraTransform.position, breadCamPos.position))
        {
            yield return null;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, breadCamPos.position, camSpeed * Time.deltaTime);
        }

        yield return new WaitUntil(() => !Distance(cameraTransform.position, breadCamPos.position));
    }
}
