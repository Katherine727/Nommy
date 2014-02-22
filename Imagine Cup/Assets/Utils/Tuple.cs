namespace Assets.Utils {
    public class Tuple<T1, T2> : IPair<T1,T2> {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }

        public Tuple(T1 item1, T2 item2) {
            Item1 = item1;
            Item2 = item2;
        }

        public static Tuple<T1,T2> New(T1 item1,T2 item2){
            return new Tuple<T1, T2>(item1, item2);
        }
    }
}
