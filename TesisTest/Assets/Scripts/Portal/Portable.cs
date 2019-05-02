using UnityEngine;
using UnityEditor;

public class Portable : MonoBehaviour
{
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;

    void Start()
    {
        fpsController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    public virtual void Teleport(Transform fromPortalTransform, Transform toPortalTransform)
    {
        Quaternion relativeDiff = toPortalTransform.rotation * Quaternion.Inverse(fromPortalTransform.rotation);
        relativeDiff *= Quaternion.Euler(0, 180, 0); 

        Vector3 positionOffset = transform.position - fromPortalTransform.position;
        positionOffset = relativeDiff * positionOffset;

        transform.position = toPortalTransform.position + positionOffset;

        if (fpsController != null)
        {
            fpsController.Rotate(relativeDiff);
        }
        else
        {
            gameObject.transform.rotation *= relativeDiff;
        }
        //EditorApplication.isPaused = true;

    }

}
