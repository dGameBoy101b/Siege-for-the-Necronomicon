using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/**
 * @author Rhys Mader 33705134
 * @date 13/05/2021
 * A class that links an option slider with an audio mixer.
 */
public sealed class VolumeSlider : MonoBehaviour
{
	[SerializeField()]
	[Tooltip("The audio mixer to change the volume of.")]
	public AudioMixer MIXER;
	
	[SerializeField()]
	[Tooltip("The name of the exposed volume parameter to target.")]
	public string MIXER_NAME;
	
	[SerializeField()]
	[Tooltip("The slider whose value should be sent to the linked mixer group.")]
	public Slider SLIDER;
	
	/**
	 * Update the volume of the linked mixer with the current value of the linked slider.
	 */
	public void updateMixer()
	{
		this.MIXER.SetFloat(this.MIXER_NAME, this.SLIDER.value - 80);
	}
}
