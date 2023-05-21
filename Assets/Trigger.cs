using _SYSTEMS_._Interaction_System_.Abstract;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Trigger<T> : MonoBehaviour, IUsable where T : Component
{
    [Title("CountDown")] 
    [ReadOnly]
    public float time;
    public float MaxTime = 3;
    public bool reverseCountDown;

    [Title("Texts")] 
    public string successText = "S";
    public string failText = "F";

    [Title("References")] 
    public Image loadingBar;
    public Image background;
    public Image Icon;
    public TextMeshProUGUI countDownText;

    [Title("Colors")] 
    public Color textColor = Color.white;
    public Color SuccessColor = Color.green;
    public Color FailColor = Color.red;

    [Title("Events")] 
    public UnityEvent OnSuccess;
    public UnityEvent OnFail;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private Coroutine _countdownCoroutine;
    protected T TheGeneric;

    private void Start()
    {
        loadingBar.fillAmount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T component))
        {
            if (_countdownCoroutine != null)
                StopCoroutine(_countdownCoroutine);
            TheGeneric = component;
            countDownText.DOFade(1, 0.15f);
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T component))
        {
            TheGeneric = null;
            if (_countdownCoroutine != null)
                StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = StartCoroutine(Countdown());
            OnExit?.Invoke();
        }
    }

    protected virtual IEnumerator Countdown()
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            loadingBar.fillAmount = time / MaxTime;
            CountDownText();
            yield return new WaitForFixedUpdate();
        }

        FailTrigger();
    }

    public virtual void Use()
    {
        if (TheGeneric == null)
            return;

        time += Time.deltaTime;
        loadingBar.fillAmount = time / MaxTime;
        CountDownText();
        if (!(time >= MaxTime)) return;

        SuccessfulTrigger();
    }

    protected virtual void SuccessfulTrigger()
    {
        loadingBar.fillAmount = 0;
        background.DOColor(SuccessColor, 0.15f).SetLoops(2, LoopType.Yoyo);
        time = 0;
        CountDownText();
        countDownText.DOFade(0, 0.15f).SetDelay(.5f);
        OnSuccess?.Invoke();

        if (successText != "")
            countDownText.text = successText;
    }

    protected virtual void FailTrigger()
    {
        _countdownCoroutine = null;
        loadingBar.fillAmount = 0;
        CountDownText();
        background.DOColor(FailColor, 0.15f).SetLoops(2, LoopType.Yoyo);
        countDownText.DOFade(0, 0.15f);
        OnFail?.Invoke();

        if (failText != "")
            countDownText.text = failText;
    }

    protected virtual void CountDownText()
    {
        if (reverseCountDown)
            countDownText.text = ((int)(MaxTime - time)).ToString();
        else
            countDownText.text = ((int)time).ToString();
    }

    public abstract void StopUse();
}