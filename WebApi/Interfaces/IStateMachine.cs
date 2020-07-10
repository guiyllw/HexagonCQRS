using Stateless;

namespace WebApi.Interfaces
{
    public interface IStateMachine<TType, TState, TTrigger>
    {
        StateMachine<TState, TTrigger> BuildFor(TType type);
    }
}
