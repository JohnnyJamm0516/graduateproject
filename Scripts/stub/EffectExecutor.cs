using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    public string actioinName;
    public List<float> parmF;
    public List<GameObject> parmGb;
    Action action;
    public void Init_Executor(string name, List<float> parm_F, List<GameObject> parm_Gb, Action act)
    {
        actioinName = name;
        parmF = parm_F;
        parmGb = parm_Gb;
        action = act;
    }
    public void Execute()
    {
        
    }
}
public class EffectDictionary
{
    Func<string, float>[] fucList;
}
