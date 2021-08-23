using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene12Controller : MonoBehaviour {
    private VoiceManager mvm_VoiceManager;
    [SerializeField]
    private GameObject mgo_WinHAGPos;
    private SpriteRenderer mspr_SetBrightness;
    // private int mn_NumCharacter = 2;
    private Status mst_HAGStatus;
    public float HAGHP {
        set {
            mst_HAGStatus.HP = value;
            if (mst_HAGStatus.HP <= 0f) {
                DisplayGameOverUI();
                WitchTakeOnHAG();
            }
        }
        get {
            return mst_HAGStatus.HP;
        }
    }
    public Status HAGStatus {
        get {
            return mst_HAGStatus;
        }
    }
    private Status mst_WitchStatus;
    public Status WitchStatus {
        get {
            return mst_WitchStatus;
        }
    }

    void Awake() {
        mvm_VoiceManager = FindObjectOfType<VoiceManager>();
        mst_HAGStatus = new Status(20f, 2f, 10f, Status.Character.HAG);
        mst_WitchStatus = new Status(Status.Character.Witch);
        mspr_SetBrightness = GameObject.Find("Bright").GetComponent<SpriteRenderer>();
        // 마녀를 무찌르세요 ! 라는 문구 0_01Straing 씬에서 VMLoader -> VMController -> KRVoiceManager, ENVoiceManager, JPVoiceManager, CNVoiceManager 각각 인스펙터창에 삽입하기
        // mvm_VoiceManager.playVoice()
    }

    void DisplayGameOverUI() {
        GameObject go_UI = GameObject.Find("UI");
        go_UI.transform.GetChild(0).gameObject.SetActive(false);
        go_UI.transform.GetChild(1).gameObject.SetActive(false);
        go_UI.transform.GetChild(2).gameObject.SetActive(true);

        StartCoroutine(SetBright());

    }
    IEnumerator SetBright() {
        Color c_TempColor = mspr_SetBrightness.color;

        for (int i = 100; i >= 0; i--) {
            if (c_TempColor.a <= 0.2f) {
                c_TempColor.a += Time.deltaTime * 0.8f;               //이미지 알파 값을 타임 델타 값 * 0.01
            }
            mspr_SetBrightness.color = c_TempColor;
            yield return null;
        }
    } 

    void WitchTakeOnHAG() {
        GameObject go_Witch = GameObject.Find("witch");
        go_Witch.transform.position = GameObject.Find("HAG").transform.position + new Vector3(0, 4, 0);
        go_Witch.GetComponent<WitchAttack>().mgo_HAG = null;     
        transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(1).transform.GetChild(4).transform.position = go_Witch.transform.position;  
        go_Witch.SetActive(false);

        Destroy(GameObject.Find("HAG"));
    }
}
