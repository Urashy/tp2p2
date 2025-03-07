﻿using System;
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



        private Serie serieToAdd;
public Serie SerieToAdd
{
    get => serieToAdd;
    set
    {
        if (serieToAdd != value)
        {
            serieToAdd = value;

            OnPropertyChanged(nameof(SerieToAdd));
        }
    }
}



        public IRelayCommand BtnAjt { get; }


        public CreerSerieViewModel()
        {

            GetDataOnLoadAsync();
            serieToAdd = new Serie();
            BtnAjt = new RelayCommand(ActionSetConversion);
        }


        

        private async void ActionSetConversion()
        {
            if (serieToAdd.Titre == null || serieToAdd.Titre == "") 
            {
                MessageAsync("Erreur", "Le titre est mal renseigné");
            }else if(serieToAdd.Resume == null || serieToAdd.Resume == "")
            {
                MessageAsync("Erreur", "Le resumé est mal renseigné");
            }
            else if (serieToAdd.NbSaisons == null || serieToAdd.NbSaisons ==0)
            {
                MessageAsync("Erreur", "Le nombre de saisons est mal renseigné");
            }
            else if (serieToAdd.NbEpisodes== null || serieToAdd.NbEpisodes==0)
            {
                MessageAsync("Erreur", "Le nombre d'épisodes est mal renseigné");
            }
            else if (serieToAdd.AnneeCreation== null || serieToAdd.AnneeCreation==0)
            {
                MessageAsync("Erreur", "L'année de création est mal renseigné");
            }
            else if (serieToAdd.Network== null || serieToAdd.Network == "")
            {
                MessageAsync("Erreur", "Le network est mal renseigné");
            }
            else
            {


                WSService service = new WSService("http://localhost:5087/api/");
                bool result = await service.PutSerieAsync("Series",serieToAdd);
                if (result)
                {
                    MessageAsync("Succès", "Série crée avec succès");
                    GetDataOnLoadAsync();
                }
                else
                {
                    MessageAsync("Erreur", "L'ajout de la série a échoué");
                }
            }
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
            List<Serie> result = await service.GetSeriesAsync("Series");
            if (result == null)
            {
                MessageAsync("Erreur", "API non disponible");
            }
            else
            {
                Series = new ObservableCollection<Serie>(result);
                Console.WriteLine("Donnée récupérées depuis l'api");
                foreach (Serie serie in result) 
                {
                    Console.WriteLine(serie.Titre);
                }
                Console.WriteLine("--------------------------------");
            }
        }




    }
}
