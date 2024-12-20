﻿using ClimaTempo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class PrevisaoService
    {
       private HttpClient client;
       private Previsao previsao;
       private JsonSerializerOptions options;

        Uri uri = new Uri("https://brasilapi.com.br/api/cptec/v1/clima/previsao/");

        public PrevisaoService()
        {
            client = new HttpClient();
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<Previsao> GetPrevisaoById(int cityCode, int days)

        {
            cityCode = 244;
            days = 3;
            Uri requesturi = new Uri($"{uri}/{cityCode}/{days}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsao = JsonSerializer.Deserialize<Previsao>(content, options);
                }
            }
            catch (Exception ex)
            { 
                 Debug.WriteLine(ex.Message);       
            }
            return previsao;
        }

        internal async Task<Previsao> GetPrevisaoById(int v)
        {
            throw new NotImplementedException();
        }

        internal async Task<Previsao> GetPrevisaoForXDaysById(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
