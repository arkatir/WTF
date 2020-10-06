using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script dont le but est de donner un framework dans lequel on peut "creer" et "detruire" des objets en scène sans avoir 
/// un impact négatif en performance à travers des Instanciate() et Destroy(). Un amas d'objets est instancié au début et 
/// desactivé dans un coin, lkorsqu'on a besoin de "creer" un objet (mob, projectile, collectible): on récupère le premier
/// objet non actif dans cette hierarchie et le teleporte au bon endroit, en ajustant les données de leur script. Pour "detruire" un objet, 
/// on le desactive et on le range dans un coin.

/// </summary>

public class ObjectPoolManager : MonoBehaviour
{
    [Header("Public Attributes")]
    public static ObjectPoolManager managerInstance;

    public List<GameObject> pooledObjects;

    //General information on gameObj reference      
    public GameObject objectToPool;
    
    public Transform storageTransform; //Where to put objects when inactive
    [Header("Private Attributes")]
    [SerializeField]
    private int spawnRadius;
    [SerializeField]
    private int initialAmountToPool;
    [SerializeField]
    private float timeBetweenSpawn;
    private float currentTime;

    private void Awake()
    {
        managerInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Init
        Vector3 storagePosition = storageTransform.position;
        pooledObjects = new List<GameObject>();
        for(int i=0; i < initialAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            //Hide and deactivate objects
            obj.SetActive(false);
            obj.transform.position = storagePosition;
            //Add to initial pool
            pooledObjects.Add(obj);
        }
    }

    #region Public Methods

    public GameObject GetAvailablePooledObject()
    {
        for(int i=0; i< pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject CreateObject(Vector3 givenPosition,  Quaternion givenRotation)
    {
        GameObject obj = GetAvailablePooledObject();
        if(obj != null)
        {
            obj.transform.position = givenPosition;
            obj.transform.rotation = givenRotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (pooledObjects.Contains(obj))
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

    // Update is called once per frame
    void Update()
    {
        
    }

}
