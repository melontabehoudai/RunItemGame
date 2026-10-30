using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 diff; //対象とカメラの維持するべき距離
    public GameObject player;　//カメラの対象（Player）
    public float followSpeed =5.0f;　//追従スピード

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       player = GameObject.FindWithTag("Player");
       //diff=target.transform.position - transform.position; //最初に維持するべき距離を定義
       diff=player.transform.position - transform.position; //最初に維持するべき距離を定義

    }

    // Update is called once per frame
    //Playerが動いた（Update/FixedUpdate後に実行される）
    void LateUpdate()
    {   //現時点Aの位置は次第に詰まっていくのでやがて限りなくイコールに近い距離になってくる
        //最終的には限りなく近づき続けるだけでイコールにはならないが、描画上は止まっているかのように表現される
        transform.position=Vector3.Lerp(
            transform.position,　//現時点A
            //target.transform.position-diff,　//目的地B
            player.transform.position-diff,　//目的地B
            Time.deltaTime*followSpeed　//A→Bへの進行に対してどれだけ進んだ位置にいるべきかの進捗率
            );
    }
}
