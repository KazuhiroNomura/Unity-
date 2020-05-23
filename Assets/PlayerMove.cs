using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove:MonoBehaviour {
    //Rigidbodyを変数に入れる
    Rigidbody rb;
    //移動スピード
    public float speed = 3f;
    //ジャンプ力
    public float thrust = 200;
    //Animatorを入れる変数
    private Animator animator;
    //ユニティちゃんの位置を入れる
    public Vector3 playerPos;
    //地面に接触しているか否か
    public bool ground;
    //重力等の変更ができるようにパブリック変数とする
    public System.Single gravity;
    //public System.Single speed;
    public System.Single jumpSpeed;
    public System.Single rotateSpeed;

    //外部から値が変わらないようにPrivateで定義
    private CharacterController characterController;
    //private Animator animator;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start() {
        this.characterController=this.GetComponent<CharacterController>();
        this.animator=this.GetComponent<Animator>();
        this.playerPos=this.transform.position;
    }
    private Int32 カウンタ = 0;
    // Update is called once per frame
    void Update() {
        //Debug.Log($"カウンタ{this.カウンタ++}");
        var A = Input.GetKey(KeyCode.JoystickButton0);
        if(Input.GetKey(KeyCode.JoystickButton0)) {
            Debug.Log($"{nameof(A)}{A}");
        }
        var B = Input.GetKey(KeyCode.JoystickButton1);
        if(Input.GetKey(KeyCode.JoystickButton1)) {
            Debug.Log($"{nameof(B)}{B}");
        }
        var X = Input.GetKey(KeyCode.JoystickButton2);
        if(Input.GetKey(KeyCode.JoystickButton2)) {
            Debug.Log($"{nameof(X)}{X}");
        }
        var Y = Input.GetKey(KeyCode.JoystickButton3);
        if(Input.GetKey(KeyCode.JoystickButton3)) {
            Debug.Log($"{nameof(Y)}{Y}");
        }
        var LB = Input.GetKey(KeyCode.JoystickButton4);
        if(Input.GetKey(KeyCode.JoystickButton4)) {
            Debug.Log($"{nameof(LB)}{LB}");
        }
        var RB = Input.GetKey(KeyCode.JoystickButton5);
        if(Input.GetKey(KeyCode.JoystickButton5)) {
            Debug.Log($"{nameof(RB)}{RB}");
        }
        var View = Input.GetKey(KeyCode.JoystickButton6);
        if(Input.GetKey(KeyCode.JoystickButton6)) {
            Debug.Log($"{nameof(View)}{View}");
        }
        var Menu = Input.GetKey(KeyCode.JoystickButton7);
        if(Input.GetKey(KeyCode.JoystickButton7)) {
            Debug.Log($"{nameof(Menu)}{Menu}");
        }
        var L_Stick = Input.GetKey(KeyCode.JoystickButton8);
        if(Input.GetKey(KeyCode.JoystickButton8)) {
            Debug.Log($"{nameof(L_Stick)}{L_Stick}");
        }
        var R_Stick = Input.GetKey(KeyCode.JoystickButton9);
        if(Input.GetKey(KeyCode.JoystickButton9)) {
            Debug.Log($"{nameof(R_Stick)}{R_Stick}");
        }
        var L_Stick_H = Input.GetAxis("L_Stick_H");
        if(L_Stick_H!=0) {
            Debug.Log($"{nameof(L_Stick_H)}{L_Stick_H}");
        }
        var L_Stick_V = Input.GetAxis("L_Stick_V");
        if(L_Stick_V!=0) {
            Debug.Log($"{nameof(L_Stick_V)}{L_Stick_V}");
        }
        var R_Stick_H = Input.GetAxis("R_Stick_H");
        if(R_Stick_H!=0) {
            Debug.Log($"{nameof(R_Stick_H)}{R_Stick_H}");
        }
        var R_Stick_V = Input.GetAxis("R_Stick_V");
        if(R_Stick_V!=0) {
            Debug.Log($"{nameof(R_Stick_V)}{R_Stick_V}");
        }
        //D-Pad
        var D_Pad_H = Input.GetAxis("D_Pad_H");
        if(D_Pad_H!=0) {
            Debug.Log($"{nameof(D_Pad_H)}{D_Pad_H}");
        }
        var D_Pad_V = Input.GetAxis("D_Pad_V");
        if(D_Pad_V!=0) {
            Debug.Log($"{nameof(D_Pad_V)}{D_Pad_V}");
        }
        var L_R_Trigger = Input.GetAxis("L_R_Trigger");
        if(L_R_Trigger!=0) {
            Debug.Log($"{nameof(L_R_Trigger)}{L_R_Trigger}");
        }
        if(this.ground) {
            //A・Dキー、←→キーで横移動
            var x = Input.GetAxisRaw("Horizontal")*Time.deltaTime*this.speed;

            //W・Sキー、↑↓キーで前後移動
            var z = Input.GetAxisRaw("Vertical")*Time.deltaTime*this.speed;

            //現在の位置＋入力した数値の場所に移動する
            this.rb.MovePosition(this.transform.position+new Vector3(x,0,z));

            //ユニティちゃんの最新の位置から少し前の位置を引いて方向を割り出す
            var direction = this.transform.position-this.playerPos;

            //移動距離が少しでもあった場合に方向転換
            if(direction.magnitude>0.01f) {
                //directionのX軸とZ軸の方向を向かせる
                this.transform.rotation=Quaternion.LookRotation(new Vector3
                    (direction.x,0,direction.z));
                //走るアニメーションを再生
                this.animator.SetBool("Running",true);
            } else {
                //ベクトルの長さがない＝移動していない時は走るアニメーションはオフ
                this.animator.SetBool("Running",false);
            }

            //ユニティちゃんの位置を更新する
            this.playerPos=this.transform.position;

            //スペースキーやゲームパッドの3ボタンでジャンプ
            if(Input.GetButton("Jump")) {
                //thrustの分だけ上方に力がかかる
                this.rb.AddForce(this.transform.up*this.thrust);
                //速度が出ていたら前方と上方に力がかかる
                if(this.rb.velocity.magnitude>0)
                    this.rb.AddForce(this.transform.forward*this.thrust+this.transform.up*this.thrust);
            }
        }
        ////rayを使用した接地判定
        //if(this.CheckGrounded()==true) {

        //    //前進処理
        //    //if(L_Stick_V>0) {
        //    //    this.moveDirection.z=this.speed;
        //    //} else {
        //    //    this.moveDirection.z=0;
        //    //}
        //    this.moveDirection.z+=L_Stick_V;

        //    //方向転換
        //    //方向キーのどちらも押されている時
        //    if(Input.GetKey(KeyCode.LeftArrow)&&Input.GetKey(KeyCode.RightArrow)) {
        //        //向きを変えない
        //    }
        //    //左方向キーが押されている時
        //    else if(L_Stick_H<0) {
        //        this.transform.Rotate(0,this.rotateSpeed*-1,0);
        //    }
        //    //右方向キーが押されている時
        //    else if(L_Stick_H>0) {
        //        this.transform.Rotate(0,this.rotateSpeed,0);
        //    }

        //    //jump
        //    if(A) {
        //        this.moveDirection.y=this.jumpSpeed;
        //    }

        //    //重力を発生させる
        //    this.moveDirection.y-=this.gravity*Time.deltaTime/2;

        //    //移動の実行
        //    var globalDirection = this.transform.TransformDirection(this.moveDirection);
        //    this.characterController.Move(globalDirection*Time.deltaTime);

        //    //速度が０以上の時、Runを実行する
        //    this.animator.SetBool("Run",this.moveDirection.z>0.0f);
        //}
    }

    //rayを使用した接地判定メソッド
    public System.Boolean CheckGrounded() {

        //初期位置と向き
        var ray = new Ray(this.transform.position+Vector3.up*0.1f,Vector3.down);

        //rayの探索範囲
        var tolerance = 0.3f;

        //rayのHit判定
        //第一引数：飛ばすRay
        //第二引数：Rayの最大距離
        return Physics.Raycast(ray,tolerance);
    }

}