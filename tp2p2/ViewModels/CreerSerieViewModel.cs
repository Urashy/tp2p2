using System;
using System.Collections.Generic;
using tp2p2.Models;
using System.Collections.ObjectModel;
using tp2p2.Services;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace tp2p2.ViewModels
{
    public class CreerSerieViewModel : ObservableObject
    {
        private ObservableCollection<Serie> _series;

        public ObservableCollection<Serie> Series
        {
            get => _series;
            set
            {
                if (_series != value)
                {
                    _series = value;
                    OnPropertyChanged();
                }
            }
        }


        public IRelayCommand BtnAjt { get; }


        public CreerSerieViewModel()
        {
            GetDataOnLoadAsync();
            BtnAjt = new RelayCommand(ActionSetConversion);
        }


        

        private void ActionSetConversion()
        {
            
        }

        private async void MessageAsync(string m1, string m2)
        {
            try
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = m1,
                    Content = m2,
                    CloseButtonText = "ok",
                    XamlRoot = App.MainRoot.XamlRoot  // Utilisation de App.MainRoot
                };

                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                // En cas d'erreur, on affiche dans la console pour le débogage
                Console.WriteLine($"Erreur lors de l'affichage du dialogue : {ex.Message}");
            }
        }


        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("http://localhost:5087/api/");
            List<Serie> result = await service.GetDevisesAsync("Series");
            if (result == null)
            {
                MessageAsync("Erreur", "API non disponible");
            }
            else
            {
                Series = new ObservableCollection<Serie>(result);
            }
        }




    }
}
