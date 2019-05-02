using UnityEngine;
using System.Collections;

public class PortalCamera : MonoBehaviour
{

    public GameObject playerCamera;
    public GameObject portal;
    public GameObject otherPortal;
    public MeshRenderer renderPlane;
    public Shader portalShader;
    public RenderTexture portalRenderTexture;


    public Transform corner_TL;
    public Transform corner_TR;
    public Transform corner_BL;
    public Transform corner_BR;

    public Transform lookTarget;
    public bool drawNearCone;
    public bool drawFrustum;

    private Camera playerCam;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.gameObject;
            if (playerCamera == null)
            {
                Debug.LogError("Cannot find player camera, please set the MainCamera tag to the player's main camera and only in this");
            }
        }

        playerCam = GetComponent<Camera>();
        if (playerCam.targetTexture != null)
            playerCam.targetTexture.Release();


        RenderTexture portalRenderTex = new RenderTexture(portalRenderTexture);
        portalRenderTex.autoGenerateMips = false;

        playerCam.targetTexture = portalRenderTex;

        renderPlane.material = new Material(portalShader);
        renderPlane.material.mainTexture = playerCam.targetTexture;
    }


    void LateUpdate()
    {

        Vector3 portalPos = portal.transform.position;
        Vector3 otherPortalPos = otherPortal.transform.position;
        Vector3 playerCameraPos = playerCamera.transform.position;

        Quaternion relative = Quaternion.Inverse(otherPortal.transform.rotation) * portal.transform.rotation; 

        Vector3 playerOffsetFromPortal = playerCameraPos - otherPortalPos;
        playerOffsetFromPortal = relative * playerOffsetFromPortal;
        transform.position = portalPos + playerOffsetFromPortal;

        transform.rotation = otherPortal.transform.rotation;

        CullCameraFrustum();
    }

    void CullCameraFrustum()
    {


        Vector3 pa = corner_BL.position;
        Vector3 pb = corner_BR.position;
        Vector3 pc = corner_TL.position;
        Vector3 pd = corner_TR.position;

        Vector3 pe = playerCam.transform.position;

        Vector3 vr = (pb - pa).normalized; 
        Vector3 vu = (pc - pa).normalized; 
        Vector3 vn = Vector3.Cross(vr, vu).normalized;

        playerCam.transform.LookAt(playerCam.transform.position + vn);

        Vector3 va = pa - pe; 
        Vector3 vb = pb - pe; 
        Vector3 vc = pc - pe; 
        Vector3 vd = pd - pe; 


        Vector3 a_n = new Vector3(pe.x * vn.x, pe.y * vn.y, pe.z * vn.z);
        Vector3 b_n = new Vector3(lookTarget.transform.position.x * vn.x, lookTarget.transform.position.y * vn.y, lookTarget.transform.position.z * vn.z);
        float n = Vector3.Distance(a_n, b_n); 

        float f = playerCam.farClipPlane; 
        float d = Vector3.Dot(va, vn); 
        float l = Vector3.Dot(vr, va) * n / d; 
        float r = Vector3.Dot(vr, vb) * n / d; 
        float b = Vector3.Dot(vu, va) * n / d; 
        float t = Vector3.Dot(vu, vc) * n / d; 

        Matrix4x4 p = new Matrix4x4(); 

        p[0, 0] = 2.0f * n / (r - l);
        p[0, 2] = (r + l) / (r - l);
        p[1, 1] = 2.0f * n / (t - b);
        p[1, 2] = (t + b) / (t - b);
        p[2, 2] = (f + n) / (n - f);
        p[2, 3] = 2.0f * f * n / (n - f);
        p[3, 2] = -1.0f;

        playerCam.projectionMatrix = p; 

        if (drawNearCone)
        { 
            Debug.DrawRay(playerCam.transform.position, va, Color.blue);
            Debug.DrawRay(playerCam.transform.position, vb, Color.blue);
            Debug.DrawRay(playerCam.transform.position, vc, Color.blue);
            Debug.DrawRay(playerCam.transform.position, vd, Color.blue);
        }

        if (drawFrustum)
            DrawFrustum(playerCam); 
    }

    Vector3 ThreePlaneIntersection(Plane p1, Plane p2, Plane p3)
    { 
        return ((-p1.distance * Vector3.Cross(p2.normal, p3.normal)) +
            (-p2.distance * Vector3.Cross(p3.normal, p1.normal)) +
            (-p3.distance * Vector3.Cross(p1.normal, p2.normal))) /
            (Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal)));
    }

    void DrawFrustum(Camera cam)
    {
        Vector3[] nearCorners = new Vector3[4]; 
        Vector3[] farCorners = new Vector3[4]; 
        Plane[] camPlanes = GeometryUtility.CalculateFrustumPlanes(cam); 
        Plane temp = camPlanes[1]; camPlanes[1] = camPlanes[2]; camPlanes[2] = temp; 

        for (int i = 0; i < 4; i++)
        {
            nearCorners[i] = ThreePlaneIntersection(camPlanes[4], camPlanes[i], camPlanes[(i + 1) % 4]);
            farCorners[i] = ThreePlaneIntersection(camPlanes[5], camPlanes[i], camPlanes[(i + 1) % 4]);
        }

        for (int i = 0; i < 4; i++)
        {
            Debug.DrawLine(nearCorners[i], nearCorners[(i + 1) % 4], Color.red, Time.deltaTime, false);
            Debug.DrawLine(farCorners[i], farCorners[(i + 1) % 4], Color.red, Time.deltaTime, false);
            Debug.DrawLine(nearCorners[i], farCorners[i], Color.red, Time.deltaTime, false);
        }
    }


}
