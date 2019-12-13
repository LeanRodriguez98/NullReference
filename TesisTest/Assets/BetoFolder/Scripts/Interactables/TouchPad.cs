using UnityEngine;

public class TouchPad : Interactable
{
	public GameObject floorToDisable;

	public Animator animator;
	public Material disabledMaterial;
	public Material enabledMaterial;
    public AudioSource sliderAudioSource;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private MeshRenderer meshRenderer;
	private bool doorIsOpen;

    private AudioSource audioClip;

	private bool aivaHasRebooted;

    //private MaterialSwaper materialSwaper;
    //private bool materialSwaperState;

    public override void Start()
	{
		base.Start();
        //materialSwaper = GetComponent<MaterialSwaper>();
		doorIsOpen = false;
		meshRenderer = GetComponent<MeshRenderer>();
		SetMaterial();

        audioClip = GetComponent<AudioSource>();
        audioClip.volume *= GameManager.GetInstance().gameOptions.soundsVolume;//PlayerPrefs.GetFloat("VolumeLevel");
        sliderAudioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume; //PlayerPrefs.GetFloat("VolumeLevel");

		aivaHasRebooted = false;

    }

    public override void Interact()
	{
		base.Interact();

		if (aivaHasRebooted)
		{
			floorToDisable.SetActive(false);
		}
		else
		{
			doorIsOpen = !doorIsOpen;
			sliderAudioSource.clip = doorIsOpen ? doorOpenSound : doorCloseSound;

			SetMaterial();
			animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
			audioClip.Play();
			PlaySliderSound();
		}
	}

	public void CloseSlidingDoorOnAivaReboot()
	{
		doorIsOpen = false;
		sliderAudioSource.clip = doorIsOpen ? doorOpenSound : doorCloseSound;
		animator.SetBool("OpenDoor", false);
		audioClip.Play();
		PlaySliderSound();

		aivaHasRebooted = true;
	}


    void Update()
    {
        //if (materialSwaperState != materialSwaper.Swaped)
       // {
       //     materialSwaperState = materialSwaper.Swaped;
            SetMaterial();
        //}
    }
    public void SetMaterial()
	{
        //if (!materialSwaper.Swaped)
       // {
            UpdateMaterial();
       // }
	}

    private void UpdateMaterial()
    {

       // if (doorIsOpen)
       //     meshRenderer.material = enabledMaterial;
       // else
       //     meshRenderer.material = disabledMaterial;
    }

    public void PlaySliderSound()
    {
        if(!sliderAudioSource.isPlaying)
            sliderAudioSource.Play();
    }

}
