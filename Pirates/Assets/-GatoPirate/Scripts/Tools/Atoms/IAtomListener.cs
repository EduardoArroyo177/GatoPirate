namespace UnityAtoms {
    public interface IAtomListener<T1, T2> {
        void OnEventRaised(T1 first, T2 second);
    }
    public interface IAtomListener<T1, T2, T3>
    {
        void OnEventRaised(T1 first, T2 second, T3 third);
    }
    public interface IAtomListener<T1, T2, T3, T4> {
        void OnEventRaised(T1 first, T2 second, T3 third, T4 fourth);
    }
}