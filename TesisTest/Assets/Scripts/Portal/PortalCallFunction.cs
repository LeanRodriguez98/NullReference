using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCallFunction : MonoBehaviour {

    [System.Serializable]
    public enum varTypes
    {
        Null, Int, Float, String, Char, Bool, GameObject, Transform
    }
    
    [System.Serializable]
    public struct CalledFunctions
    {
        public GameObject target;
        public string methodName;
        public varTypes varType;

        public int intParameter;
        public float floatParameter;
        public string stringParameter;
        public char charParameter;
        public bool boolParameter;
        public GameObject gameObjectParameter;
        public Transform transformParameter;
    }
    public CalledFunctions[] methodsToCall;

    
  

    void Start()
    {
        /* Component[] components = obj.GetComponents<Component>();
        mathodsNames = new List<string>();
        for(int i = 0; i < System.AppDomain.CurrentDomain.GetAssemblies().Length; i++)
        {
            System.Reflection.Assembly reflection = System.AppDomain.CurrentDomain.GetAssemblies()[i];

            foreach(System.Type type  in reflection.GetTypes())
            {
                foreach(Component c in components)
                {
                    if (type == c.GetType())
                    {
                        Debug.Log(type.Name);
                        mathodsNames.Add(type.Name);
                        foreach(System.Reflection.MethodInfo m in type.GetMethods())
                        {
                            if (m.Name == "TestFunction")
                            {
                           // Debug.Log(c.name + " " + m.Name);
                                
                            }
                        }
                    }
                }   
            }
        }*/
    }

    public void FindMethods()
    {
        /* for(int a  = 0; a < methodsToCall.Length; a++)
        {

        Component[] components = methodsToCall[a].taeget.GetComponents<Component>();
        for(int i = 0; i < System.AppDomain.CurrentDomain.GetAssemblies().Length; i++)
        {
            System.Reflection.Assembly reflection = System.AppDomain.CurrentDomain.GetAssemblies()[i];

            foreach(System.Type type  in reflection.GetTypes())
            {
                foreach(Component c in components)
                {
                    if (type == c.GetType())
                    {
                        
                        
                        
                        
                        foreach(System.Reflection.MethodInfo m in type.GetMethods())
                        {
                            
                            if (m.Name == "TestFunction")
                            {
                                // Debug.Log(c.name + " " + m.Name);
                                
                            }
                        }
                    }
                }   
            }
        }
        }
*/
    }
    public void CallFuncions()
    {
        for(int i = 0; i < methodsToCall.Length; i++)
        {
            switch (methodsToCall[i].varType)
            {
                case varTypes.Null:
                    methodsToCall[i].target.SendMessage(methodsToCall[i].methodName);
                    break;
                case varTypes.Int:
                    methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].intParameter);
                    break;
                case varTypes.Float:
                    methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].floatParameter);
                    break;
                case varTypes.String:
                    methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].stringParameter);
                    break;
                case varTypes.Char:
                    methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].charParameter);
                    break;
                case varTypes.Bool:
                    methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].boolParameter);
                    break;
                case varTypes.GameObject:
                    if(methodsToCall[i].gameObjectParameter != null)
                        methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].gameObjectParameter);
                    break;
                case varTypes.Transform:
                    if (methodsToCall[i].transformParameter != null)
                        methodsToCall[i].target.SendMessage(methodsToCall[i].methodName, methodsToCall[i].transformParameter);
                    break;
                default:
                    Debug.LogError("Unespected function to call in " + methodsToCall[i].target.name);
                    break;
            }


            
        }

    }
}
