using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Dialogue")]
public class DialogueTemplate : ScriptableObject
{
    [SerializeField, TextArea(1,4)] public string[] dialogue;
    [SerializeField, TextArea(1,5)] public string positiveMessage;
    [SerializeField, TextArea(1,5)] public string negativeMessage;
    [SerializeField, TextArea(1,5)] public string customRequest;
    public bool Tutroial;
}
