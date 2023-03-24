using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
	[SerializeField] Transform camTransform;
	[SerializeField] float shakeDuration;
	[SerializeField] public float shakeAmount;

	float shakingTime = 0f;
	Vector3 originalPos;

    private void Awake()
    {
		originalPos = camTransform.localPosition;
    }

    public void Shake()
    {
		shakingTime = shakeDuration;
    }

	void Update()
	{
		if (shakingTime > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakingTime -= Time.deltaTime;
		}
		else
		{
			shakingTime = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}