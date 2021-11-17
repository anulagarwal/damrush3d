using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{

    public static DrawManager Instance = null;

    [Header("Attributes")]
    [SerializeField] float inkRemaining;
    [SerializeField] float maxInk;
    [SerializeField] Color fixColorBar;
    [SerializeField] Color fallColorBar;


    [Header("Component References")]
    [SerializeField] List<DrawingManager> dm;

    [Header("Attributes - DO NOT TOUCH")]
    [SerializeField] DrawMaterial selectedMaterial;
    [SerializeField] float currentInk;



    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentInk = maxInk;
        UIManager.Instance.UpdateInkAmount(currentInk / maxInk);

        UpdateMaterial(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateMaterial(int val)
    {
        foreach(DrawingManager d in dm)
        {
            d.gameObject.SetActive(false);
        }
        switch (val)
        {
            case 0:
                selectedMaterial = DrawMaterial.Fix;
                dm[0].gameObject.SetActive(true);
                UIManager.Instance.UpdateInkColor(fixColorBar);
                break;

            case 1:
                selectedMaterial = DrawMaterial.Fall;
                dm[1].gameObject.SetActive(true);
                UIManager.Instance.UpdateInkColor(fallColorBar);
                break;

        }
    }

    public DrawMaterial GetCurrentMaterial()
    {
        return selectedMaterial;
    }


    public void SetCurrentInk(float val)
    {
        currentInk = val;
    }
    public void ReduceCurrentInk(float val)
    {
        currentInk -= val;
        UIManager.Instance.UpdateInkAmount(currentInk / maxInk);

    }
    public float GetMaxInk()
    {
        return maxInk;
    }

    public float GetCurrentInk()
    {
        return currentInk;
    }
}
