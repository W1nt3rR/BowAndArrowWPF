using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowAndArrowWPF
{
    public static class EventHub
    {
        public static Action moveBowUp;
        public static void MoveBowUp()
        {
            moveBowUp?.Invoke();
        }

        public static Action moveBowDown;
        public static void MoveBowDown()
        {
            moveBowDown?.Invoke();
        }

        public static Action moveBaloonsUp;
        public static void MoveBalloonsUp()
        {
            moveBaloonsUp?.Invoke();
        }

        public static Action moveArrowRight;
        public static void MoveArrowRight()
        {
            moveArrowRight?.Invoke();
        }

        public static Action removeArrows;
        public static void RemoveArrows()
        {
            removeArrows?.Invoke();
        }

    }
}
