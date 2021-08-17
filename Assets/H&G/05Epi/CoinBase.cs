using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBase : MonoBehaviour
{
    public float coinSpeed = 0;
    public Vector2 StartPosition;
    // Start is called before the first frame update
    private void OnEnable(){
        transform.position = StartPosition;        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isPlay){
            transform.Translate(Vector2.left * Time.deltaTime * GameManager.instance.gameSpeed);
        }
        
        if(transform.position.x < -6){
            gameObject.SetActive(false);
        }
    }
}
