using UnityEngine;

public class LaserUnicorn : MonoBehaviour

{

    public float angleAim = 30;
     private Transform target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Crosshairs.Get().SetAim(target.position);
            Vector3 toEnemy = target.position - transform.position;

            float angleEmeny = Vector3.Angle(transform.forward, toEnemy.normalized);
            if(angleEmeny > 100)
            {
                target = null;
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
