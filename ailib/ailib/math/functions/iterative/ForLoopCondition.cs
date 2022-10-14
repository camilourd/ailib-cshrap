using ailib.math.logic;

namespace ailib.math.functions.iterative
{
    public class ForLoopCondition<T> : Predicate<T>
    {
        protected int iter = -1;
        protected int start = 0;
        protected int end = 0;
        protected int inc = 1;

        public ForLoopCondition(int start, int end, int inc)
        {
            this.start = start;
            this.end = end;
            this.inc = inc;
            this.iter = start - inc;
        }

        public ForLoopCondition(int start, int end)
        {
            this.start = start;
            this.iter = start - inc;
            this.end = end;
        }

        public ForLoopCondition(int end)
        {
            this.end = end;
        }

        public void set(int start, int end, int inc)
        {
            this.start = start;
            this.end = end;
            this.inc = inc;
            this.iter = start - inc;
        }

        public override bool evaluate(T obj)
        {
            iter += inc;
            return (iter < end);
        }

        public int getStart()
        {
            return start;
        }

        public int getInc()
        {
            return inc;
        }

        public int getEnd()
        {
            return end;
        }

        public override void init()
        {
            iter = start - inc;
        }
    }
}
