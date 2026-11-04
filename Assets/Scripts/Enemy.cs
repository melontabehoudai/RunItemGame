using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyLife = 3; //敵の体力
    public GameObject enemyBody; //見た目のオブジェクトの情報

    //無敵時間の秒数
    public float freezeTime = 1.0f;

    //ダメージ中のコルーチンを参照する変数
    Coroutine damageStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //もしもダメージコルーチンがnullでなければ（ダメージ中）
       

        if(damageStart!=null)
        {   //点滅処理
            //valが一定周期で正か負か
            float val = Mathf.Sin(Time.time * 50); //三角関数と時間
            if(val>0)
             {
             //見た目のMeshRendererをON
             enemyBody.GetComponent<MeshRenderer>().enabled = true;
             }
            else
            {
             //見た目のMeshRendererをOFF
             enemyBody.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(damageStart==null)
        { 
            if (other.gameObject.tag=="Bullet")
            {
                enemyLife--; //体力が1減る
                if(enemyLife < 0)
                {
                    Destroy(gameObject);
                }
                else //体力が残っている場合
                {
                   damageStart = StartCoroutine(DamageStart()); //コルーチンの発動
                }
            }
        }
    }
    IEnumerator DamageStart()
    {
        //freezeTime待ってからダメージコルーチン変数を解放
        yield return new WaitForSeconds(freezeTime);
        damageStart = null;

        //見た目のMeshRendererをON
        enemyBody.GetComponent<MeshRenderer>().enabled = true;
    }
}
