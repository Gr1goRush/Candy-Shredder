using UnityEngine;

public class BooleanYieldInstruction : CustomYieldInstruction
    {
        private bool _result;

        public BooleanYieldInstruction(bool result)
        {
            _result = result;
        }

        public override bool keepWaiting
        {
            get { return false; }
        }

        public bool GetResult()
        {
            return _result;
        }
    }