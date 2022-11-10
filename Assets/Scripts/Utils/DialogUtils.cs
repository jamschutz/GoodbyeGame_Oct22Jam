using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogUtils
{
    // special strings
    public const string PLAYER_NAME_SPECIAL = "{PLAYER_NAME}";
    public const string NEW_LINE_SPECIAL = "{NEW_LINE}";
    public const string CHOICE_DECLARATION = "{CHOICE}";
    public const string CHOICE_OPTIONS_DECLARATION = "{CHOICES: ";
    public const string DECISIONS_DECLARATION = "{DECISIONS: ";
    public const string END_DIALOG_SPECIAL = "{END_DIALOG}";
    public const string SET_FLAG_SPECIAL = "{SET_FLAG:";

    public static bool IsChoice(string dialog)
    {
        if(dialog.Length < CHOICE_DECLARATION.Length) return false;
        return dialog.Substring(0,CHOICE_DECLARATION.Length) == CHOICE_DECLARATION;
    }


    public static bool IsDecisionList(string dialog)
    {
        if(dialog.Length < DECISIONS_DECLARATION.Length) return false;
        return dialog.Substring(0,DECISIONS_DECLARATION.Length) == DECISIONS_DECLARATION;
    }


    public static bool IsEndDialog(string dialog)
    {
        return dialog.IndexOf(END_DIALOG_SPECIAL) >= 0;
    }


    public static string GetCorrectedChoiceLine(string dialog)
    {
        // remove choice declaration
        var correctedLine = dialog.Replace(CHOICE_DECLARATION, "");

        // find the {CHOICES: 1 | 2} index bounds
        int choiceStart = correctedLine.IndexOf(CHOICE_OPTIONS_DECLARATION) + CHOICE_OPTIONS_DECLARATION.Length;
        int choiceEnd = correctedLine.IndexOf("}",choiceStart);
        
        // grab just the "1 | 2" from the above
        string rawChoices = correctedLine.Substring(choiceStart, choiceEnd - choiceStart);
        // separate choices into strings
        string[] choices = rawChoices.Split("|");

        // combine everything
        correctedLine = correctedLine.Substring(0, choiceStart - CHOICE_OPTIONS_DECLARATION.Length) + "\n";
        // show options
        for(int i = 0; i < choices.Length; i++) {
            correctedLine += $"\n{i + 1}: {choices[i].Trim()}";
        }

        return correctedLine.Trim();
    }


    public static void InjectVariables(ref string[] dialog)
    {
        for(int i = 0; i < dialog.Length; i++) {
            dialog[i] = dialog[i].Replace(PLAYER_NAME_SPECIAL, GameManager.PlayerName);
            dialog[i] = dialog[i].Replace(NEW_LINE_SPECIAL, "\n");
        }
    }


    public static string GetChoice(int choice, string dialog)
    {
        // check that the line is indeed a list of decisions
        if(!IsDecisionList(dialog)) {
            Debug.LogError($"tried to get a decision on a dialog option that's not a decision: {dialog}");
            return "";
        }

        // remove decision declaration
        dialog = dialog.Replace(DECISIONS_DECLARATION, "");
        // remove trailing "}"
        dialog = dialog.Substring(0, dialog.Length - 1);
        
        // split into decisions list
        var decisions = dialog.Split("|");

        // check selected choice in range...
        if(choice > decisions.Length) {
            Debug.LogError($"tried to get a choice that's out of range for the decisions list: {dialog}, choice: {choice.ToString()}");
            return "";
        }

        // return choice!
        return decisions[choice - 1];
    }


    public static string StripFlags(ref string dialog)
    {
        // look for {SET_FLAG: 
        int flagStart = dialog.IndexOf(SET_FLAG_SPECIAL) + SET_FLAG_SPECIAL.Length;
        
        // if not found, return null
        if(flagStart < SET_FLAG_SPECIAL.Length) return null;

        // otherwise, find the closing "}"
        int flagEnd = dialog.IndexOf("}", flagStart);
        
        // parse flag. e.g. "{SET_FLAG:flowers}" --> "flowers"
        string flag = dialog.Substring(flagStart, flagEnd - flagStart);

        // remove "{SET_FLAG:flowers}" from dialog
        string beforeFlag = dialog.Substring(0, flagStart - SET_FLAG_SPECIAL.Length);
        string afterFlag  = flagEnd < dialog.Length - 1? dialog.Substring(flagEnd) : "";
        dialog = beforeFlag + afterFlag;

        return flag.Trim();
    }
}
