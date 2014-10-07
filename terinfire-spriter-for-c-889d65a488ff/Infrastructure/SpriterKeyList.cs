using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Infrastructure
{
    public class SpriterKeyList<T> : List<T> where T : ISpriterKey
    {
        #region Properties

        private readonly Dictionary<Int32, T> _collectionByID = new Dictionary<int, T>();
        private readonly Dictionary<String, T> _collectionByName = new Dictionary<string, T>();

        #endregion

        public void Compile()
        {
            _collectionByID.Clear();
            _collectionByName.Clear();

            foreach ( T Item in this )
            {
                _collectionByID.Add( Item.ID, Item );
                _collectionByName.Add( Item.Name, Item );
            }
        }

        public T Lookup( Int32 pID )
        {
            if ( _collectionByID.ContainsKey( pID ) )
            {
                return _collectionByID[pID];
            }

            return default(T);
        }

        public T Lookup( String pName )
        {
            if ( _collectionByName.ContainsKey( pName ) )
            {
                return _collectionByName[pName];
            }

            return default( T );
        }
    }
}
