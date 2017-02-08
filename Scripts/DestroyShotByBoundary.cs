using UnityEngine;
using System.Collections;

public class DestroyShotByBoundary : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayerShotBoundary"))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
