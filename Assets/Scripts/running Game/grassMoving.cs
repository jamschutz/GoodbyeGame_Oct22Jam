using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassMoving : MonoBehaviour
{
    public GameObject myManager;
     float zPos;

    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        zPos += Time.deltaTime * myManager.GetComponent<RunningGameManager>().grassSpeed;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);

        if (zPos > 29)
        {
            zPos = -11;
        }


    }
}
