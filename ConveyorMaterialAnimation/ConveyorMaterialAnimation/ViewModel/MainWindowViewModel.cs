using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConveyorMaterialAnimation.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

      

        // Drum material animation enable
        public bool Material_animation_enable
        {
            get { return _material_animation_enable; }
            set { Set(ref _material_animation_enable, value); }
        }
        private bool _material_animation_enable;


        // Set ellipce colour
        public bool SetEllipseColour
        {
            get { return _setellipsecolour; }
            set { Set(ref _setellipsecolour, value); }
        }
        private bool _setellipsecolour;


        // Commands property definition
        public ICommand Launch_animation_command { get; private set; }
        public ICommand ConveyorStart_command { get; private set; }


        public MainWindowViewModel()
        {
            // Command
            Launch_animation_command = new RelayCommand(LaunchAnimation);
            ConveyorStart_command = new RelayCommand(ConveyorStart);

        }



        // Method
        private void LaunchAnimation()
        {


            if (!Material_animation_enable)
            {

                Material_animation_enable = true;

            }

            else if (Material_animation_enable)

            {


                Material_animation_enable = false;

            }



        }


        // Method
        private void ConveyorStart()
        {
            if (!SetEllipseColour)
            {
                SetEllipseColour = true;
            
            }

            else if (SetEllipseColour)
            {
                SetEllipseColour = false;
            }

}

    }
}
