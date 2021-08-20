﻿/*
 * - Name : SwapItem.cs
 * - Writer : 김명현
 * 
 * - Content :
 * 아이템을 드래그하면 어느방향으로 드래그 하였는지 할려주는 스크립트
 * 
 * - History
 * 1) 2021-08-11 : 어느방향으로 드래그하였는지 알려주는 함수 작성
 * 2) 2021-08-12 : 상하좌우 방향으로 드래그 기능 구현
 * 
 * - Variable 
 * mb_DragFlag                                      Flag 값 -> Flag값이 True일 경우에만 어느방향으로 드래그하였는지 알려줌
 * mv3_screenSpace                                  월드좌표를 화면좌표로 변경하여 저장해두는 변수
 * mv3_offset                                       마우스 클릭좌표를 계산하기 위해 필요
 * mv3_StartPoint                                   마우스 클릭좌표를 저장해두는 변수
 * mv3_EndPoint                                     마우스에서 손을뗄떼 좌표를 저장해주는 변수
 * DragDirection                                    드래그 방향을 숫자로 변환해 저장해두는 변수
 * mg_GameDirector                                  게임디렉터 오브젝트 연결을 위한 변수
 * 
 * 
 * - Function
 * ChangeSwapFlagTrue()                             DragFlag값을 True로 변환
 * ChangeSwapFlagFalse()                            DragFlag값을 False로 변환
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템들을 서로 바꿀때의 처리를 작성해둔 스크립트
public class SwapItem : MonoBehaviour
{
    private Vector3 mv3_screenSpace;                                                    // 월드좌표를 화면좌표로 변경하여 저장해두는 변수
    private Vector3 mv3_offset;                                                         // 마우스 클릭좌표를 계산하기 위해 필요
    Vector3 mv3_StartPoint;                                                             // 마우스 클릭좌표를 저장해두는 변수
    Vector3 mv3_EndPoint;                                                               // 마우스에서 손을뗄떼 좌표를 저장해주는 변수
    int DragDirection;                                                                  // 드래그 방향을 숫자로 변환해 저장해두는 변수
    bool mb_DragFailFlag = false;
    
    GameObject mg_GameDirector;                                                         // 게임디렉터 오브젝트 연결을 위한 변수

    GameObject mg_Col1;                                                     // 연결을 위한 변수 -> 1번째 세로줄 관리를 위한 변수
    GameObject mg_Col2;                                                     // 연결을 위한 변수 -> 2번째 세로줄 관리를 위한 변수 
    GameObject mg_Col3;                                                     // 연결을 위한 변수 -> 3번째 세로줄 관리를 위한 변수 
    GameObject mg_Col4;                                                     // 연결을 위한 변수 -> 4번째 세로줄 관리를 위한 변수 
    GameObject mg_Col5;                                                     // 연결을 위한 변수 -> 5번째 세로줄 관리를 위한 변수 
    GameObject mg_Col6;                                                     // 연결을 위한 변수 -> 6번째 세로줄 관리를 위한 변수 
    GameObject mg_Col7;                                                     // 연결을 위한 변수 -> 7번째 세로줄 관리를 위한 변수 
    GameObject mg_TempGameObject;                                           // 아이템 스왑시 해당 오브젝트에 접근하기위한 변수


    // Start is called before the first frame update
    void Start()
    {
        mg_GameDirector = GameObject.Find("GameDirector");                              // 오브젝트 연결
        mg_Col1 = GameObject.Find("Col1");
        mg_Col2 = GameObject.Find("Col2");
        mg_Col3 = GameObject.Find("Col3");
        mg_Col4 = GameObject.Find("Col4");
        mg_Col5 = GameObject.Find("Col5");
        mg_Col6 = GameObject.Find("Col6");
        mg_Col7 = GameObject.Find("Col7");
    }

    /// <summary>
    /// 마우스 왼쪽 버튼을 누른경우
    /// </summary>
    private void OnMouseDown()
    {
        if (mg_GameDirector.GetComponent<MainScript>().b_ReturnDragFlag() && mg_GameDirector.GetComponent<MainScript>().b_ReturnExchangeFlag() == false)                                                        // 마우스가 클릭 가능한 상태인 경우
        {                                                                               // 마우스 좌표를 딴다.
            mv3_screenSpace = Camera.main.WorldToScreenPoint(transform.position);                                   
            mv3_offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mv3_screenSpace.z));
            mv3_StartPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mv3_screenSpace.z)) + mv3_offset;
            //Debug.Log("마우스 다운 포지션 : " + mv3_StartPoint);
        }
    }

    /// <summary>
    /// 마우스 왼쪽 버튼을 뗀 경우
    /// </summary>
    private void OnMouseUp()
    {
        if (mg_GameDirector.GetComponent<MainScript>().b_ReturnDragFlag() && mg_GameDirector.GetComponent<MainScript>().b_ReturnExchangeFlag() == false)                                                        // 마우스가 클릭 가능한 상태인 경우
        {
            mb_DragFailFlag = false;
            v_ChangeExchageFlagTrue();
            var curmv3_screenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mv3_screenSpace.z);
            mv3_EndPoint = Camera.main.ScreenToWorldPoint(curmv3_screenSpace) + mv3_offset;
            //Debug.Log("마우스 업 포지션 : " + mv3_EndPoint);
            DragDirection = n_CalculateTheAngle(mv3_StartPoint, mv3_EndPoint);          // 처음 눌렀을때의 위치와 뗄떄의 각도를 계산하여 어느방향으로 드래그하였는지 계산한다.

            switch (DragDirection)
            {
                case -1:
                    Debug.Log("드래그 안함");
                    v_ChangeExchageFlagFalse();
                    break;
                case 0:
                    Debug.Log("드래그 방향 : Left");
                    v_DragLeft();
                    if (mb_DragFailFlag == true)
                    {
                        v_ChangeExchageFlagFalse();
                        break;
                    }
                    else if (mg_GameDirector.GetComponent<ManageItemArray>().b_InspectArrayIsPop() == false)         // 아이템 배열을 검사하여 아이템이 터지지 않을 경우 원래위치로 복원
                    {
                        Invoke("v_DragRight", 1f);
                        Invoke("v_ChangeExchageFlagFalse", 1.5f);
                    }
                    else
                        v_ChangeExchageFlagFalse();
                    break;
                case 1:
                    Debug.Log("드래그 방향 : Down");
                    v_DragDown();
                    if (mb_DragFailFlag == true)
                    {
                        v_ChangeExchageFlagFalse();
                        break;
                    }
                    else if (mg_GameDirector.GetComponent<ManageItemArray>().b_InspectArrayIsPop() == false)
                    {
                        Invoke("v_DragUp", 1f);
                        Invoke("v_ChangeExchageFlagFalse", 1.5f);
                    }
                    else
                        v_ChangeExchageFlagFalse();
                    break;
                case 2:
                    Debug.Log("드래그 방향 : Right");
                    v_DragRight();
                    if (mb_DragFailFlag == true)
                    {
                        v_ChangeExchageFlagFalse();
                        break;
                    }
                    else if (mg_GameDirector.GetComponent<ManageItemArray>().b_InspectArrayIsPop() == false)
                    {
                        Invoke("v_DragLeft", 1f);
                        Invoke("v_ChangeExchageFlagFalse", 1.5f);
                    }
                    else
                        v_ChangeExchageFlagFalse();
                    break;
                case 3:
                    Debug.Log("드래그 방향 : Up");
                    v_DragUp();
                    if (mb_DragFailFlag == true)
                    {
                        v_ChangeExchageFlagFalse();
                        break;
                    }
                    else if (mg_GameDirector.GetComponent<ManageItemArray>().b_InspectArrayIsPop() == false)
                    {
                        Invoke("v_DragDown", 1f);
                        Invoke("v_ChangeExchageFlagFalse", 1.5f);
                    }
                    else
                        v_ChangeExchageFlagFalse();
                    break;
            }
        }
    }

    #region 함수 선언부
    /// <summary>
    /// 드래그한 방향을 계산해주는 함수
    /// </summary>
    /// <param name="v3StartPoint">마우스 첫 지점</param>
    /// <param name="v3EndPoint">마우스 끝 지점</param>
    /// <returns></returns>
    public int n_CalculateTheAngle(Vector3 v3StartPoint, Vector3 v3EndPoint)
    {
        Vector3 DragDirection = v3StartPoint - v3EndPoint;
        if (DragDirection.magnitude <= 0.2f) 
            return -1;
        float aimAngle = Mathf.Atan2(DragDirection.y, DragDirection.x);

        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }
        float angle = aimAngle * Mathf.Rad2Deg;
        int swipe = (((int)angle + 45) % 360) / 90;
        return swipe;
    }

    /// <summary>
    /// Invoke를 사용하기위해 함수형태로 선언, 아래쪽으로 드래그
    /// </summary>
    public void v_DragDown()
    {
        int ni = 0;
        int nj = 0;
        int ItemPositionX = (int)this.transform.position.x;
        int ItemPositionY = (int)this.transform.position.y;
        //Debug.Log("x : " + ItemPositionX + " y : " + ItemPositionY);

        switch (ItemPositionX)
        {
            case -6:
                nj = 0;
                break;
            case -4:
                nj = 1;
                break;
            case -2:
                nj = 2;
                break;
            case 0:
                nj = 3;
                break;
            case 2:
                nj = 4;
                break;
            case 4:
                nj = 5;
                break;
            case 6:
                nj = 6;
                break;
        }
        switch (ItemPositionY)
        {
            case 5:
                ni = 0;
                break;
            case 3:
                ni = 1;
                break;
            case 1:
                ni = 2;
                break;
            case 0:
                ni = 3;
                break;
            case -1:
                ni = 4;
                break;
            case -3:
                ni = 5;
                break;
        }
        Debug.Log("바꿀아이템 좌표 : i : " + ni + " j : " + nj);
        if (ni == 5)
        {
            Debug.Log("맨 아래 아이템은 아래로 드래그 불가능");
            mb_DragFailFlag = true;
            return;
        }
        else
        {
            // 위치 이동
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 2);
            // Child 순서 변경
            this.transform.SetSiblingIndex(this.transform.GetSiblingIndex() - 1);

            // 배열 이동
            mg_GameDirector.GetComponent<ManageItemArray>().v_ReplaceWithBottomValue(ni, nj);
        }
    }

    /// <summary>
    /// Invoke를 사용하기위해 함수형태로 선언, 위쪽으로 드래그
    /// </summary>
    public void v_DragUp()
    {
        int ni = 0;
        int nj = 0;
        int ItemPositionX = (int)this.transform.position.x;
        int ItemPositionY = (int)this.transform.position.y;
        //Debug.Log("x : " + ItemPositionX + " y : " + ItemPositionY);

        switch (ItemPositionX)
        {
            case -6:
                nj = 0;
                break;
            case -4:
                nj = 1;
                break;
            case -2:
                nj = 2;
                break;
            case 0:
                nj = 3;
                break;
            case 2:
                nj = 4;
                break;
            case 4:
                nj = 5;
                break;
            case 6:
                nj = 6;
                break;
        }
        switch (ItemPositionY)
        {
            case 5:
                ni = 0;
                break;
            case 3:
                ni = 1;
                break;
            case 1:
                ni = 2;
                break;
            case 0:
                ni = 3;
                break;
            case -1:
                ni = 4;
                break;
            case -3:
                ni = 5;
                break;
        }
        Debug.Log("바꿀아이템 좌표 : i : " + ni + " j : " + nj);
        if (ni == 0)
        {
            Debug.Log("맨 위 아이템은 위로 드래그 불가능");
            mb_DragFailFlag = true;
            return;
        }
        else
        {
            // 위치 이동
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2);
            // Child 순서 변경
            this.transform.SetSiblingIndex(this.transform.GetSiblingIndex() + 1);

            // 배열 이동
            mg_GameDirector.GetComponent<ManageItemArray>().v_ReplaceWithTopValue(ni, nj);
        }
    }

    /// <summary>
    /// /// <summary>
    /// Invoke를 사용하기위해 함수형태로 선언, 오른쪽으로 드래그
    /// </summary>
    /// </summary>
    public void v_DragRight()
    {
        int ni = 0;
        int nj = 0;
        int ItemPositionX = (int)this.transform.position.x;
        int ItemPositionY = (int)this.transform.position.y;
        int n_Index = this.transform.GetSiblingIndex();
        //Debug.Log("x : " + ItemPositionX + " y : " + ItemPositionY);

        switch (ItemPositionX)
        {
            case -6:
                nj = 0;
                break;
            case -4:
                nj = 1;
                break;
            case -2:
                nj = 2;
                break;
            case 0:
                nj = 3;
                break;
            case 2:
                nj = 4;
                break;
            case 4:
                nj = 5;
                break;
            case 6:
                nj = 6;
                break;
        }
        switch (ItemPositionY)
        {
            case 5:
                ni = 0;
                break;
            case 3:
                ni = 1;
                break;
            case 1:
                ni = 2;
                break;
            case 0:
                ni = 3;
                break;
            case -1:
                ni = 4;
                break;
            case -3:
                ni = 5;
                break;
        }
        Debug.Log("바꿀아이템 좌표 : i : " + ni + " j : " + nj);
        if (nj == 6)
        {
            Debug.Log("맨 오른쪽 아이템은 오른쪽으로 드래그 불가능");
            mb_DragFailFlag = true;
            return;
        }
        else
        {
            // 위치 이동
            this.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
            // Child 순서 변경
            switch (this.transform.parent.name)
            {
                case "Col1":
                    mg_TempGameObject = mg_Col2.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col2.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col1.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
                    break;
                case "Col2":
                    mg_TempGameObject = mg_Col3.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col3.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col2.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
                    break;
                case "Col3":
                    mg_TempGameObject = mg_Col4.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col4.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col3.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
                    break;
                case "Col4":
                    mg_TempGameObject = mg_Col5.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col5.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col4.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
                    break;
                case "Col5":
                    mg_TempGameObject = mg_Col6.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col6.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col5.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
                    break;
                case "Col6":
                    mg_TempGameObject = mg_Col7.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col7.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col6.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
                    break;
            }
            //this.transform.SetSiblingIndex(this.transform.GetSiblingIndex() - 1);

            // 배열 이동
            mg_GameDirector.GetComponent<ManageItemArray>().v_ReplaceWithRightValue(ni, nj);
        }
    }

    /// <summary>
    /// Invoke를 사용하기위해 함수형태로 선언, 왼쪽으로 드래그
    /// </summary>
    public void v_DragLeft()
    {
        int ni = 0;
        int nj = 0;
        int ItemPositionX = (int)this.transform.position.x;
        int ItemPositionY = (int)this.transform.position.y;
        int n_Index = this.transform.GetSiblingIndex();
        //Debug.Log("x : " + ItemPositionX + " y : " + ItemPositionY);

        switch (ItemPositionX)
        {
            case -6:
                nj = 0;
                break;
            case -4:
                nj = 1;
                break;
            case -2:
                nj = 2;
                break;
            case 0:
                nj = 3;
                break;
            case 2:
                nj = 4;
                break;
            case 4:
                nj = 5;
                break;
            case 6:
                nj = 6;
                break;
        }
        switch (ItemPositionY)
        {
            case 5:
                ni = 0;
                break;
            case 3:
                ni = 1;
                break;
            case 1:
                ni = 2;
                break;
            case 0:
                ni = 3;
                break;
            case -1:
                ni = 4;
                break;
            case -3:
                ni = 5;
                break;
        }
        Debug.Log("바꿀아이템 좌표 : i : " + ni + " j : " + nj);
        if (nj == 0)
        {
            Debug.Log("맨 왼쪽 아이템은 왼쪽으로 드래그 불가능");
            mb_DragFailFlag = true;
            return;
        }
        else
        {
            // 위치 이동
            this.transform.position = new Vector2(this.transform.position.x - 2, this.transform.position.y);
            // Child 순서 변경
            switch (this.transform.parent.name)
            {
                case "Col2":
                    mg_TempGameObject = mg_Col1.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col1.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col2.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
                    break;
                case "Col3":
                    mg_TempGameObject = mg_Col2.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col2.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col3.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
                    break;
                case "Col4":
                    mg_TempGameObject = mg_Col3.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col3.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col4.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
                    break;
                case "Col5":
                    mg_TempGameObject = mg_Col4.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col4.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col5.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
                    break;
                case "Col6":
                    mg_TempGameObject = mg_Col5.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col5.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col6.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
                    break;
                case "Col7":
                    mg_TempGameObject = mg_Col6.transform.GetChild(n_Index).gameObject;
                    this.transform.SetParent(mg_Col6.transform);
                    this.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.SetParent(mg_Col7.transform);
                    mg_TempGameObject.transform.SetSiblingIndex(n_Index);
                    mg_TempGameObject.transform.position = new Vector2(this.transform.position.x + 2, this.transform.position.y);
                    break;
            }
            //this.transform.SetSiblingIndex(this.transform.GetSiblingIndex() - 1);

            // 배열 이동
            mg_GameDirector.GetComponent<ManageItemArray>().v_ReplaceWithLeftValue(ni, nj);

        }
    }
    
    public void v_ChangeExchageFlagFalse()
    {
        mg_GameDirector.GetComponent<MainScript>().v_ChangeExchangeFlagFalse();
    }

    public void v_ChangeExchageFlagTrue()
    {
        mg_GameDirector.GetComponent<MainScript>().v_ChangeExchangeFlagTrue();
    }


    #endregion
}
