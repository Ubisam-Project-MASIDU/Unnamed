# 잭과콩나무 - Episode13
***
 - 작성 및 제작 : 김명현
 - 언어 : C#
***
 - Update Log
     1) 2021-08-05 : 처음 시작하면 구슬 아이템들이 생성되게 설정
     2) 2021-08-06 : 아이템이 삭제되는 함수 작성
     3) 2021-08-10 : 가로로 아이템이 3개이상 모이면 삭제되게 설정
     4) 2021-08-12 : 세로로 아이템이 3개이상 모이면 삭제되게 설정
     5) 2021-08-17 : 타임아웃 기능 구현
***
 - 구동화면 및 내용

![구동화면](https://user-images.githubusercontent.com/37494407/129817077-a7ef7fd9-9522-4b72-b063-7975fddb0188.png)



    - 에피소드13 구동화면이다.
    - 화면 왼쪽에는 점수를 출력해준다. (아이템이 터질경우 점수가 증가하는 방식)
    - 오른쪽에는 남은 시간을 표현해준다. 시간이 흐를수록 흰 원이 점점 사라진다.
    - 아이템을 드래그하여 3개이상의 같은 모양을 연결해주면 아이템이 터지면서 새로운 아이템이 위에서 떨어져 내려온다.
    
    
***

- H&G (Episode13) 구성 정보
  - Image
    - 구현에 필요한 이미지 파일들은 모두 FuzzStoryWorld/Assets/Image/ 에서 참고하였다.
  - Scene
    - Scene파일의 경우에도 FuzzStoryWorld/Assets/Scenes/1_13H&G.unity 에서 진행하였다.
  - Prefab
    - Item1_Orange.prefab : 오렌지색의 구슬 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다.
    - Item2_Red.prefab : 빨간색의 구슬 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다
    - Item3_Purple.prefab : 보라색의 구슬 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다
    - Item4_Blue.prefab : 파랑색의 구슬 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다
    - Item5_Green.prefab : 초록색의 구슬 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다
    - Item6_Yellow.prefab : 노랑색의 구슬 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다
    - Item7_Star.prefab : 별모양의 아이템 프리팹이다. SwapItem.cs 파일이 적용되어있다
  - Script
    - ControlUI.cs : 화면에 비치는 UI들을 관리하기위한 스크립트이다. 그리고 종료시 연결을 지정해준다. Ex) 남은시간, 점수
    - ManageArray.cs : 메인 스크립트이다. 처음 씬이 시작되면 아이템들을 생성해주고 수시로 아이템들이 3개이상 겹쳤는지 검사하고 이를 터트리고 다시 재생성해준다.
    - ManageItem.cs : 아이템을 생성하는 함수와 삭제하는 함수를 작성해둔 함수이다.
    - SwapItem.cs : 아이템 프리팹들에 적용되는 스크립트이다. 적절한 타이밍에 아이템들이 드래그가 가능한 상태로 해주며, 아이템을 드래그할 경우 어느방향으로 드래그를 하였는지 계산한다. 
    
***

