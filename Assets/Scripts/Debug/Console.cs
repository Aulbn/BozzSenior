using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    bool isShowingConsole = false;

    string input = "";
    Vector2 scroll;
    Vector2 helpScroll;
    GUIStyle consoleStyle;
    int rows;

    float rowHeight = 50;

    List<DebugCommandBase> commandList;
    List<string> printList;
    List<string> helpList;
    int helpListIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        consoleStyle = new GUIStyle();
        consoleStyle.fontSize = 32;
        consoleStyle.normal.textColor = Color.white;

        printList = new List<string>();
        commandList = new List<DebugCommandBase>();
        commandList.Add(new DebugCommand("nextlevel", "Load the next level in the list.", "nextlevel", () => { GameManager.LoadNextLevel(); }));
        commandList.Add(new DebugCommand("returntolobby", "Load the lobby scene.", "returntolobby", () => { GameManager.LoadScene("Lobby"); }));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            Debug.Log("Console!");
            ToggleConsole();
        }

        if (input.Length > 0 && input.Substring(input.Length - 1).Equals("§"))
        {
            input = "";
            ToggleConsole();
        }
    }

    private void OnGUI()
    {
        if (!isShowingConsole) return;

        if (Event.current.type == EventType.KeyDown) {
            if (Event.current.keyCode == KeyCode.Return)
                OnReturn();
            if (Event.current.keyCode == KeyCode.DownArrow)
                helpListIndex += 1;
            if (Event.current.keyCode == KeyCode.UpArrow)
                helpListIndex -= 1;
            if (Event.current.keyCode == KeyCode.Tab)
            {
                input = helpList[helpListIndex].Replace("<b>", "").Replace("</b>", "");
            }
        }


        float y = 0;

        GUI.backgroundColor = Color.black;
        //Print box
        GUI.Box(new Rect(0, y, Screen.width, 150), "");
        Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * rows);
        scroll = GUI.BeginScrollView(new Rect(0, y + 5, Screen.width, 130), scroll, viewport);
        for (int i = 0; i < printList.Count; i++)
        {
            GUI.Label(new Rect(10, rowHeight * i, viewport.width - 100, rowHeight), printList[i], consoleStyle);
        }
        GUI.EndScrollView();

        y += 150;

        //Input box
        GUI.Box(new Rect(0, y, Screen.width, rowHeight), "");
        GUI.SetNextControlName("ConsoleInput");
        input = GUI.TextField(new Rect(10, y + 5, Screen.width - 20, rowHeight), input, consoleStyle);
        GUI.FocusControl("ConsoleInput");

        //Help box
        if (input.Length > 0)
        {
            helpList = new List<string>();
            foreach (DebugCommandBase command in commandList){
                if (command.CommandID.Contains(input))
                {
                    string commandText = command.CommandID;
                    helpList.Add($"{commandText.Substring(0, commandText.IndexOf(input))}" +
                        $"<b>{commandText.Substring(commandText.IndexOf(input), input.Length)}" +
                        $"</b>{commandText.Substring(commandText.IndexOf(input) + input.Length)}");
                }
            }

            Rect helpBoxRect = new Rect(0, y - Mathf.Min(150, helpList.Count * rowHeight), Screen.width, Mathf.Min(150, helpList.Count * rowHeight));
            GUI.Box(helpBoxRect, "");
            helpScroll = GUI.BeginScrollView(helpBoxRect, helpScroll, viewport);
            helpListIndex = Mathf.Clamp(helpListIndex, 0, helpList.Count - 1);
            for (int i = 0; i < helpList.Count; i++)
            {
                Rect helpRect = new Rect(10, rowHeight * i, viewport.width, rowHeight);
                if (i == helpListIndex)
                {
                    Color backgroundColor = GUI.backgroundColor;
                    GUI.backgroundColor = Color.white;
                    GUIStyle helpStyle = new GUIStyle(consoleStyle);
                    helpStyle.normal.background = Texture2D.whiteTexture;
                    helpStyle.normal.textColor = Color.black;
                    GUI.Label(helpRect, helpList[i], helpStyle);
                    GUI.backgroundColor = backgroundColor;
                }else
                    GUI.Label(helpRect, helpList[i], consoleStyle);
            }
            GUI.EndScrollView();
        }
    }

    void OnReturn()
    {
        Debug.Log("Pressed Return");
        foreach (DebugCommandBase command in commandList)
        {
            if (input.Equals(command.CommandID) && command as DebugCommand != null)
            {
                Print($"COMMAND: {input}");
                ((DebugCommand)command).Invoke();
                input = "";
                return;
            }
        }
        Print($"NON COMMAND: {input}");
        input = "";
    }

    public void Print(string message)
    {
        printList.Add(message);
        if (printList.Count > 20)
            printList.RemoveAt(printList.Count-1);
        Debug.Log("Print:" + message);
    }

    public void ToggleConsole()
    {
        isShowingConsole = !isShowingConsole;
        GameManager.SetPlayerControllerInput(isShowingConsole, !isShowingConsole);
    }
}

public class DebugCommandBase
{
    private string commandID;
    private string commandDescription;
    private string commandFormat;

    public string CommandID { get { return commandID; } }
    public string DommandDescription { get { return commandDescription; } }
    public string CommandFormat { get { return commandFormat; } }

    public DebugCommandBase(string id, string description, string format)
    {
        commandID = id;
        commandDescription = description;
        commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base (id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}
