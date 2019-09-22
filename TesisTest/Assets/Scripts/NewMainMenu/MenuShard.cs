using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewMainMenu
{
    public class MenuShard : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private Animator animator;

        public void SetAnimator()
        {
            animator = GetComponent<Animator>();
        }

        void OnMouseOver()
        {
            animator.SetBool("IsMouseOver", true);
        }

        void OnMouseExit()
        {
            animator.SetBool("IsMouseOver", false);
        }
    }
}
