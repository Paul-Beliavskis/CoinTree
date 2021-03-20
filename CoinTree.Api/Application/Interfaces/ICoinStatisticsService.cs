using System.Threading;
using System.Threading.Tasks;
using CoinTree.Application.Dtos;
using CoinTree.Application.Enums;

namespace CoinTree.Application.Interfaces
{
    public interface ICoinStatisticsService
    {
        Task<CoinStatsDto> GetCoinStaticsticsAsync(CoinType coinType, CancellationToken cancelationToken);
    }
}
