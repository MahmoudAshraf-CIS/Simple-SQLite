using UnityEngine;
using SQLite4Unity3d;


namespace Scheme
{
    public class Shot
    {
        public int PlayerId { get; set; }
        public int Score { get; set; }
        public float Distance { get; set; }
        public override string ToString()
        {
            return string.Format("[Person: Id={0}, Score={1},  Distance={2}]", PlayerId, Score, Distance);
        }

    }

}
