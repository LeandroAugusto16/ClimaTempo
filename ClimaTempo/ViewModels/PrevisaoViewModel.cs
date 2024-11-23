using ClimaTempo.Models;
using ClimaTempo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClimaTempo.ViewModels
{
    public partial class PrevisaoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cidade;
        [ObservableProperty]
        private string estado;
        [ObservableProperty]
        private string condicao;
        [ObservableProperty]
        private string condicao_desc;
        [ObservableProperty]
        private double max;
        [ObservableProperty]
        private double min;
        [ObservableProperty]
        private string indiceuv;

        private Previsao previsao;
        private List<Clima> proximosdias;
        private Previsao proxPrevisao;
        //Dados da pesquisa
        [ObservableProperty]
        private string cidade_pesquisada;
        
        //Dados da lista de cidades
        [ObservableProperty]
        private List<Cidade> cidadeList;

        public ICommand BuscarPrevisaoCommand { get; }
        public ICommand BuscarCidadeCommand { get; }

        public PrevisaoViewModel()
        {
            BuscarPrevisaoCommand = new Command(BuscarPrevisao);
            BuscarCidadeCommand = new Command(BuscarCidades);
        }

        public async void BuscarPrevisao()
        {
            //Busca dados da previsão de uma cidade
            previsao = await new PrevisaoService().GetPrevisaoById(244);
            Cidade = previsao.Cidade;
            Max = previsao.clima[0].Max;
            Min = previsao.clima[0].Min;

            //Busca dados da previsão para os próximos dias
            proxPrevisao = await new PrevisaoService().GetPrevisaoForXDaysById(244, 3);
            proximosdias = proxPrevisao.clima;
        }
        public async void BuscarCidades()
        {
            cidadeList = new List<Cidade>();
            cidadeList = await new CidadeService().GetCidadeByName(cidade_pesquisada);
        }
    }
}
