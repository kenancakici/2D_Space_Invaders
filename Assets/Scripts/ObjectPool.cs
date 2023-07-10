using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    private Queue<GameObject> pooledObjects; // Oyun nesnelerini saklayan Nesne Havuzumuz; Array, List, Queue  vb. olabilir
    [SerializeField] private GameObject objectPrefab; // Hangi objemiz �o�alt�lacak, 
    [SerializeField] private int poolSize; // Havuzun b�y�kl��� (eleman say�s�)

    private void Awake()
    {
        pooledObjects = new Queue<GameObject>(); // Yeni bir kuyruk olu�turuyoruz.
        // D�ng� ile havuzun i�erisinde prefab nesnelerimizi olu�turuyoruz.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // ilk etapta bu nesneleri kullanmayaca��m�z i�in pasif hale getirdik
            pooledObjects.Enqueue(obj); // S�raya soktuk.
        }      
    }

    // Pooled objemizi �a��rmak i�in yaz�lan fonksiyon.
    public GameObject GetPooedObject()
    {
        GameObject obj =  pooledObjects.Dequeue(); // onjeyi s�radan ��kart�yoruz.
        obj.SetActive(true); // Aktif hale getiriyoruz.
        pooledObjects.Enqueue(obj); // tekrar s�raya sokuyoruz.
        return obj; // ��kard���m�z objeyi d�nd�r�yoruz.
    }

}
