using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ConveyorMaterialAnimationEH.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {

        #region File for properties store
        private bool _isMaterialsReceiving;
        private bool _isConveyorRunning;
        private Portion _currentPortion;
        private double _speed;
        private double _conveyorLength;
        private bool _isMaterialAtEnd;
        private bool _conveyor_On;
        private RelayCommand<bool> _onOffConveyorCommand;
        #endregion


        ///// <summary> Conveyor On property.</summary>
        //public bool Conveyor_On
        //{
        //    get => _conveyor_On;
        //    set => _ = Set(ref _conveyor_On, value);
        //}

        /// <summary>The raw material goes to the tape.</summary>
        public bool IsMaterialsReceiving
        {
            get => _isMaterialsReceiving;
            set => _ = Set(ref _isMaterialsReceiving, value);
        }


        /// <summary>The conveyor is running.</summary>
        public bool IsConveyorRunning
        {
            get => _isConveyorRunning;
            set => _ = Set(ref _isConveyorRunning, value);
        }

        public MainWindowViewModel()
        {
            BindingOperations.EnableCollectionSynchronization(Portions, ((ICollection)Portions).SyncRoot);
            timer = new Timer(OnTimer, null, 10, 10);
        }

        public ObservableCollection<Portion> Portions { get; }
            = new ObservableCollection<Portion>();

        public Portion CurrentPortion { get => _currentPortion; private set => Set(ref _currentPortion, value); }

        private readonly Timer timer;
        private void OnTimer(object state)
        {
            if (IsConveyorRunning)
            {
                lock (((ICollection)Portions).SyncRoot)
                {
                    List<Portion> remove = new List<Portion>();
                    foreach (Portion portion in Portions)
                    {
                        if (portion == CurrentPortion)
                        {
                            double length = (DateTime.Now - portion.StartTime).TotalSeconds * Speed;
                            if (length > ConveyorLength)
                            {
                                length = ConveyorLength;
                            }
                            portion.Length = length;
                        }
                        else
                        {
                            double begin = (DateTime.Now - portion.StartTime).TotalSeconds * Speed;
                            if (begin > ConveyorLength)
                            {
                                remove.Add(portion);
                            }
                            else
                            {
                                portion.Begin = begin;
                            }
                        }
                    }
                    remove.ForEach(portion => Portions.Remove(portion));

                    IsMaterialAtEnd = Portions.Any(portion => portion.Begin + portion.Length >= ConveyorLength);
                }
            }
        }

        public bool IsMaterialAtEnd { get => _isMaterialAtEnd; set => Set(ref _isMaterialAtEnd, value); }
        public double Speed { get => _speed; set => Set(ref _speed, value); }
        public double ConveyorLength { get => _conveyorLength; set => Set(ref _conveyorLength, value); }

        public override void RaisePropertyChanged<T>([CallerMemberName] string propertyName = null, T oldValue = default, T newValue = default, bool broadcast = false)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);

            if (propertyName == nameof(IsMaterialsReceiving))
            {
                lock (((ICollection)Portions).SyncRoot)
                {
                    bool isnew = Equals(newValue, true);
                    if (isnew)
                    {
                        Portion portion = new Portion()
                        {
                            StartTime = DateTime.Now
                        };
                        CurrentPortion = portion;
                        Portions.Add(portion);
                    }
                    else
                    {
                        Portion portion = CurrentPortion;
                        if (portion.Length == 0)
                        {
                            Portions.Remove(portion);
                        }
                        else
                        {
                            portion.StartTime = DateTime.Now;
                        }
                        CurrentPortion = null;
                    }
                }
            }
            else if (propertyName == nameof(IsConveyorRunning))
            {
                if (Equals(newValue, true))
                {
                    lock (((ICollection)Portions).SyncRoot)
                    {
                        foreach (Portion portion in Portions)
                        {
                            if (portion == CurrentPortion)
                            {
                                portion.StartTime = DateTime.Now.AddSeconds(-portion.Length / Speed);
                            }
                            else
                            {
                                portion.StartTime = DateTime.Now.AddSeconds(-portion.Begin / Speed);
                            }
                        }
                    }
                }

            }
        }

        public RelayCommand<bool> OnOffConveyorCommand => _onOffConveyorCommand
            ?? (_onOffConveyorCommand = new RelayCommand<bool>(OnOffConveyorExecute));

        /// <summary>Метод включения-выключения конвейера.</summary>
        /// <param name="obj"></param>
        private void OnOffConveyorExecute(bool isTurnOn)
        {
            MessageBox.Show($"Команда \"{(isTurnOn? "Включить":"Выключить")} ковейер\".");
            IsConveyorRunning = isTurnOn;
        }
    }


}
