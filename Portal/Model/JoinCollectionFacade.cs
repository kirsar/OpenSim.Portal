using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenSim.Portal.Model
{
    public class JoinCollectionFacade<TEntity, TOtherEntity, TJoinEntity> : ICollection<TEntity>
        where TJoinEntity : IJoinEntity<TEntity>, IJoinEntity<TOtherEntity>, new()
    {
        private readonly TOtherEntity ownerEntity;
        private readonly ICollection<TJoinEntity> collection;

        public JoinCollectionFacade(
            TOtherEntity ownerEntity,
            ICollection<TJoinEntity> collection)
        {
            this.ownerEntity = ownerEntity;
            this.collection = collection;
        }

        public IEnumerator<TEntity> GetEnumerator() => collection.Select(e => ((IJoinEntity<TEntity>)e).Navigation).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TEntity item)
        {
            var entity = new TJoinEntity();
            ((IJoinEntity<TEntity>)entity).Navigation = item;
            ((IJoinEntity<TOtherEntity>)entity).Navigation = ownerEntity;
            collection.Add(entity);
        }

        public void Clear() => collection.Clear();

        public bool Contains(TEntity item) => collection.Any(e => Equals(item, e));

        public void CopyTo(TEntity[] array, int arrayIndex) => this.ToList().CopyTo(array, arrayIndex);

        public bool Remove(TEntity item) => collection.Remove(collection.FirstOrDefault(e => Equals(item, e)));

        public int Count => collection.Count;

        public bool IsReadOnly => collection.IsReadOnly;

        private static bool Equals(TEntity item, TJoinEntity e) => Equals(((IJoinEntity<TEntity>)e).Navigation, item);
    }
}