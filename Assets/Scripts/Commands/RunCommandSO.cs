using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class RunCommnandSO : CommandSO
{
    public override void Execute(Unit user, Unit target)
    {
        SceneManager.LoadScene("field");
    }

}
