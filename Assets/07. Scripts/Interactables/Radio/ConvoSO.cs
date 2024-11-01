using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class ConvoSO : ScriptableObject
{
    //List of all messages in one conversation, set in unity editor
    public List<string> statements;

    public string GetStatement(int index)
    {
        return statements[index];
    }

    public int GetCount()
    {
        return statements.Count;
    }

}
