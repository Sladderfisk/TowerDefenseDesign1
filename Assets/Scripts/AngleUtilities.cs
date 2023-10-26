using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that holds useful functions for angle calculations.
/// </summary>
public class AngleUtilities : MonoBehaviour
{
	/// <summary>
	/// Gets the signed difference between two angle clamped with maxRotationAngle.
	/// </summary>
	/// <param name="angle1">Angle from in degrees</param>
	/// <param name="angle2">Angle to in degrees</param>
	/// <param name="maxRotationAngle">The value to clamp the result between</param>
	/// <returns>The signed difference</returns>
	public static float GetAngleDiff(float angle1, float angle2, float maxRotationAngle)
	{
		float angleDiff = angle1 - angle2;
		if (angleDiff > 90) { angleDiff -= 360; }
		if (angleDiff < -270) { angleDiff += 360; }

		// Clamp so you can not rotate faster than maxRotationAngle
		angleDiff = Mathf.Clamp(angleDiff, -maxRotationAngle, maxRotationAngle);

		return angleDiff;
	}
}
