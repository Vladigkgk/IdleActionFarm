using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stack
{
    public class StackRow : MonoBehaviour
    {
        public List<StackBlock> InitRow()
        {
            var stackBlocks = new List<StackBlock>();
            stackBlocks.AddRange(GetComponentsInChildren<StackBlock>());

            return stackBlocks;
        }

    }
}