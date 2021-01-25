using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ShortcutController : MonoBehaviour {
    [Serializable]
    public class Shortcut {
        public KeyCode keycode;
        public UnityEvent actions;
    }

    public List<Shortcut> Shortcuts;

    public void Update() {
        foreach (Shortcut shortcut in Shortcuts) {
            if (Input.GetKeyDown(shortcut.keycode))
                shortcut.actions.Invoke();
        }
    }
}
