using UnityEngine;

public class LaserUnicorn : MonoBehaviour

{

    public float angleAim = 30;
    private Transform target = null;
    public int damage = 10;
    
    void Update()
    {
        LineRenderer l = GetComponentInChildren<LineRenderer>();
        if (target != null)
        {
            Crosshairs.Get().SetAim(target.position);
            Vector3 toEnemy = target.position - transform.position;
            toEnemy.y = 0;

            float angleEmeny = Vector3.Angle(transform.forward, toEnemy.normalized);
            if(angleEmeny > 100)
            {
                l.SetPosition(1,Vector3.zero);
                l.SetPosition(0, Vector3.zero);
                target = null;
            }
            else
            {
               
              l.SetPosition(1, target.position);
              l.SetPosition(0, l.transform.position);

                //faire des dégats
                if (Time.time > 1)
                {
                    //attaquer l'ennemi enlever 10 dégats par seconde
                    target.GetComponent<EnemyStats>().RemoveHealth(damage);
                }

            }
        }

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
            //On récupère l'angle de notre notre enemi
            float angleEmeny = Vector3.Angle(transform.forward, toEnemy.normalized);
            if (angleEmeny < angleMin)
            {
                target = enemy.transform;
                angleMin = angleEmeny;               
            }
           
        }
    }
}
