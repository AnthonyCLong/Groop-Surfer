using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioReference : MonoBehaviour {
    public CustomButton defaultRadio;
    public List<CustomButton> radios = new List<CustomButton>();

    public void Start() {
        if (defaultRadio)
            defaultRadio.Select();
    }

    public void Add(CustomButton button) {
        radios.Add(button);
    } 
}
