namespace ailib.search
{
    public class BasicGoalBased<T, R> : GoalBased<T, R>
    {
        protected Goal<T, R> goal;

        public Goal<T, R> getGoal()
        {
            return goal;
        }

        public virtual void setGoal(Goal<T, R> goal)
        {
            this.goal = goal;
        }
    }
}
