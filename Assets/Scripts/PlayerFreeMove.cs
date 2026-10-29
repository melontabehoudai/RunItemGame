using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeMove : MonoBehaviour

    
{
    CharacterController controller;
    [SerializeField] private GameObject animatorBody; //Animatorコンポーネントを持っているオブジェクトの取得
    Animator animator;

    Vector3 moveDirection = Vector3.zero;　//Moveメソッドに与える方向
    public float gravity;　//重力値
    public float speedZ;　//前に進む力
    public float speedJump;　//ジャンプ力

    private InputAction moveAction;//Moveアクションを参照するための変数
    private InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //必要なコンポーネントを自動取得
        controller = GetComponent<CharacterController>();
        animator = animatorBody.GetComponent<Animator>();

        //PlayerInputコンポーネントに設定されている現Mapは何かを取得（Playerマップ）
        InputActionMap actionMap = GetComponent<PlayerInput>().currentActionMap;
        moveAction = actionMap.FindAction("Move");//現マップ上にいる”Move”というアクションを取得
        moveAction = actionMap.FindAction("Jump");//現マップ上にいる”Jump”というアクションを取得
            
    }

    // Update is called once per frame
    void Update()
    {
        //CharacterContollerコンポーネントが接地判断を持っている
        if(controller.isGrounded)
        {
            //上下キーによる前か後ろかの値の設定
            //if(Input.GetAxis("Vertical")!=0.0f)
            if(moveAction.ReadValue<Vector2>().y !=0.0f)
            {
                //moveDirection.z = Input.GetAxis("Vertical") * speedZ;
                moveDirection.z = moveAction.ReadValue<Vector2>().y * speedZ;
            }
            else
            {
                moveDirection.z = 0;
            }

            //横回転　Rotateメソッドで直接回転させる
            //transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);
            transform.Rotate(0, moveAction.ReadValue<Vector2>().x * 3, 0);

            //if (Input.GetButton("Jump"))
            if(jumpAction.IsPressed())
            {
                moveDirection.y = speedJump;　//y方向をセッティング
                //animator.SetTrigger("jump");
            }

            //重力分の力を毎フレーム追加
            moveDirection.y -= gravity * Time.deltaTime;

            //移動実行　セッティングしたmoveDirecctionの値を使ってMoceメソッドの発動
            //実際にPlayerが動く
            //回転を含む全身だと、グローバルな座標指定からズレるので、グローバルな座標に変換してからMoveに与える
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            controller.Move(globalDirection * Time.deltaTime);

            //移動後接地していたらY方向の速度はリセットする
            if (controller.isGrounded) moveDirection.y = 0;

            //速度が0以上なら走っているフラグをtrueにする
            //animator.SetBool("run", moveDirection.z != 0.0f);

        }
    }
}
