using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    private Button _thisButton;
    
    public AudioClip onSelect;

    public AudioClip onClick;
    
    private AudioSource _thisAudioSource;

    private Vector3 _originalButtonSize;

    public float ButtonSizeMultiplier;
    
    void Start()
    {
        _thisButton = gameObject.GetComponent<Button>();
        _thisAudioSource = gameObject.GetComponent<AudioSource>();
        _originalButtonSize = gameObject.transform.localScale;
    }   

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_thisButton.interactable && gameObject.activeSelf)
        {
            gameObject.transform.localScale = gameObject.transform.localScale * ButtonSizeMultiplier;
            PlayAudioSource(onSelect);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_thisButton.interactable && gameObject.activeSelf)
        {
            var scaleX = gameObject.transform.localScale.x;

            if (scaleX > _originalButtonSize.x)
            {
                gameObject.transform.localScale = gameObject.transform.localScale / ButtonSizeMultiplier;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_thisButton.interactable && gameObject.activeSelf)
        {
            StopAllSounds();

            var scale = gameObject.transform.localScale.x;

            if (scale > _originalButtonSize.x)
            {
                gameObject.transform.localScale = gameObject.transform.localScale / ButtonSizeMultiplier;
            }

            PlayAudioSource(onClick);
        }
    }
    
    private void PlayAudioSource(AudioClip toPlay)
    {
        if (_thisAudioSource.isPlaying)
        {
            _thisAudioSource.Stop();
        }

        _thisAudioSource.clip = toPlay;
        
        _thisAudioSource.Play();
    }

    private void StopAllSounds()
    {
        var all = FindObjectsOfType<AudioSource>();

        for (int x = 0; x < all.Length; x++)
        {
            if (all[x].gameObject.GetComponent<AudioSource>())
            {
                all[x].gameObject.GetComponent<AudioSource>().Stop();
            }
        }
    }
}
