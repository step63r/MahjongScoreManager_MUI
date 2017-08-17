# 麻雀スコア管理ツール
![キャプチャ１](./MahjongScoreManager_MUI/Readme/20170816001.png)
## Overview
麻雀の得点を計算、保存、集計するツールです。  
対局ごとにルールを設定できるのが特長です。

## Requirement
 * Windows 10 Home 
 * NET Framework 4.5.2 以上

 ## Install
 こちらで配布しています。  
 <http://projectstep.web.fc2.com/computer/csp/progs.html>  
 インストーラを実行し、その指示に従ってください。

 ## Usage
 ### 新規対局
 点棒状況からスコアを計算します。

 東～北それぞれ対局者名、得点を入力し、「計算」を押下します。  
 スコアに間違いがなければ「保存」を押下で対局データを保存できます。

 「その他収支」にはトビ賞、役満祝儀などを入力します。  
 例えば東家が南家を飛ばして終局した場合、東家に 10、南家に -10 を入力します。

 必要事項を入力したのに「計算」が押下できない場合、  
 入力した点数の合計に誤りがある可能性がありますのでご確認ください。

 ### 対局情報参照
 保存した対局データを閲覧できます。

 集計期間を指定し、「検索」を押下すると  
 その期間に対局した全ての対局者の個人成績が集計され表示されます。

 ### 対局者変更
 対局を行うメンバーを登録します。

 画面下部のテキストボックスに名前を入力し  
 「追加」を押下するとメンバーが追加されます。

登録した対局者を削除することもできますが、  
当該対局者の対局データが存在する場合、  
データに不整合が発生することがありますので  
あまりお勧めしません。

### 対局設定
対局設定を登録します。  
設定を新しく作成する場合は、  
「新規登録」押下→設定入力→「更新」押下  
という順番に行います。

<dl>
    <dt>設定名称</dt>
    <dd>任意の名前を入力します。</dd>
    <dt>レート</dt>
    <dd>実額への換算レートを入力します。<br />例えば「点5」であれば 0.05 と入力します。</dd>
    <dt>ウマ（3位→2位）</dt>
    <dd>対局終了時に3位から2位へ渡される点数を入力します。<br />例えば「10-20」であれば 10,000 を入力します。</dd>
    <dt>ウマ（4位→1位）</dt>
    <dd>対局終了時に4位から1位へ渡される点数を入力します。<br />例えば「10-20」であれば 20,000 を入力します。</dd>
    <dt>端数の取り扱い</dt>
    <dd>百位の取り扱いを指定します。</dd>
</dl>

## Disclaimer
本ソフトウェアを用いていかなる損失を生じても  
当方では責任を負いかねますのでご了承ください。

## License
本ソフトウェアのライセンスは MIT License に従います。

## Contact Us
<dl>
    <dt>サイト</dt>
    <dd>http://projectstep.web.fc2.com/</dd>
    <dt>メール</dt>
    <dd>clannad.ilove.magister at gmail.com</dd>
</dl>

## History
| Revision | Updates |
|:---------------:|:-----------------|
| 2017.8.16.0 | β版リリース |
