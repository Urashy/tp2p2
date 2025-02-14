using tp2p2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp2p2.Services
{
    internal interface IService
    {
        Task<List<Serie>> GetSeriesAsync(string nomControleur);
        Task<Serie> GetSerieAsync(string nomControleur, int serieid);
        Task<bool> PutSerieAsync(string nomControleur, Serie serie);
        Task<bool> DeleteSerieAsync(string nomControleur, int serieid);
        Task<bool> UpdateSerieAsync(string nomControleur, Serie serie);
    }
}