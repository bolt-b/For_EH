using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ConveyorMaterialAnimation.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>The raw material goes to the tape.</summary>
        public bool IsMaterialsReceiving
        {
            get => _isMaterialsReceiving;
            private set => _ = Set(ref _isMaterialsReceiving, value);
        }
        private bool _isMaterialsReceiving;


        /// <summary>The conveyor is running.</summary>
        public bool IsConveyorRunning
        {
            get => _isConveyorRunning;
            private set => _ = Set(ref _isConveyorRunning, value);
        }
        private bool _isConveyorRunning;


        // Commands property definition
         /// <summary>Materials start/stop command. Inverts the <see cref="IsMaterialsReceiving"/> property.</summary>
       public RelayCommand MaterialStartStopCommand { get; }
        /// <summary>Conveyor start/stop command. Inverts the <see cref="IsConveyorRunning"/> property.</summary>
        public RelayCommand ConveyorStartStopCommand { get; }


        public MainWindowViewModel()
        {
            // Command
            MaterialStartStopCommand = new RelayCommand(MaterialStartStopExecute);
            ConveyorStartStopCommand = new RelayCommand(ConveyorStartStopExecute);
        }



        /// <summary>Execute Method for <see cref="MaterialStartStopCommand"/>.</summary>
        private void MaterialStartStopExecute()
        {
            IsMaterialsReceiving = !IsMaterialsReceiving;
        }


        /// <summary>Execute Method for <see cref="ConveyorStartStopCommand"/>.</summary>
        private void ConveyorStartStopExecute()
        {
            IsConveyorRunning = !IsConveyorRunning;
        }

    }
}
