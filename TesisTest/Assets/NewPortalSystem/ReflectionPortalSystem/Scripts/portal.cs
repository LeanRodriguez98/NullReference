using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode] 
public class portal : MonoBehaviour
{

	[Serializable]
	public class AdvancedSettings
	{
		public bool teleport = true;
		public int textureSize = 2048;
	}
    public GameObject partner;
    public AdvancedSettings advancedSettings;


    private bool disablePixelLights = false;
	private float clipPlaneOffset = 0f;//0.07f;
	private Hashtable reflectionCameras = new Hashtable(); 
	private RenderTexture reflectionTexture = null;
	private int OldReflectionTextureSize = 0;
	private static bool portalRendered=false;
	private int textureSize;
	private bool teleport;
	private Dictionary<int, GameObject> clones = new Dictionary<int, GameObject>();
    private GameObject auxClone;
    private PortalCallFunction functionAtTeleport;

    [Space(10)]
    [SerializeField] private bool activateVisualGlitch = true;
    [SerializeField] private float visualGlitchDuration = 0.0f;

    private void Start()
    {
        if (visualGlitchDuration < 0)
        {
            visualGlitchDuration = 0;
        }

        functionAtTeleport = GetComponent<PortalCallFunction>();

    }

    public void OnWillRenderObject()
	{
		textureSize = advancedSettings.textureSize;
		teleport = advancedSettings.teleport;

        if (!partner)
        {
            return;
        }

        if (textureSize != partner.GetComponent<portal>().textureSize)
        {
				textureSize = Mathf.Max (advancedSettings.textureSize, partner.GetComponent<portal>().advancedSettings.textureSize);
				partner.GetComponent<portal>().textureSize = Mathf.Max(advancedSettings.textureSize, partner.GetComponent<portal>().advancedSettings.textureSize);
        }

		var rend = GetComponent<Renderer>();
		if (!enabled || !rend || !rend.sharedMaterial || !rend.enabled)
        {
			if (!rend.sharedMaterial)
            {
				Material portalMat = new Material(Shader.Find("FX/Portal"));
				rend.sharedMaterial = portalMat;
			}
			return;
		}
				

		Camera cam = Camera.current;
        if (!cam)
        {
            return;
        }

        if (portalRendered)
        {
            return;
        }
		portalRendered = true;

		Camera reflectionCamera;
		CreateMirrorObjects (cam, out reflectionCamera);

		Vector3 pos = partner.transform.position;
		Vector3 normal = partner.transform.up;

		int oldPixelLightCount = QualitySettings.pixelLightCount;
        if (disablePixelLights)
        {
            QualitySettings.pixelLightCount = 0;
        }

		UpdateCameraModes (cam, reflectionCamera);

		Transform formerParent = cam.transform.parent;
		cam.transform.SetParent (transform);

		Vector3 localPos = cam.transform.localPosition;
		Quaternion localRot = cam.transform.localRotation;

		cam.transform.SetParent (partner.transform);

		cam.transform.localPosition = localPos;

		cam.transform.localRotation = localRot;
		float d = -Vector3.Dot (normal, pos) - clipPlaneOffset;
		Vector4 reflectionPlane = new Vector4 (normal.x, normal.y, normal.z, d);

		Matrix4x4 reflection = Matrix4x4.zero;
		CalculateReflectionMatrix (ref reflection, reflectionPlane);

		Vector3 complement = partner.transform.forward;
		float d2 = -Vector3.Dot (complement, pos) - clipPlaneOffset;
		Vector4 reflectionPlane2 = new Vector4 (complement.x, complement.y, complement.z, d2);

		Matrix4x4 reflection2 = Matrix4x4.zero;
		CalculateReflectionMatrix (ref reflection2, reflectionPlane2);

		Vector3 oldpos = cam.transform.position;
		Vector3 newpos = reflection2.MultiplyPoint (reflection.MultiplyPoint (oldpos));
		reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection * reflection2;

		Vector4 clipPlane = CameraSpacePlane (reflectionCamera, pos, normal, 1.0f);

		Matrix4x4 projection = cam.CalculateObliqueMatrix (clipPlane);
		reflectionCamera.projectionMatrix = projection;

		cam.transform.SetParent (transform);

		cam.transform.localPosition = localPos;
		cam.transform.localRotation = localRot;
		cam.transform.SetParent (formerParent);

		reflectionCamera.transform.position = newpos;
		reflectionCamera.targetTexture = reflectionTexture;

		Vector3 euler = cam.transform.eulerAngles;
		cam.transform.localScale = Vector3.one;
		Material[] materials = rend.sharedMaterials;
		foreach (Material mat in materials)
        {
            if (mat.HasProperty("_ReflectionTex"))
            {
                mat.SetTexture("_ReflectionTex", reflectionTexture);
            }
		}
		

		reflectionCamera.Render ();

        if (disablePixelLights)
        {
            QualitySettings.pixelLightCount = oldPixelLightCount;
        }
         
		portalRendered = false;
	}


	bool isBehindMe(Vector3 loc)
	{
		return transform.InverseTransformPoint(loc).y < 0;
	}

	bool isCamBehindMe(GameObject cam)
	{
		return transform.InverseTransformPoint(cam.transform.position).y < 0;
	}

	Vector3 getChildCameraPos(GameObject obj)
	{
		Vector3 myCamPos = new Vector3(0,0,0);
		float CamCount = 0;
		foreach(Transform child in obj.transform)
		{
			if (child.gameObject.GetComponent<Camera>())
            {
				myCamPos += child.gameObject.GetComponent<Camera> ().transform.position;
				CamCount++;
			}
		}
		return myCamPos * (1f / CamCount);
	}

	GameObject GetChildCamera(GameObject obj)
	{
		foreach(Transform child in obj.transform)
		{
			if (child.gameObject.GetComponent<Camera>())
            {
				return child.gameObject.GetComponent<Camera>().gameObject;
			}
		}
		return null;
	}

	bool HasChildCamera(GameObject obj)
	{
		float CamCount = 0;
		foreach(Transform child in obj.transform)
		{
			if (child.gameObject.GetComponent<Camera>())
            {
				CamCount++;
			}
		}
		return CamCount > 0;
	}

	void OnTriggerExit(Collider other)
	{
        if (HasChildCamera(other.gameObject))
        {
            GetChildCamera(other.gameObject).GetComponent<Camera>().nearClipPlane = .3f;
        }
        else
        {
            Destroy(auxClone);
            clones.Remove(other.gameObject.GetInstanceID());
        }
	}

	private void Update()
	{
		
	}

	void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Clone")
			return;

		if (HasChildCamera(other.gameObject))
		{
			Camera cam = GetChildCamera(other.gameObject).GetComponent<Camera>();
			cam.nearClipPlane = Mathf.Min(.3f, .007f * (cam.transform.position - transform.position).magnitude);
		}


		foreach (GameObject clone in clones.Values)
		{
			Destroy(this.auxClone);
		}
		clones.Clear();
		
			if (teleport && (!HasChildCamera(other.gameObject) && !isBehindMe(other.transform.position)))
			{

				if (!clones.ContainsKey(other.gameObject.GetInstanceID()) && !clones.ContainsValue(other.gameObject))
				{


					GameObject clone = Instantiate(other.gameObject, transform.position, Quaternion.identity);
					clone.name = "Clone";
					clone.tag = "Clone";

				//Destroy(clone, Time.fixedDeltaTime);

				//if (!clone.GetComponent<DestroyOnTime>())
				//{
				//clone.AddComponent<DestroyOnTime>();
				//}

				clones.Add(other.gameObject.GetInstanceID(), clone);
					clones.TryGetValue(other.gameObject.GetInstanceID(), out this.auxClone);
				}
			}
			else
			{
				if (isBehindMe(other.transform.position))
				{
					Destroy(auxClone);
					clones.Remove(other.gameObject.GetInstanceID());
				}
			}
		
		if (teleport && (!HasChildCamera(other.gameObject) || isCamBehindMe(GetChildCamera(other.gameObject))))
        {

			Transform previousParent = other.transform.parent;
			Vector3 prevScale = other.transform.localScale;
			Vector3 localVel = transform.InverseTransformVector(other.gameObject.GetComponent<Rigidbody>().velocity);

			if (!clones.ContainsKey (other.gameObject.GetInstanceID()))
			{
				other.transform.SetParent (transform);
				Vector3 localPos = other.transform.localPosition;
				Quaternion localRot = other.transform.localRotation;
				other.transform.SetParent (partner.transform);

				other.transform.localPosition = new Vector3(localPos.x,-localPos.y,-localPos.z); 
				other.transform.localRotation = localRot;
				other.transform.SetParent (previousParent);
			}
			else
			{
				other.transform.SetParent (transform);
				Vector3 localPos = other.transform.localPosition;
				Quaternion localRot = other.transform.localRotation;
				other.transform.SetParent (previousParent);
				if (auxClone)
                {
					auxClone.transform.SetParent (partner.transform);
					auxClone.transform.localPosition = new Vector3 (localPos.x, -localPos.y, -localPos.z);
					auxClone.transform.localRotation = localRot;
					auxClone.transform.SetParent (null);
				}
			}

			Vector3 pos = partner.transform.position;
			Vector3 normal = partner.transform.up;

			float d = -Vector3.Dot (normal, pos) - clipPlaneOffset;
			Vector4 reflectionPlane = new Vector4 (normal.x, normal.y, normal.z, d);

			Matrix4x4 reflection = Matrix4x4.zero;
			CalculateReflectionMatrix (ref reflection, reflectionPlane);


			Vector3 complement = partner.transform.forward;
			float d2 = -Vector3.Dot (complement, pos) - clipPlaneOffset;
			Vector4 reflectionPlane2 = new Vector4 (complement.x, complement.y, complement.z, d2);

			Matrix4x4 reflection2 = Matrix4x4.zero;
			CalculateReflectionMatrix (ref reflection2, reflectionPlane2);


			other.transform.localScale = prevScale;
			if (!clones.ContainsKey (other.gameObject.GetInstanceID()))
            {
				other.transform.rotation = Quaternion.LookRotation (reflection2.MultiplyVector (reflection.MultiplyVector (other.transform.forward)), reflection2.MultiplyVector (reflection.MultiplyVector (other.transform.up)));
				other.transform.localScale = prevScale;
			}
            else if (auxClone)
            {
				auxClone.transform.rotation = Quaternion.LookRotation (reflection2.MultiplyVector (reflection.MultiplyVector (other.transform.forward)), reflection2.MultiplyVector (reflection.MultiplyVector (other.transform.up)));
				auxClone.transform.localScale = other.transform.localScale;
			}

			if (other.gameObject.GetComponent ("PortalableFirstPersonController"))
            {
				other.gameObject.GetComponent ("PortalableFirstPersonController").SendMessage ("UpdateOrientation", other.transform.rotation);

                if (activateVisualGlitch)
                {
                    if (GlitchEffect.glitchEffectInstance != null)
                    {
                        if (visualGlitchDuration == 0)
                        {
                            GlitchEffect.glitchEffectInstance.DisplayGlitchOn();
                        }
                        else
                        {
                            GlitchEffect.glitchEffectInstance.DisplayGlitchOn(visualGlitchDuration);
                        }
                    }
                }


                if (functionAtTeleport != null)
                {
                    functionAtTeleport.CallFuncions();
                }

            }

			if (!clones.ContainsKey (other.gameObject.GetInstanceID()))
            {
				other.gameObject.GetComponent<Rigidbody> ().velocity = partner.transform.TransformVector (-localVel);
			}

            if (isBehindMe(other.transform.position))
            {
                partner.GetComponent<portal>().OnTriggerStay(other);
            }

		}
	}


	void OnDisable()
	{
		if( reflectionTexture )
        {
			DestroyImmediate( reflectionTexture );
			reflectionTexture = null;
		}
        foreach (DictionaryEntry kvp in reflectionCameras)
        {
            DestroyImmediate(((Camera)kvp.Value).gameObject);
        }

		reflectionCameras.Clear();
	}



	private void UpdateCameraModes( Camera src, Camera dest )
	{
        if (dest == null)
        {
            return;
        }
		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;        
		if( src.clearFlags == CameraClearFlags.Skybox )
		{
			Skybox sky = src.GetComponent(typeof(Skybox)) as Skybox;
			Skybox mysky = dest.GetComponent(typeof(Skybox)) as Skybox;
			if( !sky || !sky.material )
			{
				mysky.enabled = false;
			}
			else
			{
				mysky.enabled = true;
				mysky.material = sky.material;
			}
		}
	
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
	}

	private void CreateMirrorObjects( Camera currentCamera, out Camera reflectionCamera )
	{
		reflectionCamera = null;

		if( !reflectionTexture || OldReflectionTextureSize != textureSize )
		{
            if (reflectionTexture)
            {
                DestroyImmediate(reflectionTexture);
            }
			reflectionTexture = new RenderTexture( textureSize, textureSize, 16 );
			reflectionTexture.name = "__PortalView" + GetInstanceID();
			reflectionTexture.isPowerOfTwo = true;
			reflectionTexture.hideFlags = HideFlags.DontSave;
			OldReflectionTextureSize = textureSize;
		}

		reflectionCamera = reflectionCameras[currentCamera] as Camera;
		if( !reflectionCamera ) 
		{
			GameObject go = new GameObject( "Portal Camera id" + currentCamera.GetInstanceID() + " for " + gameObject.name, typeof(Camera), typeof(Skybox) );
			reflectionCamera = go.GetComponent<Camera>();
			reflectionCamera.enabled = false;

			reflectionCamera.gameObject.AddComponent<FlareLayer>();

			go.hideFlags = HideFlags.HideAndDontSave; 
			reflectionCameras[currentCamera] = reflectionCamera;
		}        
	}

	private static float SngDirection(float a)
	{
        if (a > 0.0f)
        {
            return 1.0f;
        }
        if (a < 0.0f)
        {
            return -1.0f;
        }
		return 0.0f;
	}

	private Vector4 CameraSpacePlane (Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 offsetPos = pos + normal * clipPlaneOffset;
		Matrix4x4 m = cam.worldToCameraMatrix;
		Vector3 cpos = m.MultiplyPoint( offsetPos );
		Vector3 cnormal = m.MultiplyVector( normal ).normalized * sideSign;
		return new Vector4( cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos,cnormal) );
	}

	private static void CalculateReflectionMatrix (ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = (1F - 2F*plane[0]*plane[0]);
		reflectionMat.m01 = (- 2F*plane[0]*plane[1]);
		reflectionMat.m02 = (- 2F*plane[0]*plane[2]);
		reflectionMat.m03 = (- 2F*plane[3]*plane[0]);
		reflectionMat.m10 = (- 2F*plane[1]*plane[0]);
		reflectionMat.m11 = (1F - 2F*plane[1]*plane[1]);
		reflectionMat.m12 = (- 2F*plane[1]*plane[2]);
		reflectionMat.m13 = (- 2F*plane[3]*plane[1]);
		reflectionMat.m20 = (- 2F*plane[2]*plane[0]);
		reflectionMat.m21 = (- 2F*plane[2]*plane[1]);
		reflectionMat.m22 = (1F - 2F*plane[2]*plane[2]);
		reflectionMat.m23 = (- 2F*plane[3]*plane[2]);
		reflectionMat.m30 = 0F;
		reflectionMat.m31 = 0F;
		reflectionMat.m32 = 0F;
		reflectionMat.m33 = 1F;
	}
}

