using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenEffects : MonoBehaviour
{
	public Image abilityEffect;
	private Color _effectColor;
	private float _effectColorAlpha = 0.05f;
	private float _fadeSpeed = 0.01f;

	private bool _isUsingPower = false;

	void Update ()
	{
		EffectFlash ();
	}

	public void EffectFlash ()
	{
		if (Master.gestureController.IsAnyHandClenched && Master.gestureController.BothHandsUp && !_isUsingPower) 
		{
			if (Master.gestureController.IsLeftFistPower) 
			{
				_effectColor = Master.colorManager.powerLeft;
			}
			else if (Master.gestureController.IsRightFistPower) 
			{
//				_effectColor = Master.colorManager.powerRight;
				_effectColor = Master.colorManager.powerLeft;
			}

			_effectColor.a = _effectColorAlpha;
			EffectFadeIn ();
		}
		else if (!Master.gestureController.IsAnyHandClenched && _isUsingPower) 
		{
			EffectFadeOut ();
		}
	}

	void EffectFadeIn ()
	{
		StopCoroutine ("FadeOut");
		StartCoroutine ("FadeIn");
	}

	void EffectFadeOut ()
	{
		StopCoroutine ("FadeIn");
		StartCoroutine ("FadeOut");
	}

	IEnumerator FadeIn ()
	{
		_isUsingPower = true;
		while (abilityEffect.color != _effectColor) 
		{
			abilityEffect.color = Color.Lerp (abilityEffect.color, _effectColor, _fadeSpeed);
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}

	IEnumerator FadeOut ()
	{
		_isUsingPower = false;
		while (abilityEffect.color != Color.clear)
		{
			abilityEffect.color = Color.Lerp (abilityEffect.color, Color.clear, _fadeSpeed);
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}
}