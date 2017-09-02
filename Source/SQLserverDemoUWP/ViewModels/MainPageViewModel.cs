using SQLserverDemoUWP.Models;
using SQLserverDemoUWP.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SQLserverDemoUWP.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        readonly NorthwindRepository Repository;

        private string _SearchString;

        public string SearchString
        {
            get { return _SearchString; }
            set
            {
                if (_SearchString != value)
                {
                    _SearchString = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<Product> _Result;

        public List<Product> Result
        {
            get { return _Result; }
            set { _Result = value; RaisePropertyChanged(); }
        }

        private bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _ResultText;

        public string ResultText
        {
            get { return _ResultText; }
            set
            {
                if (_ResultText != value)
                {
                    _ResultText = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainPageViewModel()
        {
            Repository = new NorthwindRepository();
        }


        public async void OnSearch()
        {
            try
            {
                IsLoading = true;
                Result = null;
                Result = await Repository.SearchProducts(SearchString);

                if (Result.Any())
                    ResultText = $"Resutls for {SearchString}:";
                else
                    ResultText = $"Search string '{SearchString}' returned no items";
            }
            catch (Exception)
            {
                //ToDo
            }
            finally
            {
                SearchString = null;
                IsLoading = false;
            }
        }

        public void RaisePropertyChanged([CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
