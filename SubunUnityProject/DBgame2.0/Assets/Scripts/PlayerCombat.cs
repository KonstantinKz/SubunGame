using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask attackLayer;
    public PlayerMovement playerMovement;
    public Text scoreText;
    public Text gameOverScore;
    public Text highScoreText;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float score;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        scoreText.text = score.ToString();
        gameOverScore.text = "YOUR SCORE: \n" + score.ToString();
    }

    private void Attack()
    {
        //animation
        animator.SetTrigger("Attack");

        //detect souls in range of attack
        Collider2D[] hitSouls = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        foreach (var soul in hitSouls)
        {
            soul.GetComponent<LampFall>().Damage();

            playerMovement.jump = true;
            animator.SetTrigger("Jump");
            score += 10;
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
