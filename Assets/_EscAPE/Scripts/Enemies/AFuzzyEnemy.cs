using UnityEngine;

public abstract class AFuzzyEnemy : AEnemy
{  
    /////// FUZZIFICATION (Conjuntos Difusos)

    //Hombro Izquierdo <= 30 | 30 a 60
    protected float LowHealth(float hp)
    {
        if (hp <= 30)
            return 1f;
        if (hp >= 60)
            return 0;

        return (60 - hp) / 30;
    }
    //Triangular 50 | 30 a 70
    protected float MediumHealth(float hp)
    {
        if (hp <= 30 || hp >= 70) return 0f;

        if (hp == 50f) return 1;

        if (hp < 50) return (hp - 30f) / 20f;

        else return (70f - hp) / 20f;
    }
    //Hombro Derecho >= 80 | 50 a 80
    protected float HighHealth(float hp)
    {
        if (hp <= 50) return 0;

        if (hp >= 80) return 1;

        return (hp - 50) / 30;
    }
    //Hombro Izquierdo <= 5 | 5 a 15
    protected float CloseToPlayer(float dist)
    {
        if (dist <= 5) return 1f;
        if (dist >= 15) return 0f;

        return (15 - dist) / 10f;
    }
    //Hombro Derecho >= 20 | 10 a 20
    protected float FarToPlayer(float dist)
    {
        if (dist <= 10) return 0f;
        if (dist >= 20) return 1f;

        return (dist - 10) / 10;
    }

    /////////Operaciuones difusas    
    protected float AND(float a, float b)
    {
        return Mathf.Min(a, b);
    }
    protected float OR(float a, float b)
    {
        return Mathf.Max(a, b);
    }
    protected float NOT(float a)
    {
        return 1 - a;
    }
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada