using UnityEngine;
using TMPro;

public class ObjectiveManager2 : MonoBehaviour
{
    [Header("UI Riferimenti")]
    // Riferimento al testo dell'obiettivo nella UI
    public TMP_Text objectiveText;

    // Indice dell'obiettivo attuale
    private int currentObjectiveIndex = 0;

    // Lista degli obiettivi
    private string[] objectives = new string[]
    {
        "Parla con il Mago al piano di sotto",
    };

    void Start()
    {
        // Imposta il primo obiettivo all'avvio
        ShowCurrentObjective();
    }

    void Update()
    {

    }

    // Mostra l'obiettivo attuale
    void ShowCurrentObjective()
    {
        if (objectiveText != null && currentObjectiveIndex < objectives.Length)
        {
            objectiveText.text = "Obiettivo: " + objectives[currentObjectiveIndex];
        }
    }

    // Passa all'obiettivo successivo
    public void NextObjective()
    {
        currentObjectiveIndex++;

        if (currentObjectiveIndex < objectives.Length)
        {
            ShowCurrentObjective();
        }
        else
        {
            objectiveText.text = "Tutti gli obiettivi completati!";
        }
    }

    // Facoltativo: per resettare gli obiettivi se vuoi riavviare
    public void ResetObjectives()
    {
        currentObjectiveIndex = 0;
        ShowCurrentObjective();
    }
}
