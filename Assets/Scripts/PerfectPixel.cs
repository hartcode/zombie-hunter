using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PerfectOverride
{
	public int referenceOrthographicSize;
	public float referencePixelsPerUnit;
}

public class PerfectPixel : MonoBehaviour
{
	public int referenceOrthographicSize;
	public float referencePixelsPerUnit;
	public List<PerfectOverride> overrides;

	private int lastSize = 0;

	// Use this for initialization
	void Start()
	{
		UpdateOrthoSize();
	}

	PerfectOverride FindOverride(int size)
	{
		return overrides.FirstOrDefault(x => x.referenceOrthographicSize == size);
	}

	void UpdateOrthoSize()
	{
		lastSize = Screen.height;

		// first find the reference orthoSize
		float refOrthoSize = (referenceOrthographicSize / referencePixelsPerUnit) * 0.5f;

		// then find the current orthoSize
		var overRide = FindOverride(lastSize);
		float ppu = overRide != null ? overRide.referencePixelsPerUnit : referencePixelsPerUnit;
		float orthoSize = (lastSize / ppu) * 0.5f;

		// the multiplier is to make sure the orthoSize is as close to the reference as possible
		float multiplier = Mathf.Max(1, Mathf.Round(orthoSize / refOrthoSize));

		// then we rescale the orthoSize by the multipler
		orthoSize /= multiplier;

		// set it
		this.GetComponent<Camera>().orthographicSize = orthoSize;

		Debug.Log(lastSize + " " + orthoSize + " " + multiplier + " " + ppu);
	}

	// Update is called once per frame
	void Update()
	{
		#if UNITY_EDITOR
		if (lastSize != Screen.height)
		UpdateOrthoSize();
		#endif
	}
}