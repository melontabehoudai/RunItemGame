using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
 {

    [Header("弾の自動消滅時間")]
    public float deleteTime = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
