using UnityEngine;
using UnityEngine.Events;

public class LaserUnicorn : MonoBehaviour

{

    public float angleAim = 30;
    private Transform target = null;
    public int damage = 50;
    public float fireTime = 0;
    
    void Update()
    {
        //on est sut la licorne on récupère le lineRenderer de la boule qui est une fille de la licorne
        LineRenderer l = GetComponentInChildren<LineRenderer>();
        if (target != null)
        {
           
            Vector3 toEnemy = target.position - transform.position;
            toEnemy.y = 0;

            float angleEmeny = Vector3.Angle(transform.forward, toEnemy.normalized);
            float distance = toEnemy.magnitude;
            //si il a plus de vie, s'il est trop loin ou s'il y a plus d'angle
            if (angleEmeny > 100 || distance > 50 || target.GetComponent<EnemyStats>().GetHealth() <=0) 
            {
                //on (ré)initialise notre position et celle de la cible à 0
                l.SetPosition(1,Vector3.zero);
                l.SetPosition(0, Vector3.zero);
                target = null;
                //on réinitialise l'autAim
                Crosshairs.Get().ResetAim();
            }
            else
            {
                l.SetPosition(1, target.position);
                l.SetPosition(0, l.transform.position);
                //on utilise l'autoAim de martin a partir de notre boule
                Crosshairs.Get().SetAim(target.position);
                    
                if ((Time.time - fireTime) >.1f)
                {
                    //attaquer l'ennemi enlever 10 dégats par seconde
                    target.GetComponent<EnemyStats>().RemoveHealth(damage * .1f);
                    fireTime = Time.time;
                    EventManager.TriggerEvent("unicorn");
                    Debug.Log("FIRE !!");
                }

            }
        }

        //on utilise la molette de la souris pour lancer le laser
        if (Input.GetButton("Fire3"))
        {
            findTarget();
        }
    }

    public void findTarget()
    {
      
        EnemyStats[] enemies = FindObjectsOfType(typeof(EnemyStats)) as EnemyStats[];
        float angleMin = angleAim;
        //on parcours tous les enemies
        foreach (EnemyStats enemy in enemies)
        {
            //on soustrait la distance de l'ennemy avec notre position de la licorne
            Vector3 toEnemy = enemy.transform.position - transform.position;
            toEnemy.y = 0;
            //On récupère l'angle de notre notre enemie
            float angleEmeny = Vector3.Angle(transform.forward, toEnemy.normalized);
            float distance = toEnemy.magnitude;
            //si l'angle de l'enemi est inférieur a l'angle minimum et que la distance est de moins de 50 et que l'enemie est toujours en vie 
            if (angleEmeny < angleMin && distance < 50 && enemy.GetComponent<EnemyStats>().GetHealth() > 0)
            {
                target = enemy.transform;
                angleMin = angleEmeny;               
            }
           
        }
    }
}
