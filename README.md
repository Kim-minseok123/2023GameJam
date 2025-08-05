# 한국콘텐츠진흥원 주최 2023 대한민국 게임잼 대상 수상작 - Amundsen 
https://www.kocca.kr/seriousgame/gameinfo/info.do?gameTp=7&gameNo=49&menuNo=204001

## 목차
  - [소개](#소개) 
  - [개발 동기](#개발-동기)
  - [개발 환경](#개발-환경)
  - [개발 과정](#개발-과정)
  - [역할](#역할)
  - [플레이영상](#플레이영상)
  - [게임 다운로드](#게임-다운로드)
## 소개
<div align="center">

<img alt="Title" src="https://github.com/user-attachments/assets/48d4f60e-637d-4ecb-afc4-2957242db22c" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/ba5ef49a-660d-4db3-a0aa-2e5b40b03bd2" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/ed6d4374-dc9c-4780-b7cb-9c8a0cc74654" width="49%" height="330"/>
<img alt="Title" src="https://github.com/user-attachments/assets/e3312b46-49f4-44ec-ae33-05c51d9b57f7" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/8ace1cae-ef95-4022-96f2-f47e3de5992a" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/68077895-0ec9-4ff5-99e6-27d84bc5b4c4" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/e012a26b-e380-4426-a1b8-0fa358a96ab1" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/08085cd0-abd1-4ea6-9a8c-f2b9340fd063" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/577c4587-e7b5-4f8b-94a4-51e4372a0340" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/bb848ca9-06db-40a9-b09a-6e0637210021" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/6cffec11-a2af-4bfe-8c1c-9ca7334233c8" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/6af2f197-b4de-4825-ba26-8fa49503e4b1" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/f53dab38-9a22-48b6-9141-9c571716572f" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/e461240e-5981-415a-9067-bbfa817f47bb" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/14801a20-4cbb-4f9b-8596-d1677fce6319" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/aa260576-8aa0-42c0-bc74-a2dfbd8e3add" width="49%" height="230"/>
<img alt="Title" src="https://github.com/user-attachments/assets/3bd221e8-4c30-4e12-9a99-9747b87329a1" width="49%" height="230"/>

  < 게임 플레이 사진 >

</div>

+ 2023 대한민국 게임잼 대상 수상작입니다.

+ 게임잼에서 주어진 **기록 유산** 중 **'로알 아문센의 남극 탐험' 필름 컬렉션**을 주제로 제작하였습니다.

+ 개발 기간: 2023.08.13 ~ 2023.08.15 ( 약 60시간 )

+ TEAM
  -   프로그래머 : 김민석, 김영남
  -   아트 : 김서윤, 심경천
  -   기획 : 금재욱 

+ 형상 관리: Git SourceTree

<br>

## 개발 동기
첫 남극 탐험이라는 중요한 사건이 기록된 원본 필름, 심지어 영상으로 기록했다는 점에서 유일무이하다.

기록 유산의 진정한 가치는 이 기록 내용을 후대에 전하는 것!
- 첫 탐극 탐험이 언제, 어떤 모습으로 이루어졌는지!
- 인류 최초로 남극점에 도달한 사람들이 누구인지!

우리는 게임으로 그 가치를 계승하자! 라고 판단.
게임을 통해 기록이 전하고자 했던 내용을 대신 전달하고 게임 플레어이들이 이 내용을 기억하게끔 하는 것을 목표로 삼았다.

<br>

## 개발 환경
+ Unity 2021.3.17f1 LTS

+ Visual Studio 2022

<br>

## 개발 과정
- 가치를 계승하기 위한 결정들 #1
  - 인류 최초로 남극점에 도달한 사람들을 알리기 위한 결정
  - 아문센 탐험팀 5인을 플레이어블 캐릭터로 만들기!
  - 각 캐릭터에 5인의 실제 능력과 역할을 바탕으로 한 특수 능력 부여하기

- 가치를 계승하기 위한 결정들 #2
  - 언제/ 어떤 모습으로 남극점에 도달했는지 알리기 위한 결정
  - 각 스테이지의 목표를 **<필름 수집>**으로 설정하기
  - 필름 획득 시, 실제 기록 유산이 출력되도록 하기

- 단순히 기록 전달만으로의 기능보단 재미있는 게임을 만드는 것도 중요하다!
  - 도전 / 퍼즐 요소를 추가해서 재밌게 조작할 수 있는 게임을 만들자
  - 온도 게이지가 모두 떨어지기 전에 남극점에 도달하자.
  - 각각의 능력을 적재적소에 활용해야만 한다.
  
<br>

## 역할
### 프로그래밍
김민석 : 플레이어 이동 점프 조작 제어, 캐릭터 5개 스위칭 설계, 5개 캐릭터 능력 제작

김영남 : 3개 스테이지 제작, UI 연동, 타이틀 화면, 엔딩 화면 제작  

### 아트
김서윤 : 캐릭터 아트 및 애니메이션

심경천 : UI, 타일 및 배경 아트

### 기획
금재욱 : 레벨 디자인, 스킬 디자인 등 총 기획  

<br>

## 플레이영상
https://www.youtube.com/watch?v=0q5UMyOv6-Q

## 게임 다운로드
https://drive.google.com/file/d/1wEI_R13ufpnqf3xzZVd0iGN99WOKE90Q/view?usp=sharing

