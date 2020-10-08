using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script dont le but est de donner un framework dans lequel on peut "creer" et "detruire" des objets en scène sans avoir 
/// un impact négatif en performance à travers des Instanciate() et Destroy(). Un amas d'objets est instancié au début et 
/// desactivé dans un coin, lorsqu'on a besoin de "creer" un objet (mob, projectile, collectible): on récupère le premier
/// objet non actif dans cette hierarchie et le teleporte au bon endroit, en ajustant les données de leur script. Pour "detruire" un objet, 
/// on le desactive et on le range dans un coin.
/// Chaque type d'objet peut etre instancié séparé en l'appellant par son nom dans les assets. Celui-ci est indexé dans un dictionnaire
/// en début de runtime.
/// </summary>

public class ObjectPoolManager : MonoBehaviour
{
    [Header("Public Attributes")]
    public static ObjectPoolManager managerInstance;

    public Dictionary<string, List<GameObject>> pooledObjectsDictionary = new Dictionary<string, List<GameObject>>();

    //General information on gameObj reference      
    public List<GameObject> objectsToPool;
    
    public Transform storageTransform; //Where to put objects when inactive
    [Header("Private Attributes")]
    [SerializeField]
    private int initialAmountToPool;

    private void Awake()
    {
        managerInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Init
        Vector3 storagePosition = storageTransform.position;
        foreach(GameObject o in objectsToPool)
        {
            string savedName = o.name;
            List<GameObject> newListObjects = new List<GameObject>();
            pooledObjectsDictionary.Add(savedName, newListObjects);

            for (int i = 0; i < initialAmountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(o);
                //Hide and deactivate objects
                obj.SetActive(false);
                obj.transform.position = storagePosition;
                //Add to initial pool
                pooledObjectsDictionary[savedName].Add(obj);
            }
        }
        
        
    }

    #region Public Methods

    public GameObject GetAvailablePooledObject(string name)
    {
        for(int i=0; i< pooledObjectsDictionary[name].Count; i++)
        {
            if (!pooledObjectsDictionary[name][i].activeInHierarchy)
            {
                return pooledObjectsDictionary[name][i];
            }
        }
        return null;
    }

    public GameObject CreateObject(string givenname, Vector3 givenPosition,  Quaternion givenRotation)
    {
        GameObject obj = GetAvailablePooledObject(givenname);
        if(obj != null)
        {
            obj.transform.position = givenPosition;
            obj.transform.rotation = givenRotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.Log("No available object in object pool!");
            return null;
        }
    }
    public void RemoveObject(GameObject obj)
    {
        string chosenName = (obj.name).Replace("(Clone)", "");
        if (pooledObjectsDictionary[chosenName].Contains(obj))
        {
            obj.SetActive(false);
            obj.transform.position = storageTransform.position;
        }
        else
        {
            Destroy(obj);
        }
    }
    #endregion

}
