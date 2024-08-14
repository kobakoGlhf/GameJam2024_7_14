using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiTimeLimit : MonoBehaviour
{
    [SerializeField] Image _circle;
    [SerializeField] Text _text;

    [SerializeField] float _currentTime = 1.0f;
    [SerializeField] float _maxTime = 3.0f;

    bool _isInstantiate = false;

    Queue<GameObject> _createdTimer=new Queue<GameObject>();
    public static bool _timerStop;

    public static GameObject _recentObject;
    private void Start()
    {
        if (_isInstantiate)
        {
            _recentObject = gameObject;
        }
        _timerStop = true;
    }
    void Update()
    {
        if (_isInstantiate&&_timerStop)
        {
            // �^�C�}�[���Z
            _currentTime -= Time.deltaTime;

            // UI�ɕ\��
            // �G���[�`�F�b�N
            if (_circle == null)
            {
                Debug.LogWarning($"'{_circle}' is null.", this.gameObject);
            }
            if (_text == null)
            {
                Debug.LogWarning($"'{_text}' is null.", this.gameObject);
            }
            // �又���Bnull�Ȃ��B
            else
            {
                // �T�[�N���Q�[�W
                _circle.fillAmount = _currentTime / _maxTime;

                // �e�L�X�g
                _text.text = _currentTime.ToString("0.##") + "s";

                // �j��
                if (_currentTime <= 0.0f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// �O�����炱�̊֐����Ăяo���āA�^�C�}�[�\�����J�n�B
    /// </summary>
    /// <param name="time">���b�̃^�C�}�[�H</param>
    /// <param name="pos">�ǂ��ɕ\������H</param>
    /// /// <param name="canvas">�e�ƂȂ�L�����o�X</param>
    public void Display(GameObject originalObject,float time, Vector2 pos, GameObject canvas)
    {
        GameObject obj;
        obj = Instantiate(originalObject, pos, Quaternion.identity, canvas.transform); //�C���X�^���X�쐬
        //�ݒ荀��
        obj.SetActive(true); //�L���ɂ���
        UiTimeLimit uiTimeLimit = obj.GetComponent<UiTimeLimit>();
        uiTimeLimit._isInstantiate = true; //�C���X�^���X���[�h

        //�����l�����
        uiTimeLimit._maxTime = time;
        uiTimeLimit._currentTime = time;

        //�ǉ��@delete�p�ɐ�������obj��Queue�ɓ����
        _createdTimer.Enqueue(obj);
    }
    public void DestroyTimer()
    {
        if (_createdTimer.Count != 0)
        {
            Destroy(_createdTimer.Dequeue());
        }
    }
    IEnumerator FadeOut(float timer)
    {
        var image= GetComponent<Image>();
        float colorCangeTimer = image.color.a / timer;
        while (timer > 0&& image != null)
        {
            timer -= Time.deltaTime;
            image.color -= new Color(0, 0, 0, colorCangeTimer * Time.deltaTime);
            _text.color -= new Color(0, 0, 0, colorCangeTimer * Time.deltaTime);
            yield return null;
        }
    }
}
