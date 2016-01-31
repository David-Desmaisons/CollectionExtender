# CollectionExtender
C# collection framework


##IEnumerable Extensions

Cartesian

    public static IEnumerable<TResult> Cartesian<TResult, TSource1, TSource2>(this IEnumerable<TSource1> first,
                    IEnumerable<TSource2> second, Func< TSource1, TSource2, TResult> Agregator )
                    
FirstOrDefault

    public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, Func<T, bool> predicate)
    
    public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue)

ForEach

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T,int> action)
    
    public static bool ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, 
                                  CancellationToken iCancellationToken)

ForEachCartesian

    public static void ForEachCartesian<TSource1, TSource2>(this IEnumerable<TSource1> first,
                        IEnumerable<TSource2> second, Action<TSource1, TSource2> Do)

Index

    public static int Index<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
    
    public static int Index<T>(this IEnumerable<T> enumerable, T value)
    
Indexes

    public static IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
    
    public static IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, T value)
    
    
Zip

    public static IEnumerable<TResult> Zip<TResult, TSource1, TSource2, TSource3>(
                          this IEnumerable<TSource1> enumerable,
                          IEnumerable<TSource2> enumerable2, IEnumerable<TSource3> enumerable3,
                          Func<TSource1, TSource2, TSource3, TResult> Agregate)


##IList Extensions

AddRange

    public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> enumerable)
    
##IDictionary Extensions

FindOrCreate

         TValue FindOrCreateEntity<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> Fac)

UpdateOrCreate

        public static TValue UpdateOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> Fac,
                                                                Action<TKey, TValue> Updater)
