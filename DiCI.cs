using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiCI : MonoBehaviour
{
    RubyController rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay2D(Collider2D collision)
    {//²ÈÏÝÚå¿ÛÑª1
        rb = collision.GetComponent<RubyController>();
        if (rb != null)
        {
            //Debug.Log("DICI");
            rb.changeHealth(-1);
        }
    }
}
