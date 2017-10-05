using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
	public RectTransform[] fillRects;
	private Image[] fillImgs;
	public Image[] extraHPImgs;

    private Slider hpSlider;

    void Awake()
    {
		fillImgs = new Image[3];
        hpSlider = GetComponent<Slider>();
		for (int i = 0; i < fillRects.Length; i++) 
		{
			fillImgs[i] = fillRects[i].GetComponent<Image>();
		}

		UpdateVisible (0);
    }
		
    public void UpdateHealth(int amount)
    {
		hpSlider.value = amount;
    }

    public void UpdateMaxHealth(int _maxHealth)
    {
		if (_maxHealth == 105) 
		{
			UpdateVisible(1);
		}
		if (_maxHealth == 110)
		{
			UpdateVisible(2);
		}
    }


	public void UpdateVisible(int x)
	{
		hpSlider.fillRect = fillRects[x];

		for (int i = 0; i < fillRects.Length; i++) 
		{
			if (i == x) 
			{
				fillImgs[i].enabled = true;
			} 
			else 
			{
				fillImgs[i].enabled = false;
			}
		}
			
		x--;
		for (int i = 0; i < extraHPImgs.Length; i++) 
		{
			if (i == x) 
			{
				extraHPImgs[i].enabled = true;
			} 
			else 
			{
				extraHPImgs[i].enabled = false;
			}
		}

	}
}