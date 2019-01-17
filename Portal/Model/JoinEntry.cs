namespace OpenSim.Portal.Model
{
    public interface IJoinEntity<T>
    {
        T Navigation { get; set; }
    }

   
    public abstract class JoinEntity<T> : IJoinEntity<T>
    {
        public abstract T Navigation { get; set; }
    }

    public class JoinEntity<T1, T2> : JoinEntity<T1>, IJoinEntity<T2>
    {
        public int FirstId { get; set; }
        public T1 First { get; set; }

        public override T1 Navigation
        {
            get => First;
            set => First = value;
        }

        public int SecondId { get; set; }
        public T2 Second { get; set; }
        T2 IJoinEntity<T2>.Navigation
        {
            get => Second;
            set => Second = value;
        }
    }
}
