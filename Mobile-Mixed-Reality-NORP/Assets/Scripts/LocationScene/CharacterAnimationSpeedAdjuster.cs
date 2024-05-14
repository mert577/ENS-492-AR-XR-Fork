using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterAnimationSpeedAdjuster : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    [SerializeField] private float walkTime;
    [SerializeField] AudioClip[] FootstepAudioClips;
    private int _animIDSpeed;
    // private float _animationBlend;


    private Transform targetPosition;
    private float speed;


    private void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("TargetPosition").transform;
        // Get the integer ID of the "Speed" parameter
        _animIDSpeed = Animator.StringToHash("Speed");
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        StartCoroutine(MoveToTargetPositionRoutine());
    }

    private IEnumerator MoveToTargetPositionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(walkTime);
            GetComponent<LookAtCamera>().enabled = false;
            if (targetPosition != null)
            {
                Vector3 direction = (targetPosition.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, targetPosition.position);

                Debug.Log(distance);
                

                // If the animation blend is low, character just stops
                if (distance < 1f)
                {
                    distance = 0f;
                    speed = 0f;
                }
                else
                {
                    Vector3 velocity = direction * (distance / walkTime);
                    _rigidbody.velocity = velocity;
                    speed = distance / 5f;
                    speed = (1.6f + 1.6f + speed) / 3;
                    transform.LookAt(targetPosition.position);
                }

                // Set the "Speed" parameter in the animator
                _animator.SetFloat(_animIDSpeed, speed);
            }

            yield return new WaitForSeconds(walkTime);
            _rigidbody.velocity = Vector3.zero;
            _animator.SetFloat(_animIDSpeed, 0f);
            GetComponent<LookAtCamera>().enabled = true;
        }
    }

    private void Update()
    {
        //transform.position += new Vector3(0, 0, 0.004f);



    }



    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, 0.5f);
            }
        }
    }
}
