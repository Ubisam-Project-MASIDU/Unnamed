/*
 * - Name : WitchAttack.cs
 * - Writer : 최대준
 * 
 * - Content :
 * 마녀가 헨젤과 그레텔에게 공격하는 것을 표현하는 스크립트 클래스이다.
 * 
 * - History
 * 1) 2021-08-13 : 코드 구현중. 
 * 2) 2021-08-13 : 주석 작성.
 *  
 * - Variable 
 *
 * - Function
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMagic : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======
    private enum Magic {
        Thunder
    }
    [SerializeField]
    public GameObject mgo_HAG;     
    [SerializeField]
    public GameObject[] mgo_MagicPrefab = new GameObject[1];     

    void Update() {

        if(Mathf.Abs(mgo_HAG.transform.position.x - transform.position.x) < 3.0 ) {
            Invoke("CreateEffect", 0.1f);
        } else {
            Invoke("FinishEffect", 1f);
        }
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackSkill(Magic emSpecifyMagic) {
        switch (emSpecifyMagic) {
            // case Magic.Thunder :
                // mgo_MagicPrefab[((int)emSpecifyMagic)]
        }
    }
}
