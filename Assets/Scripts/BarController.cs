using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

    public Slider HPBar;
    public Slider RedBar;
    public Slider BlueBar;
    public Slider GreenBar;
    public Slider YellowBar;

    public Text hptext;

    public void SetHP(int i)
    {
        HPBar.value = i;
        hptext.text = HPBar.value + "/" + HPBar.maxValue; 
    }
    public void SetRed(int i)
    {
        RedBar.value = i;
    }
    public void SetBlue(int i)
    {
        BlueBar.value = i;
    }
    public void SetGreen(int i)
    {
        GreenBar.value = i;
    }
    public void SetYellow(int i)
    {
        YellowBar.value = i;
    }

    public int GetHP()
    {
        return Convert.ToInt32(HPBar.value);
    }
    public int GetRed()
    {
        return Convert.ToInt32(RedBar.value);
    }
    public int GetBlue()
    {
        return Convert.ToInt32(BlueBar.value);
    }
    public int GetGreen()
    {
        return Convert.ToInt32(GreenBar.value);
    }
    public int GetYellow()
    {
        return Convert.ToInt32(YellowBar.value);
    }
}
