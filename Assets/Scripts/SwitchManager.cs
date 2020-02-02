using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager Instance;

    public Switch[] Switches;

    private int m_switchCounter;

    private int m_switchSize;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_switchCounter = 0;
        m_switchSize = Switches.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_switchCounter == m_switchSize)
        {
            SceneManager.LoadSceneAsync("Cutscene_End");
        }
    }

    public void UpdateSwitchCounter()
    {
        m_switchCounter++;
    }
}
