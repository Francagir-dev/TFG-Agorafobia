
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public class ShowTaskUI : MonoBehaviour
{
    public LocalizedStringTable stringTable = new LocalizedStringTable { TableReference = "Tasks" };
    public TextMeshProUGUI taskText;
    private int phobiaLevel;
    string finalText;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (PersistentManager.infoManager._session.phobiaLVL > 2) phobiaLevel = 2;
            else phobiaLevel = PersistentManager.infoManager._session.phobiaLVL;
                
        }
        catch (Exception e)
        {
            phobiaLevel = 2;
        }
    }

    void OnEnable()
    {
        stringTable.TableChanged += LoadStrings;
    }

    void OnDisable()
    {
        stringTable.TableChanged -= LoadStrings;
    }

    void LoadStrings(StringTable stringTable)
    {
        finalText = GetLocalizedString(stringTable, "Task2");
        Debug.Log(finalText);
        ChangeTextMeshpro();
    }

    static string GetLocalizedString(StringTable table, string entryName)
    {
        // Get the table entry. The entry contains the localized string and Metadata
        StringTableEntry entry = table.GetEntry(entryName);
        return entry.GetLocalizedString(); // We can pass in optional arguments for Smart Format or String.Format here.
    }

    public void ChangeTextMeshpro()
    {
        taskText.text = finalText;
    }
}