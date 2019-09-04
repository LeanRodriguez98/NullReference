using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponetSwitcher : MonoBehaviour {

    public MonoBehaviour[] componentToEnable;
    public MonoBehaviour[] componentToDisable;

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < componentToEnable.Length; i++)
            {
                componentToEnable[i].enabled = false;
            }
            for (int i = 0; i < componentToDisable.Length; i++)
            {
                componentToEnable[i].enabled = true;
            }

            Destroy(this.gameObject);
        }
    }

}
