using UnityEngine;
using System.Collections;


public class Argument
{
    public int intValue;
    public bool booleanValue;
    public string stringValue;
}

public interface IArgumentGetter
{
    Argument Arg0
    {
        get;
    }

    Argument Arg1
    {
        get;
    }
}

public interface IArgumentSetter
{
    Argument Arg0
    {
        set;
    }

    Argument Arg1
    {
        set;
    }
}

public class EventFactory : MonoBehaviour, IArgumentGetter
{
    public Argument Arg0
    {
        get
        {
            return arg0;
        }
    }

    public Argument Arg1
    {
        get
        {
            return arg1;
        }
    }


    public void SetVar0(int num)
    {
        arg0.intValue = num;
    }

    public void SetVar0(bool boolean)
    {
        arg0.booleanValue = boolean;
    }

    public void SetVar0(string str)
    {
        arg0.stringValue = str;
    }

    public void SetVar1(int num)
    {
        arg1.intValue = num;
    }

    public void SetVar1(bool boolean)
    {
        arg1.booleanValue = boolean;
    }

    public void SetVar1(string str)
    {
        arg1.stringValue = str;
    }

    public void Make(GameObject prefab)
    {
        var newObj = Instantiate(prefab);

        foreach (var container in newObj.GetComponents<IArgumentSetter>())
        {
            container.Arg0 = arg0;
            container.Arg1 = arg1;
        }
    }

    Argument arg0 = new Argument();
    Argument arg1 = new Argument();
}
