namespace ailib.search
{
    public interface GoalBased<T, R>
    {
        Goal<T, R> getGoal();
        void setGoal(Goal<T, R> goal);
    }
}
