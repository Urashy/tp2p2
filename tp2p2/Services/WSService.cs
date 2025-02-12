﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using tp2p2.Models;
using Windows.Web.Http;
using System.Net.Http.Json;

namespace tp2p2.Services
{
    public class WSService : IService
    {
        private System.Net.Http.HttpClient httpClient;

        public WSService(string url)
        {
            httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json") // Fix le typo "application./json"
            );
        }

        public async Task<List<Serie>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<Serie>>(nomControleur);
            }
            catch (Exception ex)
            {
                // Ajouter un breakpoint ici pour voir l'exception
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }

}