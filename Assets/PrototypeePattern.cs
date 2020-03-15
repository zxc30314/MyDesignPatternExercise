using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PrototypeePattern : MonoBehaviour
{
    public const int Count = 100;
    public int[] number;
    // Start is called before the first frame update
    void Start()
    {
        number = new int[Count];
        for (int i = 0; i < number.Length; i++)
        {
            number[i] = i;
        }
       int[] tempNamber=(int[]) number.Clone();
    }

}
