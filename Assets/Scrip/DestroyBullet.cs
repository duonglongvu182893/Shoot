using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    Vector3 firtSpaw;

    // Start is called before the first frame update
    void Start()
    {
        firtSpaw = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, firtSpaw) >= 50f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
