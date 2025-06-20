using WebApplication2.Models.Request;

namespace WebApplication2.Models.Request
{
    public interface IQueryDataById<TId>
    {
        void SetId(TId id);
        TId GetId();
    }

    public class QueryDataById<TId> : IQueryDataById<TId>
    {
        private TId id;

        public QueryDataById()
        {
        }

        public QueryDataById(TId id)
        {
            this.id = id;
        }

        public void SetId(TId id)
        {
            this.id = id;
        }

        public TId GetId()
        {
            return this.id;
        }
    }
}
