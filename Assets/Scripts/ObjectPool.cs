using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    private Queue<GameObject> pooledObjects; // Oyun nesnelerini saklayan Nesne Havuzumuz; Array, List, Queue  vb. olabilir
    [SerializeField] private GameObject objectPrefab; // Hangi objemiz çoðaltýlacak, 
    [SerializeField] private int poolSize; // Havuzun büyüklüðü (eleman sayýsý)

    private void Awake()
    {
        pooledObjects = new Queue<GameObject>(); // Yeni bir kuyruk oluþturuyoruz.
        // Döngü ile havuzun içerisinde prefab nesnelerimizi oluþturuyoruz.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // ilk etapta bu nesneleri kullanmayacaðýmýz için pasif hale getirdik
            pooledObjects.Enqueue(obj); // Sýraya soktuk.
        }      
    }

    // Pooled objemizi çaðýrmak için yazýlan fonksiyon.
    public GameObject GetPooedObject()
    {
        GameObject obj =  pooledObjects.Dequeue(); // onjeyi sýradan çýkartýyoruz.
        obj.SetActive(true); // Aktif hale getiriyoruz.
        pooledObjects.Enqueue(obj); // tekrar sýraya sokuyoruz.
        return obj; // Çýkardýðýmýz objeyi döndürüyoruz.
    }

}
