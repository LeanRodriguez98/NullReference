using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewMainMenu
{
    public class MenuShard : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private Animator animator;
        [SerializeField] [HideInInspector] private AudioSource audioSource;
        public void SetAnimator()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        void OnMouseOver()
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
            animator.SetBool("IsMouseOver", true);
        }

        void OnMouseExit()
        {
            animator.SetBool("IsMouseOver", false);
        }

        public void FullRotate()
        {
            animator.SetTrigger("FullRotation");
        }
    }
}
