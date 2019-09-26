using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlatesMaterials : MonoBehaviour {

    public Substance.Game.Substance substance;
    public Substance.Game.SubstanceGraph[] graphs;
    public string valueName;
    public float valueSpeed;

    public void SetGraphs()
    {
        GameObject[] gos = Utilities.GetAllObjectsInScene().ToArray();
        List<BetoScripts.PressurePlate> aux = new List<BetoScripts.PressurePlate>();
        foreach (GameObject go in gos)
        {
            if (go.GetComponent< BetoScripts.PressurePlate>() != null)
            {
                aux.Add(go.GetComponent<BetoScripts.PressurePlate>());
            }
        }
        BetoScripts.PressurePlate[] presurePlates = aux.ToArray();
        graphs = substance.graphs.ToArray();
        Debug.Log("Presureplates in scene = " + presurePlates.Length);
        Debug.Log("PresurePLates graphs instance created = " + graphs.Length);

        if (presurePlates.Length > graphs.Length)
        {
            Debug.LogError("Graphs instances are insufficients, please add " +(presurePlates.Length - graphs.Length).ToString() + " more and try again!");
            return;
        }

        AnimatedMaterial.TimeValues timeValue;
        timeValue.updateValueName = valueName;
        timeValue.speed = valueSpeed;
        for (int i = 0; i < presurePlates.Length; i++)
        {
            timeValue.substanceGraph = graphs[i];
            presurePlates[i].SetAnimatedMaterial(timeValue);
        }
    }
}
