using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomComponent : MonoBehaviour
{
    //provides method to do dynamic upgrades at runtime
    public object this[string propertyName]
    {
        get { return this.GetType().GetField(propertyName).GetValue(this); }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
}
