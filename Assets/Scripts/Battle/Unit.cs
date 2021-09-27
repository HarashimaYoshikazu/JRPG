using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //unityのnameとかぶるからnewする
    public new string name;
    //HP
     public  int hp;
   

    public int mp;
   
    //実行するコマンド

    public CommandSO serectCommand;

    //持ってる技

    public CommandSO[] commands;

    public Unit target;




}
