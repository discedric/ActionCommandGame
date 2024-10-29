using ActionCommandGame.Model;
using ActionCommandGame.Services.Model.Filters;

namespace ActionCommandGame.Services.Extensions.Filters
{
    internal static class PlayerFilterExtensions
    {
        public static IQueryable<Player> ApplyFilter(this IQueryable<Player> query, PlayerFilter? filter)
        {
            if (filter is null)
            {
                return query;
            }

            return query;
        }
    }
}
