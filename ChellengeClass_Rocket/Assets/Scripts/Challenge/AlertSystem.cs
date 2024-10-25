using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    // fov가 45라면 45도 각도안에 있는 aesteriod를 인식할 수 있음.
    [SerializeField] private float fov = 45f;
    // radius가 10이라면 반지름 10 범위에서 aesteriod들을 인식할 수 있음.
    [SerializeField] private float radius = 10f;
    private float alertThreshold;

    private Animator animator;
    private static readonly int blinking = Animator.StringToHash("isBlinking");

    private void Start()
    {
        animator = GetComponent<Animator>();
        // FOV를 라디안으로 변환하고 코사인 값을 계산
        alertThreshold = Mathf.Cos(fov * Mathf.Deg2Rad / 2);
    }

    private void Update()
    {
        CheckAlert();
    }

    private void CheckAlert()
    {
        // 주변 반경의 소행성들을 확인하고 이를 감지하여 Alert를 발생시킴(isBlinking -> true)
        int layerMask = LayerMask.GetMask("Aesteriod");

        var hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 0f, layerMask);

        bool check = false;

        foreach (var hit in hits)
        {
            Vector2 dir = hit.transform.position - transform.position;
            float cos = Vector2.Dot(dir.normalized, transform.up.normalized);
            if (cos >= alertThreshold)
            {
                //작동
                check = true;
                break;
            }
        }

        animator.SetBool(blinking, check);
    }
}